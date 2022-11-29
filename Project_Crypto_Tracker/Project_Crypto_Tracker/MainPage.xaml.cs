using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;
using Project_Crypto_Tracker.Model;
using Microsoft.AppCenter.Analytics;

namespace Project_Crypto_Tracker
{
    public partial class MainPage : ContentPage
    {
        private string CoinapiKey = "FF93A9CB-9604-4112-BCF7-01331B4ABE4A";
        private string baseImgUrl = "https://s3.eu-central-1.amazonaws.com/bbxt-static-icons/type-id/png_64/";

        public MainPage()
        {
            InitializeComponent();
            Analytics.TrackEvent("MainPage Opened",
                new Dictionary<string, string>() { { "Page", nameof(MainPage)} });
            CoinListView.ItemsSource = GetCoins();
        }
        
        private void Refresh_Button_Clicked(object sender, EventArgs e)
        {

            CoinListView.ItemsSource = GetCoins();
        }

        private List<Coin> GetCoins()
        {
            List<Coin> coins;

            var client = new RestClient("http://rest.coinapi.io/v1/assets?filter_asset_id=BTC;BTS;NXT;ARI;GEO");
            var request = new RestRequest();
            request.AddHeader("X-CoinAPI-Key", CoinapiKey);

            //deserialise the response
            var response = client.Execute(request);
            coins = JsonConvert.DeserializeObject<List<Coin>>(response.Content);

            foreach (var c in coins)
            {
                if (!String.IsNullOrEmpty(c.Id_Icon))
                    c.Icon_Url = baseImgUrl + c.Id_Icon.Replace("-", "") + ".png";
                else
                    c.Icon_Url = "";


            }
            return coins;
          
        }
    }
}
