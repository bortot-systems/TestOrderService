using System.Text.Json.Serialization;

namespace TestOrderService.Models
{    
    public class OrdersRequest
    {
        [JsonPropertyName("pedidos")]
        public List<Order> Orders { get; set; }
    }
}
