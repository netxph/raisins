using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raisins.Services.Models;
using System.ComponentModel;
using System.Threading;
using System.Windows.Input;

namespace Raisins.Client.Raffle
{
    public class MainViewModel : INotifyPropertyChanged
    {

        bool _stopped = true;
        int _scrollSpeed = 0;

        public MainViewModel()
        {
            Tickets = RaffleService.RetrieveRandomTickets(PaymentClass.Internal);
            CurrentTicket = Tickets[0];
        }

        Ticket[] _tickets;

        public Ticket[] Tickets
        {
            get { return _tickets; }
            set
            {
                if (_tickets == value) return;

                _tickets = value;
                OnPropertyChanged("Tickets");
            }
        }

        RaffleService _raffleService = null;
        public RaffleService RaffleService 
        {
            get
            {
                if (_raffleService == null)
                {
                    _raffleService = new RaffleService();
                }

                return _raffleService;
            }
        }

        Ticket _currentTicket;

        public Ticket CurrentTicket
        {
            get { return _currentTicket; }
            set
            {
                if (_currentTicket == value) return;

                _currentTicket = value;
                OnPropertyChanged("CurrentTicket");
                OnPropertyChanged("CurrentName");
                OnPropertyChanged("CurrentTicketCode");
            }
        }

        public string CurrentName
        {
            get
            {
                if (CurrentTicket != null)
                {
                    return CurrentTicket.Name;
                }

                return string.Empty;
            }
        }

        public string CurrentTicketCode
        {
            get
            {
                if (CurrentTicket != null)
                {
                    return CurrentTicket.TicketCode;
                }

                return string.Empty;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        ICommand _drawCommand;

        public ICommand DrawCommand
        {
            get
            {
                if (_drawCommand == null)
                {
                    _drawCommand = new RelayCommand(param => OnDrawCommand());
                }

                return _drawCommand;
            }
        }

        protected virtual void OnDrawCommand()
        {
            _stopped = !_stopped;

            if (!_stopped)
            {
                _scrollSpeed = 100;
                var drawThreadStart = new ThreadStart(() =>
                {
                    while (!_stopped)
                    {
                        foreach (var ticket in Tickets)
                        {
                            if (!_stopped)
                            {
                                CurrentTicket = ticket;
                                Thread.Sleep(_scrollSpeed);
                            }
                        }
                    }
                });

                var drawThread = new Thread(drawThreadStart);
                drawThread.IsBackground = true;
                drawThread.Start();
            }
            
        }

    }
}
