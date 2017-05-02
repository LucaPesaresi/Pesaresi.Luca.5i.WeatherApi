using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
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

namespace Pesaresi.Luca._5i.WeatherApi
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void btnCalc_Click(object sender, RoutedEventArgs e)
        {
            string citta = txtCitta.Text.ToLower();

            HttpClient client = new HttpClient();        
            string result = await client.GetStringAsync(new Uri(@"http://api.wunderground.com/api/2bb7bfc49b148335/conditions/q/IT/" + txtCitta.Text + ".json"));            
            Ricerca(result);
           
        }
        private void Ricerca(string result)
        {
            try
            {
                RootObject Meteo = JsonConvert.DeserializeObject<RootObject>(result);

                txtDesc.Text = Meteo.current_observation.display_location.full;
                txtAlt.Text = Meteo.current_observation.display_location.elevation + " m";
                txtLat.Text = Meteo.current_observation.display_location.latitude;
                txtLong.Text = Meteo.current_observation.display_location.longitude;
                txtTemp.Text = Meteo.current_observation.temperature_string;
                txtUmi.Text = Meteo.current_observation.relative_humidity;
                txtVel.Text = Meteo.current_observation.wind_gust_kph + " km/h " + Meteo.current_observation.wind_dir;  
                imgTempo.Source = new BitmapImage(new Uri(Meteo.current_observation.icon_url));
                imgTempo.ToolTip = Meteo.current_observation.icon;
            }
            catch
            {
                MessageBox.Show("Località non corretta!", "Attenzione!");
            }          
        }

        private void txtCitta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                btnCalc_Click(null, null);
        }
    }
}
