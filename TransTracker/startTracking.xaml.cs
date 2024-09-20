namespace TransTracker;
using TransTracker.Models;
using static System.Net.Mime.MediaTypeNames;

public partial class startTracking : ContentPage
{
    //private bool isCheckingLocation;
    
    public startTracking()
    {
        InitializeComponent();
    }

    private async void OnStartTrackingClicked(object sender, EventArgs e)
    {
        await GetCurrentLocation();
    }

    private async Task GetCurrentLocation()
    {
        try
        {
            //isCheckingLocation = true;

            var request = new GeolocationRequest(GeolocationAccuracy.High, TimeSpan.FromSeconds(10));

            Location location = await Geolocation.Default.GetLocationAsync(request);

            if (location != null)
            {
                var apiService = new ApiService();
                CurrentLocation.Text = $"Latitude: {location.Latitude}, Longitude: {location.Longitude}";
                string url = $"http://71.163.166.96:3002/sendlocation?latitude={location.Latitude}&longitude={location.Longitude}&id=2456";
                hours.Text = url;
                var result = await apiService.GetAsync<TestModel>(url);
                if (result != null && !string.IsNullOrEmpty(result.id))
                {
                    hours.Text = result.id;
                }
            }
            else
            {
                CurrentLocation.Text = "Unable to get location";
            }
        }
        catch (Exception ex) { 
            hours.Text = ex.Message;
        }
    }
}
