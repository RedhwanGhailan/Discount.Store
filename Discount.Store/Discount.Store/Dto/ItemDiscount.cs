using Newtonsoft.Json;

namespace Discount.Store.Dto
{
    public class ItemDiscount
    {
        [JsonProperty("quantity")]
        public int Quantity;

        [JsonProperty("newPrice")]
        public decimal NewPrice;
    }
}
