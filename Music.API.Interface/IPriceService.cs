using Music.API.Interface.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Music.API.Interface
{
    public interface IPriceService
    {
        decimal CalculatePrice(Subscription subscription);
    }
}
