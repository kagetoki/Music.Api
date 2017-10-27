using Music.API.Entites.Models;

namespace Music.API.Interface
{
    public interface IPaymentService
    {
        void InitiatePayment(Subscription subscription);
        void FinishPayment(string paymentId);
    }
}
