using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D = Raisins.Payments.Models;

namespace Raisins.Payments.Interfaces
{
    public interface IPaymentTypeRepository
    {
         IEnumerable <D.PaymentType> GetAll();
        D.PaymentType Get(string type);
    }
}
