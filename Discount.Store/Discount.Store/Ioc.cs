using Discount.Store.Services;
using StructureMap;

namespace Discount.Store
{
    internal static class Ioc
    {
        internal static void Configure(IContainer container)
        {
            container.Configure(config =>
            {
                config
                    .For<IMainService>()
                    .Use<MainService>();

                config
                   .For<ICartService>()
                   .Use<CartService>();
            });
        }
    }
}