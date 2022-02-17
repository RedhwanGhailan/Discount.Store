using Discount.Store.Dto;
using Newtonsoft.Json;
using StructureMap;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Discount.Store.Services
{
    public class MainService : IMainService
    {
        private readonly ICartService _cartService;
        private readonly IEnumerable<Item> _cachedItems;

        public MainService(ICartService cartService)
        {
            _cartService = cartService;
            _cachedItems = new List<Item>();
        }

        public void StartProcess()
        {
            Console.WriteLine("Discount Store Started");
            var items = LoadData();
            PrintItems(items);
        }

        private void PrintItems(IEnumerable<Item> items)
        {
            Console.WriteLine();
            Console.WriteLine($"---------------List Of Items--------------");
            Console.WriteLine($"No\t Name\t\t Price\t Currency");
            Console.WriteLine($"------------------------------------------");
            foreach (var item in items)
            {
                PrintItem(item);
            }
            Console.WriteLine($"------------------------------------------");


            var shouldRun = true;
            while (shouldRun)
            {
                Console.Write("Select an item by entering its number: ");
                var input = Console.ReadKey();
                Console.WriteLine();

                var selectedItem = FindItem((input.KeyChar - '0'), items);

                Console.WriteLine();
                Console.WriteLine($"You selected the following Item");
                Console.WriteLine($"------------------------------------------");
                PrintItem(selectedItem);
                Console.WriteLine();

                DisplayOptions(selectedItem);
            }

            Console.Write("\n\rPress any key to exit!");
            Console.ReadKey();
        }

        private Item FindItem(int itemNumber, IEnumerable<Item> items)
        {
            return items.FirstOrDefault(item => item.Number == itemNumber);
        }

        private void PrintItem(Item item)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            if (item.Number == 3)
            {
                Console.WriteLine($"{item.Number}\t{item.Name}\t{item.Price}\t{item.Currency}");
            }
            else
            {
                Console.WriteLine($"{item.Number}\t{item.Name}\t\t{item.Price}\t{item.Currency}");
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        private void DisplayOptions(Item selectedItem)
        {
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1 - Add the Item to the Cart");
            Console.WriteLine("2 - Remove the Item to the Cart ");
            Console.WriteLine("q - Quit");

            var input = Console.ReadKey();
            Console.WriteLine("\n");

            switch (input.Key)
            {
                case ConsoleKey.NumPad1:
                case ConsoleKey.D1:
                    _cartService.Add(selectedItem);
                    break;
                case ConsoleKey.NumPad2:
                case ConsoleKey.D2:
                    _cartService.Remove(selectedItem);
                    break;
                case ConsoleKey.Q:
                    break;
                default:
                    Console.WriteLine("Invalid option!");
                    Console.WriteLine("Try again!");
                    break;
            }
            if (!_cartService.MyCart.Any())
            {
                Console.WriteLine($"Oops! Your cart is empry, you welcome to add Items!");
                Console.WriteLine();
            }
            else
            {
                PrintMyCart();
            }
        }

        private void PrintMyCart()
        {
            var totalItemsQuantity = 0;
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"------------------------------------------");
            Console.WriteLine($"******** Shopping Cart ********");
            Console.WriteLine($"----------------------------------------------------------------------");
            Console.WriteLine($"Name\t\t Unit Price\t Currency \t Quantity \t Amount");
            Console.WriteLine($"----------------------------------------------------------------------");

            foreach (var kvpItem in _cartService.MyCart)
            {
                totalItemsQuantity += kvpItem.Value;
                if (kvpItem.Key.Number == 3)
                {
                    Console.WriteLine($"{kvpItem.Key.Name}\t{kvpItem.Key.Price}\t\t{kvpItem.Key.Currency}\t\t\t{kvpItem.Value}\t{_cartService.GetTotalAmountPerItem(kvpItem)}");
                }
                else
                {
                    Console.WriteLine($"{kvpItem.Key.Name}\t\t{kvpItem.Key.Price}\t\t{kvpItem.Key.Currency}\t\t\t{kvpItem.Value} \t{_cartService.GetTotalAmountPerItem(kvpItem)}");
                }
            }

            Console.WriteLine($"-----------------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Items: {totalItemsQuantity} \t\t\t\t\t\t Total amount: {_cartService.GetTotal()}");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
        }

        public IEnumerable<Item> LoadData()
        {
            if (_cachedItems.Any())
            {
                return _cachedItems;
            }

            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "Discount.Store.JsonItems.json";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream != null)
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        var json = reader.ReadToEnd();

                        try
                        {
                            return JsonConvert.DeserializeObject<List<Item>>(json);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Fatal error when Deserialize the Json object: {e.Message}");
                            throw;
                        }
                    }
                }
            }

            return Enumerable.Empty<Item>().ToList();
        }
    }
}