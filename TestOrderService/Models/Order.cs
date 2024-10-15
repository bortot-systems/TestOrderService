using System.Text.Json.Serialization;

namespace TestOrderService.Models
{
    public class Order
    {
        [JsonPropertyName("pedido_id")]
        public int OrderId { get; set; }

        [JsonPropertyName("produtos")]
        public List<Product> Products { get; set; }
    }
}
