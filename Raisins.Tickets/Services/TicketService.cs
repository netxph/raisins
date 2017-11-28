using Raisins.Tickets.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D = Raisins.Tickets.Models;

namespace Raisins.Tickets.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IBeneficiaryForTicketRepository _beneficiaryRepository;
        private readonly ITicketCalculator _ticketCalculator;

        protected ITicketRepository TicketRepository { get { return _ticketRepository; } }
        protected IBeneficiaryForTicketRepository BeneficiaryRepository { get { return _beneficiaryRepository; } }
        protected ITicketCalculator TicketCalculator { get { return _ticketCalculator; } }

        public TicketService(ITicketRepository ticketRepository, IBeneficiaryForTicketRepository beneficiaryRepository, ITicketCalculator ticketCalculator)
        {
            if (ticketRepository == null)
            {
                throw new ArgumentNullException("TicketService:ticketRepository");
            }
            _ticketRepository = ticketRepository;

            if (beneficiaryRepository == null)
            {
                throw new ArgumentNullException("TicketService:beneficiaryRepository");
            }
            _beneficiaryRepository = beneficiaryRepository;

            if (ticketCalculator == null)
            {
                throw new ArgumentNullException("TicketService:ticketCalculator");
            }
            _ticketCalculator = ticketCalculator;
        }

        public void GenerateTickets(int paymentID, int beneficiaryID, decimal amount, decimal exchangeRate, string name)
        {
            D.Tickets tickets = new D.Tickets();
            int iteration = TicketCalculator.Count(amount, exchangeRate);

            for (int i = 0; i < iteration; i++)
            {
                tickets.Add(new D.Ticket(paymentID, beneficiaryID, i, name));
            }
            TicketRepository.Add(tickets);
        }

        public D.Tickets GetAll()
        {
            return TicketRepository.GetAll();
        }

        public D.Beneficiary GetBeneficiary(string name)
        {
            return BeneficiaryRepository.GetBeneficiary(name);
        }

        public int CalculateTickets(decimal amount, D.Currency currency)
        {
            int count = 0;

            if (!currency.IsBulkEligible())
            {
                count = TicketCalculator.CalculateBasic(amount, (int)currency.Ratio);
            }
            else
            {
                if (amount < 500)
                {
                    count = TicketCalculator.CalculateBasic(amount, (int)currency.Ratio);
                }
                else
                {
                    count = ComputeBulk(amount);
                }
            }

            return count;
        }

        //TODO: change to morphism
        protected virtual int ComputeBulk(decimal amount)
        {
            int count = 0;

            if (amount >= 500 && amount < 1000)
            {
                count = TicketCalculator.CalculateSilver(amount);
            }
            else if (amount >= 1000 && amount < 2000)
            {
                count = TicketCalculator.CalculateGold(amount);
            }
            else
            {
                count = TicketCalculator.CalculatePlatinum(amount);
            }

            return count;
        }
    }
}
