using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace DecoratorExample;

public static class Program
{
    public static async Task Main(string[] args)
    {
        IServiceCollection services = new ServiceCollection();

        services.AddHttpClient(
            "products",
            configure =>
            {
                configure.BaseAddress = new Uri("https://dummyjson.com/");
            });

        services.AddMemoryCache();

        services.AddTransient<IProvideProducts, ProductApiClient>();
        services.AddTransient<IProvideProductsWithCache, CachingProductApiClient>();

        IServiceProvider provider = services.BuildServiceProvider();

        IProvideProductsWithCache productProvider =
            provider.GetRequiredService<IProvideProductsWithCache>();

        IEnumerable<Product> products1 = await productProvider.GetProductsAsync();

        foreach (Product p in products1.Take(3))
        {
            Console.WriteLine(p.Description);
        }
    }
}
