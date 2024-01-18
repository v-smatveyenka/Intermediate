using Facade.Models;

namespace Facade.Interfaces
{
    public interface IInvoiceSystem
    {
        void SendInvoice(Invoice invoice);
    }
}
