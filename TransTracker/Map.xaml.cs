using Microsoft.Maui.Controls.Maps;
using TransTracker.Models;
using System.Timers;
using Microsoft.Maui.Maps;
using Microsoft.Maui;

namespace TransTracker
{
    public partial class Map : ContentPage
    {
        public string BusId { get; set; }
        private System.Timers.Timer locationUpdateTimer;


        public Map(string busId)
        {
            InitializeComponent();
            BusId = busId;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            StartLocationUpdates();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            StopLocationUpdates();
        }

        public void StartLocationUpdates()
        {
            if (locationUpdateTimer == null)
            {
                locationUpdateTimer = new System.Timers.Timer(5000);
                locationUpdateTimer.Elapsed += async (sender, e) => await LookUpTransID(BusId);
                locationUpdateTimer.AutoReset = true;
                locationUpdateTimer.Enabled = true;
            }
        }

        public void StopLocationUpdates()
        {
            if (locationUpdateTimer != null)
            {
                locationUpdateTimer.Stop();
                locationUpdateTimer.Dispose();
                locationUpdateTimer = null;
            }
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

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    BusMap.Pins.Clear();
                    var newPin = new Pin
                    {
                        Label = result.Id,
                        Location = new Location(latitude, longitude),
                        Type = PinType.Place,
                    };

                    BusMap.Pins.Add(newPin);
                    BusMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Location(latitude, longitude), Distance.FromMiles(1))); // Move the map to the new location
                });
            }
            else
            {
                Console.WriteLine("No valid data from API.");
            }
        }
    }
}
