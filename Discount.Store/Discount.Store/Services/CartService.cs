using System;
using System.Collections.Generic;

using Discount.Store.Dto;
namespace Discount.Store.Services
{
    public class CartService : ICartService
    {
        public Dictionary<Item, int> MyCart { get; set; }

        public CartService()
        {
            MyCart = new Dictionary<Item, int>();
        }

        public void Add(Item item)
        {
            if (MyCart.ContainsKey(item))
            {
                MyCart[item] +=1;
            }
            else
            {
                MyCart.Add(item, 1);
            }
        }

        public void Remove(Item item)
        {
            if (MyCart.ContainsKey(item))
            {
                if (MyCart[item] > 1)
                {
                    MyCart[item] -= 1;
                }
                else
                {
                    MyCart.Remove(item);
                } 
            }
        }

        public decimal GetTotal()
        {
            decimal _totalAmount = 0;
            if (MyCart == null) throw new ArgumentNullException(nameof(MyCart));

            foreach (var kvpItem in MyCart)
            {
                if (!kvpItem.Key.OfferDiscount)
                {
                    _totalAmount += kvpItem.Key.Price * kvpItem.Value;
                }
                else
                {
                    var itemQuntity = kvpItem.Value;
                    var discountQuantity = kvpItem.Key.Discount.Quantity;

                    _totalAmount += (itemQuntity % discountQuantity) * kvpItem.Key.Price;
                    _totalAmount += (itemQuntity / discountQuantity) * kvpItem.Key.Discount.NewPrice;
                }         
            }

            return _totalAmount;
        }

        public decimal GetTotalAmountPerItem(KeyValuePair<Item, int> kvpItem)
        {
            decimal _totalAmountPerItem = 0;

            if (!kvpItem.Key.OfferDiscount)
            {
                _totalAmountPerItem += kvpItem.Key.Price * kvpItem.Value;
            }
            else
            {
                var itemQuntity = kvpItem.Value;
                var discountQuantity = kvpItem.Key.Discount.Quantity;

                _totalAmountPerItem += (itemQuntity % discountQuantity) * kvpItem.Key.Price;
                _totalAmountPerItem += (itemQuntity / discountQuantity) * kvpItem.Key.Discount.NewPrice;
            }

            return _totalAmountPerItem;
        }
    }
}
