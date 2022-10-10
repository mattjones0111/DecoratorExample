using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace DecoratorExample;

public sealed class ProductApiClient : IProvideProducts
{
    readonly HttpClient httpClient;

    public ProductApiClient(IHttpClientFactory httpClientFactory)
    {
        httpClient = httpClientFactory.CreateClient("products");
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        HttpResponseMessage response = await httpClient.GetAsync("products");

        response.EnsureSuccessStatusCode();

        Root rootResponse = await response.Content.ReadFromJsonAsync<Root>();

        return rootResponse?.Products;
    }
}

public sealed class CachingApiClientDecorator : IProvideProducts
{
    readonly IProvideProducts innerClient;
    readonly IMemoryCache cache;

    public CachingApiClientDecorator(IProvideProducts innerClient, IMemoryCache cache)
    {
        this.innerClient = innerClient;
        this.cache = cache;
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        return await cache.GetOrCreateAsync(
            "products",
            entry => innerClient.GetProductsAsync());
    }
}
