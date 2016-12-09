using Raisins.Client.Web.Models;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;
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
        private Ticket _facadeTicket;

        private bool _canExecuteRaffle;

        public bool CanExecuteRaffle
        {
            get
            {
                return _canExecuteRaffle;
            }
            set
            {
                _canExecuteRaffle = value;

                OnNotifyPropertyChanged("CanExecuteRaffle");
                OnNotifyPropertyChanged("DrawRaffleVictor");
            }
        }

        protected virtual RaffleService RaffleService
        {
            get
            {
                return _raffleService;
            }
        }

        public Ticket FacadeTicket
        {
            get
            {
                return _facadeTicket;
            }
            set
            {
                _facadeTicket = value;

                OnNotifyPropertyChanged("FacadeTicket");
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
                    (o) => CanExecuteRaffle,
                    (o) => OnDrawRaffleVictor((PaymentClass)o));
            }
        }

        public ICommand LoadCommand
        {
            get
            {
                return new RelayCommand(
                    (o) => true,
                    (o) =>
                    {
                        Task.Run(
                            () => RaffleService.LoadData())
                        .ContinueWith((t) =>
                        {
                            CanExecuteRaffle = true;
                        });
                    });
            }
        }

        protected virtual void OnDrawRaffleVictor(PaymentClass paymentClass)
        {
            try
            {
                WinningTicket = null;

                CancellationTokenSource cts = new CancellationTokenSource();

                //todo: find a better way to handle this asynchronously
                Task.Run(() =>
                {
                    BeginUIWait(paymentClass, cts.Token);
                });


                Task.Run(async()=>
                {
                    var ticket = RaffleService.GetRandomTicket(paymentClass);

                    await Task.Delay(TimeSpan.FromSeconds(3));

                    return ticket;
                })
                .ContinueWith((t) =>
                {
                    cts.Cancel();

                    WinningTicket = t.Result;
                });


            }
            catch (Exception ex)
            {
                HandleException?.Invoke(this, ex);
            }
        }

        private void BeginUIWait(PaymentClass paymentClass, CancellationToken token)
        {
            //just want dirty numbers
            var rnd = new System.Random();
            var tickets = RaffleService.GetTickets(paymentClass);

            while (!token.IsCancellationRequested)
            {
                FacadeTicket = tickets.ElementAt(rnd.Next(0, tickets.Count()));
            }

            FacadeTicket = null;
        }

        public RaffleViewModel(RaffleService raffleService)
        {
            if (raffleService == null)
            {
                throw new ArgumentNullException("raffleService");
            }

            _raffleService = raffleService;
        }

        protected virtual void OnNotifyPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
