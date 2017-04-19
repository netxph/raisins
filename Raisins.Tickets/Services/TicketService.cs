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
        private readonly ITicketCalculator _ticketCalculator;
        private readonly IBeneficiaryForTicketRepository _beneficiaryRepository;

        protected ITicketRepository TicketRepository { get { return _ticketRepository; } }
        protected IBeneficiaryForTicketRepository BeneficiaryRepository { get { return _beneficiaryRepository; } }
        protected ITicketCalculator TicketCalculator { get { return _ticketCalculator; } }

        public TicketService(ITicketRepository ticketRepository, IBeneficiaryForTicketRepository beneficiaryRepository)
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

            _ticketCalculator = new TicketCalculator();
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
    }
}
