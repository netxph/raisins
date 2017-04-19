using Raisins.Payments.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Payments.Interfaces
{
    public interface ICurrencyService
    {
        IEnumerable<Currency> GetCurrencies();
    }
}
