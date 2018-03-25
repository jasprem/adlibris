namespace Catalog.Commands
{
    public class UpdateProductStatusCommand
    {
        public string ProductId { get; set; }
        public int TotalBeforeOrder { get; set; }
        public int TotalAfterOrderExecuted { get; set; }
    }
}
