using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace TestOrderService.Models
{
    public class OrderPackingResult
    {
        public int OrderId { get; set; }
        public List<BoxAllocation> Boxes { get; set; }
    }

    public class BoxAllocation
    {
        public string BoxId { get; set; }
        public List<string> Products { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("observacao")]
        public string Message { get; set; }
    }
}
