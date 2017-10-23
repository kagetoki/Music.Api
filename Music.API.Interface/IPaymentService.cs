using Music.API.Interface.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Music.API.Interface
{
    public interface IPaymentService
    {
        void InitiatePayment(Subscription subscription);
        void FinishPayment(string paymentId);
    }
}
