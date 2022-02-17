using Discount.Store.Services;
using StructureMap;
using System;

namespace Discount.Store
{
    internal class Program
    {
        /// <summary>
        /// Discount Store 
        /// 
        /// </summary>

        private static IContainer Container { get; set; } = new Container();

        static void Main(string[] args)
        {
            Ioc.Configure(Container);
            MainService mainService;

            try
            {
                mainService = Container.GetInstance<MainService>();
                mainService.StartProcess();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Fatal error when setting up instance: {e}");
            }

            Console.WriteLine("Done. Enter to exit.");
            Console.ReadLine();
        }
    }
}
