using Newtonsoft.Json;

namespace SaleSavvy_API.Models
{
    public class Product
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
