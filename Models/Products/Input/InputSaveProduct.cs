namespace SaleSavvy_API.Models.Products.Input
{
    public class InputSaveProduct
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime? CreationDate { get; set; }
    }
}
