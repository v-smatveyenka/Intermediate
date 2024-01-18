using Facade.Interfaces;
using Facade.Models;

namespace Facade
{
    public class InvoiceSystem : IInvoiceSystem
    {
        public void SendInvoice(Invoice invoice)
        {
            Console.WriteLine($@"Email: {invoice.Email}
ProductName: {invoice.ProductName}
Quantity: {invoice.Quantity}
Cost: {invoice.Cost}
");
        }
    }
}
