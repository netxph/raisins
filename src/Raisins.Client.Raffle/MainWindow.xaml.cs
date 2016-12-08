using Raisins.Client.Randomizer.RandomOrg;
using System.Windows;

namespace Raisins.Client.Raffle
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var data = new EntityFrameworkRaisinsDataProvider();
            var random = new RandomOrgIntegerRandomizerService("d8fd8706-0482-4460-8017-59719fd3ccb9");

            this.DataContext = new RaffleViewModel(
                                new RaffleService(data, random));
        }
    }
}
