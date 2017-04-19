using Raisins.Payments.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D = Raisins.Payments.Models;
using EF = Raisins.Data.Models;

namespace Raisins.Data.Repository
{
    public class PaymentTypeRepository : IPaymentTypeRepository
    {
        public RaisinsContext _context { get; set; }
        public PaymentTypeRepository() : this(new RaisinsContext())
        {
        }
        public PaymentTypeRepository(RaisinsContext context)
        {
            _context = context;
        }
        public D.PaymentType Get(string type)
        {
            return ConvertToDomain(_context.Types.FirstOrDefault(c => c.Type == type));
        }

        public IEnumerable<D.PaymentType> GetAll()
        {
            return ConverToDomainList(_context.Types);
        }

        private D.PaymentType ConvertToDomain(EF.PaymentType efType)
        {
            return new D.PaymentType(efType.Type);
        }
        public IEnumerable<D.PaymentType> ConverToDomainList(IEnumerable<EF.PaymentType> efTypes)
        {
            List<D.PaymentType> sources = new List<D.PaymentType>();
            foreach (var efType in efTypes)
            {
                sources.Add(new D.PaymentType(efType.Type));
            }
            return sources;
        }
    }
}
