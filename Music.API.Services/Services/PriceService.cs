using Music.API.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using Music.API.Entites.Models;

namespace Music.API.Services.Services
{
    public class PriceService : IPriceService
    {
        public decimal CalculatePrice(Subscription subscription)
        {
            return 12m;
        }
    }
}
