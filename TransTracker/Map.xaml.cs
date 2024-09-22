using Microsoft.Maui.Controls.Maps;
using TransTracker.Models;
using static System.Net.Mime.MediaTypeNames;

namespace TransTracker
{
    public partial class Map : ContentPage
    {
        public string BusId { get; set; }
        public Map(string busId)
        {
            InitializeComponent();
            BusId = busId;
            
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LookUpTransID(BusId);
        }
        public async Task LookUpTransID(string busId)
        {
            var apiService = new ApiService();
            string apiUrl = $"http://71.163.166.96:3002/getlocation?id={busId}";

            var result = await apiService.GetAsync<LocationModel>(apiUrl);

            if (result != null)
            {
                double latitude = double.Parse(result.Latitude);
                double longitude = double.Parse(result.Longitude);

                var newPin = new Pin
                {
                    Label = result.Id,
                    Location = new Location(latitude, longitude),
                    Type = PinType.Place
                };

                BusMap.Pins.Add(newPin);
            }
            else
            {
                Console.WriteLine("No valid data from API.");
            }
        }

    }
}
