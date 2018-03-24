using System;
using System.Collections.Generic;
using System.Transactions;
using Catalog.Commands;
using Catalog.Domain;
using Catalog.EventHandlers;
using Catalog.Events;
using Catalog.Persistence;

namespace Catalog.CommandProcessors
{
    public class OrderService
    {
        private readonly IProductRepository _productRepository;
        private readonly OrderEventHandler _orderEventHandler;

        public OrderService(
            IProductRepository productRepository,
            OrderEventHandler orderEventHandler
            )
        {
            _productRepository = productRepository;
            _orderEventHandler = orderEventHandler;
        }

        public void ExecuteOrder(OrderCommand orderCommand)
        {
            var productAggregate = _productRepository.Get(orderCommand.ProductId);

            if (productAggregate.GetTotalAvailable() < orderCommand.ItemCount)
            {
                var orderFailedEvent = new OrderFailedEvent
                {
                    CustomerId = orderCommand.CustomerId,
                    CartId = orderCommand.CartId,
                    ProductId = orderCommand.ProductId
                };
                _orderEventHandler.Handle(orderFailedEvent);
                return;
            }

            var remaining = orderCommand.ItemCount;
            var newOrders = new List<Order>();
            var changedProducts = new List<Product>();
            foreach (var product in productAggregate.Products)
            {
                int itemsToBeRemoved;
                if (remaining == 0)
                {
                    break;
                }
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
                var order = new Order(orderCommand.CustomerId, orderCommand.ProductId, product.Shelf, itemsToBeRemoved);
                newOrders.Add(order);
            }

            using (var tran = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    foreach (var order in newOrders)
                    {
                        _productRepository.AddOrder(order);
                    }
                    foreach (var changedProduct in changedProducts)
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

            var orderSucceededEvent = new OrderSucceededEvent
            {
                CustomerId = orderCommand.CustomerId,
                CartId = orderCommand.CartId,
                ProductId = orderCommand.ProductId
            };
            _orderEventHandler.Handle(orderSucceededEvent);
        }
    }
}
