namespace Facade
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var orderService = new OrderService(
                      new ProductCatalog(),
                      new PaymentSystem(),
                      new InvoiceSystem());

            orderService.PlaceOrder("Id1", 20, "test@test.com");
        }
    }
}
