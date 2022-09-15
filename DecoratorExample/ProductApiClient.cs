using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

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
        Console.WriteLine("Fetching products from source api...");

        HttpResponseMessage response = await httpClient.GetAsync("products");

        response.EnsureSuccessStatusCode();

        Root rootResponse = await response.Content.ReadFromJsonAsync<Root>();

        return rootResponse?.Products;
    }
}
