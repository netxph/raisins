using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord.Framework.Config;
using Castle.ActiveRecord;
using Raisins.Services;
using System.Windows.Input;
using System.Threading;

namespace Raisins.Client.Raffle.ViewModel
{
    public class RaffleViewModel : WorkspaceViewModel
    {

        ICommand _drawCommand = null;
        string _ticketNumber = null;

        public RaffleViewModel()
        {
            XmlConfigurationSource source = new XmlConfigurationSource("config.xml");

            ActiveRecordStarter.Initialize(source, typeof(Beneficiary), typeof(Payment), typeof(Currency), typeof(Ticket), typeof(Account), typeof(Role), typeof(Setting));
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

        public string TicketNumber 
        {
            get { return _ticketNumber;  }
            set
            {
                _ticketNumber = value;
                base.OnPropertyChanged("TicketNumber");
            }
        }

        protected void OnRaffleDraw()
        {
            TicketNumber = "HelloWorld";
        }
    }
}
