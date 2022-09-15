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

        services.AddTransient<IProvideProducts, ProductApiClient>();

        IServiceProvider provider = services.BuildServiceProvider();

        IProvideProducts productProvider =
            provider.GetRequiredService<IProvideProducts>();

        IEnumerable<Product> products = await productProvider.GetProductsAsync();

        foreach (Product p in products.Take(3))
        {
            Console.WriteLine(p.Description);
        }
    }
}
