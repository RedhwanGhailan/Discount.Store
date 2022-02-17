using Discount.Store.Dto;
using System.Collections.Generic;

namespace Discount.Store.Services
{
    public interface ICartService
    {
        Dictionary<Item, int> MyCart { get; set; }

        void Add(Item item);

        void Remove(Item item);

        decimal GetTotal();

        decimal GetTotalAmountPerItem(KeyValuePair<Item, int> kvpItem);

    }
}
