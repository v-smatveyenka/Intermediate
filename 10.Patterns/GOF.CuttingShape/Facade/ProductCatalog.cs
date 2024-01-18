using Facade.Interfaces;
using Facade.Models;

namespace Facade
{
    public class ProductCatalog : IProductCatalog
    {
        public ProductDetails GetProductDetails(string productId) => new()
        {
            Id = "Id1",
            Name = "Milk",
            Price = 100
        };
    }
}
