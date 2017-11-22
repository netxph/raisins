using Raisins.Tickets.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raisins.Tickets.Models;
using EF = Raisins.Data.Models;

namespace Raisins.Data.Repository
{
    public class BeneficiaryForTicketRepository : IBeneficiaryForTicketRepository
    {
        public RaisinsContext _context { get; set; }
        public BeneficiaryForTicketRepository() : this(RaisinsContext.Instance)
        {
        }
        public BeneficiaryForTicketRepository(RaisinsContext context)
        {
            _context = context;
        }
        public Beneficiary GetBeneficiary(string name)
        {
            return ConvertToDomain(_context.Beneficiaries.FirstOrDefault(b => b.Name == name));
        }
        private Beneficiary ConvertToDomain(EF.Beneficiary efBeneficiary)
        {
            return new Beneficiary(efBeneficiary.BeneficiaryID, efBeneficiary.Name);
        }
    }
}
