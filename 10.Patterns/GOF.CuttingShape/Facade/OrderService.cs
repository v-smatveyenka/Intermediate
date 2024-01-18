using Facade.Interfaces;
using Facade.Models;

namespace Facade
{
    public class OrderService : IOrderService
    {
        private readonly IProductCatalog _productCatalog;
        private readonly IPaymentSystem _paymentSystem;
        private readonly IInvoiceSystem _invoiceSystem;

        public OrderService(IProductCatalog productCatalog, IPaymentSystem paymentSystem, IInvoiceSystem invoiceSystem)
        {
            _productCatalog = productCatalog;
            _paymentSystem = paymentSystem;
            _invoiceSystem = invoiceSystem;
        }

        public void PlaceOrder(string productId, int quantity, string email)
        {
            var product = _productCatalog.GetProductDetails(productId);
            var cost = quantity * product.Price;

            var payment = new Payment()
            {
                ProductId = productId,
                Cost = cost
            };

            var paymentResult = _paymentSystem.MakePayment(payment);

            if (!paymentResult)
            {
                throw new InvalidOperationException("Payment failed");
            }

            var invoice = new Invoice()
            {
                ProductName = product.Name,
                Quantity = quantity,
                Cost = cost,
                Email = email
            };

            _invoiceSystem.SendInvoice(invoice);
        }
    }
}
