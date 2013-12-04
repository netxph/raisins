using Raisins.Client.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Raisins.Client.Raffle
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool _shuffle = false;

        public RaffleService Raffle { get; set; }

        public System.Timers.Timer Timer { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            Raffle = new RaffleService();
            Timer = new System.Timers.Timer();
            Timer.Interval = 10;
            Timer.Elapsed += (sender, e) =>
                {
                   _labelTicket.Dispatcher.Invoke(new Action(() =>
                        {
                            var ticket = Raffle.GetRandomTicket((PaymentClass)Enum.Parse(typeof(PaymentClass), _comboType.SelectedItem.ToString()));
                            _labelName.Content = ticket.Name;
                            _labelTicket.Content = ticket.TicketCode;
                            DoEvents();
                        }));
                };
            Timer.Enabled = false;

            _comboType.ItemsSource = Enum.GetNames(typeof(PaymentClass));
            _comboType.SelectedIndex = 0;
            
        }

        private void Window_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (!Timer.Enabled)
            {
                Timer.Interval = 50;
                Timer.Start();
            }
            else
            {
                Timer.Stop();
            }
        }

        private void showRandomTicket()
        {
            
        }

        public static void DoEvents()
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background,
                                          new Action(delegate { }));
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);

            Timer.Stop();
            Timer.Dispose();
        }
        
    }
}
