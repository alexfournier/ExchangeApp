using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ExchangeApp.Resources;
using System.Globalization;
using Microsoft.Phone.Tasks;
using System.Windows.Input;
namespace ExchangeApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        string localString = string.Empty;
        float exchangeRate;
        float baseAmount;
        float result;
        OpenExchange exchangeObj;
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            exchangeObj = new ExchangeApp.OpenExchange();
        }

        private bool GetBaseValue()
        {
            try
            {
                baseAmount = Convert.ToSingle(InputField.Text);
                return true;
            }
            catch (Exception)
            {
                MessageBox.Show("No Value Entered!");
                return false;
            }
        }

        private void CalculateResult()
        {
            result = baseAmount * exchangeRate;
        }

        private void DisplayResult()
        {
            CultureInfo cultureSelected = new CultureInfo(localString);   // create a "Culture" specifier
            ResultField.Text = string.Format(cultureSelected, "{0:C}", result);    // convert the value to a currency, using the Euro format.
        }

        private void CADButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

            if (GetBaseValue())
            {
                exchangeRate = exchangeObj.GetExchangeRate("CAD");

                if (exchangeRate > 0)
                {
                    localString = "en-US";
                    //GetExchangeRate(CADButton.Name);
                    CalculateResult();
                    DisplayResult();
                }
                else
                {
                    MessageBox.Show("Server is unavailable. Try again");
                }
            }
        }

        private void EuroButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (GetBaseValue())
            {
                exchangeRate = exchangeObj.GetExchangeRate("EUR");
                if (exchangeRate > 0)
                {
                    localString = "fr-FR";
                    //GetExchangeRate(EuroButton.Name);
                    CalculateResult();
                    DisplayResult();
                }
                else
                {
                    MessageBox.Show("Server is unavailable. Try again");
                }
            }
        }
        private void GBPButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (GetBaseValue())
            {
                exchangeRate = exchangeObj.GetExchangeRate("GBP");
                if (exchangeRate > 0)
                {
                    localString = "en-GB";
                    //GetExchangeRate(GBPButton.Name);
                    CalculateResult();
                    DisplayResult();
                }
                else
                {
                    MessageBox.Show("Server is unavailable. Try again");
                }
            }
        }
        private void YenButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (GetBaseValue())
            {
                exchangeRate = exchangeObj.GetExchangeRate("JPY");
                if (exchangeRate > 0)
                {
                    localString = "ja-JP";
                    //GetExchangeRate(YenButton.Name);
                    CalculateResult();
                    DisplayResult();
                }
                else
                {
                    MessageBox.Show("Server is unavailable. Try again");
                }
            }
        }

        private void InputField_GotFocus(object sender, System.Windows.Input.GestureEventArgs e)
        {
            InputField.Text = "";
        }

        private void AppBarIconBtn(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/AboutPage.xaml", UriKind.Relative));
        }

        private void closeKB(object sender, EventArgs e)
        {
            this.Focus();
        }

        private void fetchData(object sender, EventArgs e)
        {
            exchangeObj = new ExchangeApp.OpenExchange();
        }
    }
}