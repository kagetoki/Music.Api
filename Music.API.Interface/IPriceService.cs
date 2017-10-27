using Music.API.Entites.Models;

namespace Music.API.Interface
{
    public interface IPriceService
    {
        decimal CalculatePrice(Subscription subscription);
    }
}
