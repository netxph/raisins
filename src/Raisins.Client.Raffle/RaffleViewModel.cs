using Raisins.Client.Raffle;
using Raisins.Client.Web.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Raisins.Client.Raffle
{
    public class RaffleViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<Exception> HandleException;

        private readonly RaffleService _raffleService;
        private Ticket _winningTicket;
        private PaymentClass _selectedPaymentClass;

        protected virtual RaffleService RaffleService
        {
            get
            {
                return _raffleService;
            }
        }

        public ObservableCollection<PaymentClass> PaymentClasses
        {
            get;
            set;
        }

        public PaymentClass SelectedPaymentClass
        {
            get
            {
                return _selectedPaymentClass;
            }
            set
            {
                _selectedPaymentClass = value;

                OnNotifyPropertyChanged("SelectedPaymentClass");
            }
        }

        public Ticket WinningTicket
        {
            get
            {
                return _winningTicket;
            }
            set
            {
                _winningTicket = value;

                OnNotifyPropertyChanged("WinningTicket");
            }
        }

        public ICommand DrawRaffleVictor
        {
            get
            {
                return new RelayCommand(
                    (o) => true,
                    (o) => OnDrawRaffleVictor());
            }
        }

        protected virtual void OnDrawRaffleVictor()
        {
            try
            {
                Task.Run(async () =>
                {
                    var ticket = RaffleService.GetRandomTicket(SelectedPaymentClass);

                    await Task.Delay(TimeSpan.FromSeconds(4));

                    return ticket;
                })
                .ContinueWith((t) =>
                {
                    WinningTicket = t.Result;
                });
            }
            catch (Exception ex)
            {
                HandleException?.Invoke(this, ex);
            }
        }

        public RaffleViewModel(RaffleService raffleService)
        {
            if(raffleService == null)
            {
                throw new ArgumentNullException("raffleService");
            }

            _raffleService = raffleService;

            PaymentClasses = new ObservableCollection<PaymentClass>(
                                Enum.GetValues(typeof(PaymentClass))
                                    .Cast<PaymentClass>());
        }

        protected virtual void OnNotifyPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
