using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Castle.ActiveRecord.Framework.Config;
using Castle.ActiveRecord;

namespace Raisins.Services.SecurityConsole
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            XmlConfigurationSource source = new XmlConfigurationSource("config.xml");

            ActiveRecordStarter.Initialize(source, typeof(Beneficiary), typeof(Payment), typeof(Currency), typeof(Ticket), typeof(Account), typeof(Role), typeof(Setting));

            _comboBeneficiary.ItemsSource = Beneficiary.FindAll();
            _comboBeneficiary.DisplayMemberPath = "Name";

            _comboCurrency.ItemsSource = Currency.FindAll();
            _comboCurrency.DisplayMemberPath = "CurrencyCode";

            _comboClass.ItemsSource = Enum.GetValues(typeof(PaymentClass));
            _comboClass.SelectedIndex = 0;
        }

        

        private void _buttonCreate_Click(object sender, RoutedEventArgs e)
        {
            Account account = new Account();
            account.UserName = _textUserName.Text;
            account.Salt = Account.GetSalt();
            account.Password = Account.GetHash(_textPassword.Password, account.Salt);

            Setting setting = new Setting();
            setting.Account = account;
            setting.Beneficiary = (Beneficiary)_comboBeneficiary.SelectedItem;
            setting.Currency = (Currency)_comboCurrency.SelectedItem;
            setting.Location = _textLocation.Text;
            setting.Class = (PaymentClass)Enum.Parse(typeof(PaymentClass), _comboClass.SelectedValue.ToString());

            account.Create();
            setting.Create();

            MessageBox.Show("User added.");
        }

        
    }
}
