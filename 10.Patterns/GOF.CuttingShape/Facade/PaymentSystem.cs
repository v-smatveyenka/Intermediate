using Facade.Interfaces;
using Facade.Models;

namespace Facade
{
    public class PaymentSystem : IPaymentSystem
    {
        public bool MakePayment(Payment payment) => true;
    }
}
