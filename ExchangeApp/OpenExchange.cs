using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ExchangeApp
{
    public class OpenExchange
    {
        public OpenExchange()
        {
            RetrieveExchangeRates();
        }

        private OpenExchangeResponse currencyJsonData = null;

        public float GetExchangeRate(string country)
        {
            float val;
            if (currencyJsonData != null)
            {
                try
                {
                    decimal rate = currencyJsonData.Rates[country]; // decimal is like float, but used for currency
                    val = (float)rate;    // convert to float
                }
                catch (KeyNotFoundException)
                {
                    val = -1.0f;
                }
            }
            else
                val = -1.0f;    // -1 is invalid. We could check this on return.
            return val;
        }

        [DataContract]
        public class OpenExchangeResponse
        {
            [DataMember(Name = "disclaimer")]
            public string Disclaimer { get; set; }

            [DataMember(Name = "license")]
            public string License { get; set; }

            [DataMember(Name = "timestamp")]
            public string Timestamp { get; set; }

            [DataMember(Name = "base")]
            public string Base { get; set; }

            [DataMember(Name = "rates")]
            public Dictionary<string, decimal> Rates { get; set; }
        }

        private void RetrieveExchangeRates()
        {
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
               MessageBox.Show("Network error. Check your internet connecton!");
            }
            else
            {
                WebClient webClient = new WebClient();
                webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadStringCompleted);
                webClient.DownloadStringAsync(new Uri("http://openexchangerates.org/api/latest.json?app_id=4873a24a1a2546f88de76f1636b640ca"));
            }
        }

        private void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                string json = e.Result;
                if (!string.IsNullOrEmpty(json))
                {
                    // parsing the Data
                    currencyJsonData = JsonConvert.DeserializeObject<OpenExchangeResponse>(json);
                }
            }
        }
    }
}
