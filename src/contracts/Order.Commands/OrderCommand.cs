﻿namespace Order.Commands
{
    public class OrderCommand
    {
        public string CustomerId { get; set; }
        public string CartId { get; set; }
        public string ProductId { get; set; }
        public int ItemCount { get; set; }
    }
}
