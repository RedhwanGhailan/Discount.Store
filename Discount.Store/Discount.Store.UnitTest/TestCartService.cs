using Discount.Store.Services;
using Discount.Store.Dto;
using NUnit.Framework;
using FluentAssertions;
using System;
using System.Linq;

namespace Discount.Store.UnitTest
{
    /// <summary>
    /// Discount Store Tester
    /// This test is based on Given When Then (GWT)
    /// Using NUnit Framework
    /// </summary>

    [TestFixture]
    public class TestCartService
    {
        private ICartService _cartService;
        private Item _item;
        
        [SetUp]
        public void SetUp()
        {
            _cartService = new CartService();
            _item = null;
        }

        [Test]
        public void Test_Add_Item_To_MyCart()
        {
            GivenNewItem();

            WhenAddAnItem();

            ThenMyCartShouldNotBeEmpty();
            ThenMyCartShouldHaveItemWithQuantityOfOne();
            ThenMyCartShouldHaveItemWithQuantityOfGreaterThanOne();
        }

        public void Test_Remove_Item_To_MyCart()
        {
            GivenNewItem();

            WhenRemoveAnItem();

            ThenMyCartShouldNotBeEmpty();
            ThenMyCartShouldHaveItemWithQuantityOfOne();
        }

        public void Test_GetTotal_Amount_MyCart()
        {
            GivenNewItem();

            WhenGetTotalAmount();

            ThenMyCartShouldNotBeEmpty();
            ThenMyCartShouldHaveItemWithQuantityOfOne();
            ThenMyCartShouldHaveItemWithQuantityOfGreaterThanOne();
            ThenMyCartShouldUseTheDiscountOfAnItemIfExist();
            ThenMyCartShouldUseTheNormalPriceOfAnItemIfNoDiscount();
        }

        private void ThenMyCartShouldUseTheNormalPriceOfAnItemIfNoDiscount()
        {
            _cartService.MyCart.FirstOrDefault(item => item.Key.Number == _item.Number)
               .Key.OfferDiscount.Should().BeFalse();
        }

        private void ThenMyCartShouldUseTheDiscountOfAnItemIfExist()
        {
            _cartService.MyCart.FirstOrDefault(item => item.Key.Number == _item.Number)
                .Key.OfferDiscount.Should().BeTrue();
        }

        private void WhenGetTotalAmount()
        {
            _cartService.GetTotal();
        }

        private void ThenMyCartShouldHaveItemWithQuantityOfGreaterThanOne()
        {
            _cartService.MyCart.ContainsKey(_item).Should().BeTrue();
            _cartService.MyCart[_item].Should().BeGreaterThan(1);
        }

        private void WhenRemoveAnItem()
        {
            _cartService.Remove(_item);
        }

        public void ThenMyCartShouldHaveItemWithQuantityOfOne()
        {
            _cartService.MyCart.ContainsKey(_item).Should().BeTrue();
            _cartService.MyCart[_item].Should().Be(1);
            _cartService.MyCart[_item].Should().BeGreaterThan(1);
        }

        public void ThenMyCartShouldNotBeEmpty()
        {
            _cartService.MyCart.Should().NotBeNullOrEmpty();
        }

        public void WhenAddAnItem()
        {
            _cartService.Add(_item);
        }

        public void GivenNewItem()
        {
            _item = new Item();
            _item.Name = "Test Item";
            _item.Number = 1;
            _item.Price = 1;
            _item.Currency = "Euro";
            _item.OfferDiscount = true;
            _item.Discount = new ItemDiscount();
            _item.Discount.Quantity = 2;
            _item.Discount.NewPrice = 1.5m;
        }
    }
}
