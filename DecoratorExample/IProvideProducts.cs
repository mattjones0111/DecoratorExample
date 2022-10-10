using System.Collections.Generic;
using System.Threading.Tasks;

namespace DecoratorExample;

public interface IProvideProducts
{
    Task<IEnumerable<Product>> GetProductsAsync();
}

public interface IProvideProductsWithCache
{
    Task<IEnumerable<Product>> GetProductsAsync();
}
