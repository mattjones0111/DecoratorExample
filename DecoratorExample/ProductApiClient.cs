using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace DecoratorExample;

public sealed class ProductApiClient : IProvideProducts
{
    readonly IMemoryCache cache;
    readonly HttpClient httpClient;

    public ProductApiClient(
        IHttpClientFactory httpClientFactory,
        IMemoryCache cache)
    {
        httpClient = httpClientFactory.CreateClient("products");
        this.cache = cache;
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        return await cache.GetOrCreateAsync(
            "products",
            async entry => 
            {
                HttpResponseMessage response = await httpClient.GetAsync("products");

                response.EnsureSuccessStatusCode();

                Root rootResponse = await response.Content.ReadFromJsonAsync<Root>();

                return rootResponse?.Products;
            });
    }
}
