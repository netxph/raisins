using Raisins.Payments.Interfaces;
using Raisins.Payments.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Payments.Services
{
    public class BeneficiaryService : IBeneficiaryService
    {
        private readonly IBeneficiaryForPaymentRepository _beneficiaryRepository;
        protected IBeneficiaryForPaymentRepository BeneficiaryRepository { get { return _beneficiaryRepository; } }

        public BeneficiaryService(IBeneficiaryForPaymentRepository beneficiaryRepository)
        {
            if (beneficiaryRepository == null)
            {
                throw new ArgumentNullException("BeneficiaryService:beneficiaryRepository");
            }

            _beneficiaryRepository = beneficiaryRepository;
        }
        public IEnumerable<Beneficiary> GetBeneficiaries()
        {
            return BeneficiaryRepository.GetAll();
        }
    }
}
