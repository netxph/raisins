using EF = Raisins.Data.Models;
using Raisins.Payments.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D = Raisins.Payments.Models;

namespace Raisins.Data.Repository
{
    public class PaymentSourceRepository : IPaymentSourceRepository
    {
        public RaisinsContext _context { get; set; }
        public PaymentSourceRepository() : this(new RaisinsContext())
        {
        }
        public PaymentSourceRepository(RaisinsContext context)
        {
            _context = context;
        }
        public D.PaymentSource Get(string source)
        {
            return ConvertToDomain(_context.Sources.FirstOrDefault(c => c.Source == source));
        }

        public IEnumerable<D.PaymentSource> GetAll()
        {
            return ConverToDomainList(_context.Sources);
        }

        private D.PaymentSource ConvertToDomain(EF.PaymentSource efSource)
        {
            return new D.PaymentSource(efSource.Source);
        }
        public IEnumerable<D.PaymentSource> ConverToDomainList(IEnumerable<EF. PaymentSource> efSources)
        {
            List<D.PaymentSource> sources = new List<D.PaymentSource>();
            foreach (var efSource in efSources)
            {
                sources.Add(new D.PaymentSource(efSource.Source));
            }
            return sources;
        }
    }
}
