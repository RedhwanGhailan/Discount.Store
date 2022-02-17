using Newtonsoft.Json;

namespace Discount.Store.Dto
{
    public class Item
    {
        [JsonProperty("itemNumber")]
        public int Number;

        [JsonProperty("itemName")]
        public string Name;

        [JsonProperty("itemPrice")]
        public decimal Price;

        [JsonProperty("itemCurrency")]
        public string Currency;

        [JsonProperty("itemDiscount")]
        public ItemDiscount Discount;

        [JsonProperty("itemOfferDiscount")]
        public bool OfferDiscount;
    }
}
