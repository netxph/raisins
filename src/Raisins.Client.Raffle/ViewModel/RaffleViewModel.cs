using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord.Framework.Config;
using Castle.ActiveRecord;
using Raisins.Services;
using System.Windows.Input;
using System.Threading;
using System.Security.Cryptography;
using NHibernate.Criterion;

namespace Raisins.Client.Raffle.ViewModel
{
    public class RaffleViewModel : WorkspaceViewModel
    {

        ICommand _drawCommand = null;
        ICommand _loadedCommand = null;
        
        string _winningTicketNumber = null;
        string _winner = null;
        string _selectedPaymentClass = null;
        Ticket[] _raffleTickets = null;

        public RaffleViewModel()
        {
            XmlConfigurationSource source = new XmlConfigurationSource("config.xml");

            ActiveRecordStarter.Initialize(source, typeof(Beneficiary), typeof(Payment), typeof(Currency), typeof(Ticket), typeof(Account), typeof(Role), typeof(Setting), typeof(WinnerLog));
        }

        public string[] PaymentClasses
        {
            get
            {
                return Enum.GetNames(typeof(PaymentClass));
            }
        }

        public Ticket[] RaffleTickets
        {
            get { return _raffleTickets; }
            set
            {
                _raffleTickets = value;
                OnPropertyChanged("RaffleTickets");
            }
        }

        public string SelectedPaymentClass
        {
            get
            {
                if (_selectedPaymentClass == null)
                {
                    _selectedPaymentClass = PaymentClass.Internal.ToString();
                }

                return _selectedPaymentClass;
            }
            set 
            {
                _selectedPaymentClass = value;
                InitializeData();
                OnPropertyChanged("SelectedPaymentClass");
            }

        }

        public ICommand DrawCommand
        {
            get
            {
                if (_drawCommand == null)
                    _drawCommand = new RelayCommand(param => this.OnRaffleDraw());

                return _drawCommand;
            }
        }


        public ICommand LoadedCommand
        {
            get
            {
                if (_loadedCommand == null)
                    _loadedCommand = new RelayCommand(param => this.OnLoad());

                return _loadedCommand;
            }
        }

        protected void OnLoad()
        {
            InitializeData();
        }

        protected void InitializeData()
        {
            RNGCryptoServiceProvider rnd = new RNGCryptoServiceProvider();

            RaffleTickets = Ticket.FindAllByPaymentClass((PaymentClass)Enum.Parse(typeof(PaymentClass), SelectedPaymentClass));

            Ticket[] winners = RaffleTickets.Where(ticket => ticket.WinnerLog != null).ToArray();

            RaffleTickets = RaffleTickets.Except(winners).ToArray();
        }

        private int GetRandom(RNGCryptoServiceProvider rnd)
        {
            byte[] randomInt = new byte[4];
            rnd.GetBytes(randomInt);
            return Convert.ToInt32(randomInt[0]);
        }

        public string WinningTicketNumber 
        {
            get { return _winningTicketNumber;  }
            set
            {
                _winningTicketNumber = value;
                base.OnPropertyChanged("WinningTicketNumber");
            }
        }

        public string Winner
        {
            get { return _winner; }
            set
            {
                _winner = value;
                base.OnPropertyChanged("Winner");
            }
        }

        protected void OnRaffleDraw()
        {
            int max = RaffleTickets.Length;

            if (max > 0)
            {
                Random rnd = new Random((int)DateTime.UtcNow.Ticks);

                Ticket winningTicket = null;

                do
                {
                    winningTicket = RaffleTickets[rnd.Next(max)];
                } while (winningTicket.WinnerLog != null);

                WinningTicketNumber = winningTicket.TicketCode;
                Winner = winningTicket.Name;
                

                WinnerLog winnerLog = new WinnerLog()
                {
                    Ticket = winningTicket,
                    Name = Winner,
                    CreatedDate = DateTime.UtcNow
                };

                winnerLog.Create();
                RaffleTickets = RaffleTickets.Except(new Ticket[] { winningTicket }).ToArray();
            }
        }
    }
}
