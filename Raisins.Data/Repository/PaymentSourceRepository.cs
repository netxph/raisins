using DATA = Raisins.Data.Models;
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
        public PaymentSourceRepository() : this(RaisinsContext.Instance)
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

        private D.PaymentSource ConvertToDomain(DATA.PaymentSource efSource)
        {
            return new D.PaymentSource(efSource.Source);
        }
        public IEnumerable<D.PaymentSource> ConverToDomainList(IEnumerable<DATA. PaymentSource> efSources)
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
