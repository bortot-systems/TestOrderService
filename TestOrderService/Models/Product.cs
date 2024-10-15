using System.Text.Json.Serialization;

namespace TestOrderService.Models
{
    public class Product
    {
        [JsonPropertyName("produto_id")]
        public string ProductId { get; set; }

        [JsonPropertyName("dimensoes")]
        public Dimensions Dimensions { get; set; }
    }

}
