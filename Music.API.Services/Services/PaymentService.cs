using Music.API.Interface;
using Music.API.Entites.Models;
using System.Collections.Concurrent;
using Music.API.Entities.Commands;

namespace Music.API.Services.Services
{
    public class PaymentService : IPaymentService
    {
        private static ConcurrentDictionary<string, Subscription> AwaitingSubscriptions = new ConcurrentDictionary<string, Subscription>();
        private readonly IReleaseService _releaseService;
        private readonly IPriceService _priceService;
        public PaymentService(IReleaseService releaseService, IPriceService priceService)
        {
            _priceService = priceService;
            _releaseService = releaseService;
        }
        public void FinishPayment(string paymentId)
        {
            //this is emulation too. Payment Id should be used to get subscription id
            //and only after that we can get subscription instance
            Subscription sub;
            AwaitingSubscriptions.TryGetValue(paymentId, out sub);
            var cost = _priceService.CalculatePrice(sub);
            var subscriptionCommand = new SubscriptionCreateCommand(sub.ReleaseId, sub.UtcExpirationDate, cost, sub.ShopIds, sub.OwnerId);
            ActorModel.TellReleaseActor(subscriptionCommand.ReleaseId, subscriptionCommand);
        }

        public void InitiatePayment(Subscription subscription)
        {
            //it should be stored to persistent storage
            //but for now I'm just emulating it
            AwaitingSubscriptions.TryAdd(subscription.SubscriptionId, subscription);
        }
    }
}
