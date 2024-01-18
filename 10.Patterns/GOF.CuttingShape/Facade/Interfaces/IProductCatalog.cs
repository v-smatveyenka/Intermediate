using Facade.Models;

namespace Facade.Interfaces
{
    public interface IProductCatalog
    {
        ProductDetails GetProductDetails(string productId);
    }
}
