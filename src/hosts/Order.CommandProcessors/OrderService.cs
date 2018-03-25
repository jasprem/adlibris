using System;
using System.Collections.Generic;
using System.Transactions;
using Catalog.CommandProcessors;
using Catalog.Commands;
using Catalog.Domain;
using Catalog.Persistence;
using Order.Commands;
using Order.EventProcessors;
using Order.Events;

namespace Order.CommandProcessors
{
    public class OrderService
    {
        private readonly IProductRepository _productRepository;
        private readonly OrderEventHandler _orderEventHandler;
        private readonly UpdateProductStatusCommandHandler _updateProductStatusCommandHandler;

        public OrderService(
            IProductRepository productRepository,
            OrderEventHandler orderEventHandler,
            UpdateProductStatusCommandHandler updateProductStatusCommandHandler)
        {
            _productRepository = productRepository;
            _orderEventHandler = orderEventHandler;
            _updateProductStatusCommandHandler = updateProductStatusCommandHandler;
        }

        public void ExecuteOrder(OrderCommand orderCommand)
        {
            var productAggregate = _productRepository.Get(orderCommand.ProductId);
            var totalAvailable = productAggregate.GetTotalAvailable();

            if (totalAvailable < orderCommand.ItemCount)
            {
                PublishOrderFailedEvent(orderCommand);
                return;
            }

            var orderResult = CreateOrders(productAggregate, orderCommand);
            PersistOrderResult(orderResult);
            PublishOrderSucceededEvent(orderCommand);
            UpdateProductAvailability(orderCommand.ProductId, totalAvailable - orderCommand.ItemCount);
        }

        private static (List<Catalog.Domain.Order> newOrders, List<Product> changedProducts) CreateOrders(ProductAggregate productAggregate, OrderCommand orderCommand)
        {
            var remaining = orderCommand.ItemCount;
            var newOrders = new List<Catalog.Domain.Order>();
            var changedProducts = new List<Product>();
            foreach (var product in productAggregate.Products)
            {
                int itemsToBeRemoved;
                if (remaining == 0) break;
                if (product.Available == 0) continue;
                if (remaining > product.Available)
                {
                    itemsToBeRemoved = product.Available;
                    remaining = remaining - itemsToBeRemoved;
                }
                else
                {
                    itemsToBeRemoved = remaining;
                    remaining = 0;
                }
                product.Remove(itemsToBeRemoved);
                changedProducts.Add(product);
                var order = new Catalog.Domain.Order(orderCommand.CustomerId, orderCommand.ProductId, product.Shelf, itemsToBeRemoved);
                newOrders.Add(order);
            }

            return (newOrders, changedProducts);
        }

        private void PersistOrderResult((List<Catalog.Domain.Order> newOrders, List<Product> changedProducts) orderResult)
        {
            using (var tran = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    foreach (var order in orderResult.newOrders)
                    {
                        _productRepository.AddOrder(order);
                    }
                    foreach (var changedProduct in orderResult.changedProducts)
                    {
                        _productRepository.UpdateProduct(changedProduct);
                    }
                    tran.Complete();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        private void PublishOrderFailedEvent(OrderCommand orderCommand)
        {
            var orderFailedEvent = new OrderFailedEvent
            {
                CustomerId = orderCommand.CustomerId,
                CartId = orderCommand.CartId,
                ProductId = orderCommand.ProductId
            };
            _orderEventHandler.Handle(orderFailedEvent);
        }

        private void PublishOrderSucceededEvent(OrderCommand orderCommand)
        {
            var orderSucceededEvent = new OrderSucceededEvent
            {
                CustomerId = orderCommand.CustomerId,
                CartId = orderCommand.CartId,
                ProductId = orderCommand.ProductId,
                TotalOrdered = orderCommand.ItemCount
            };
            _orderEventHandler.Handle(orderSucceededEvent);
        }

        private void UpdateProductAvailability(string productId, int current)
        {
            var updateProductStatusCommand = new UpdateProductStatusCommand
            {
                ProductId = productId,
                TotalAfterOrderExecuted = current
            };
            _updateProductStatusCommandHandler.Handle(updateProductStatusCommand);
        }
    }
}
