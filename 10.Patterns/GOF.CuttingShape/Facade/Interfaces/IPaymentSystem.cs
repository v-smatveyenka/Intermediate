using Facade.Models;

namespace Facade.Interfaces
{
    public interface IPaymentSystem
    {
        bool MakePayment(Payment payment);
    }
}
