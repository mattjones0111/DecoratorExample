using System.Collections.Generic;
using System.Threading.Tasks;

namespace DecoratorExample;

public interface IProvideProducts
{
    Task<IEnumerable<Product>> GetProductsAsync();
}
