using System.Text.Json.Serialization;

namespace TestOrderService.Models
{
    public class Dimensions
    {
        [JsonPropertyName("altura")]
        public int Height { get; set; }
        [JsonPropertyName("largura")]
        public int Width { get; set; }
        [JsonPropertyName("comprimento")]
        public int Length { get; set; }
    }
}
