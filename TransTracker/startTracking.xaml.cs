namespace TransTracker;
using TransTracker.Models;
using System.Timers;
using static System.Net.Mime.MediaTypeNames;

public partial class startTracking : ContentPage
{
    private Timer locationTimer;

    public startTracking()
    {
        InitializeComponent();
    }

    private void OnStartTrackingClicked(object sender, EventArgs e)
    {
        StartLocationUpdates();
    }

    public void StartLocationUpdates()
    {
        if (locationTimer == null)
        {
            locationTimer = new Timer(5000);
            locationTimer.Elapsed += async (sender, e) => await OnLocationUpdate();
            locationTimer.AutoReset = true;
            locationTimer.Enabled = true;
        }
    }

    private async Task OnLocationUpdate()
    {
        await GetCurrentLocation();
    }

    private async Task GetCurrentLocation()
    {
        try
        {
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                var request = new GeolocationRequest(GeolocationAccuracy.High, TimeSpan.FromSeconds(10));

                Location location = await Geolocation.Default.GetLocationAsync(request);

                if (location != null)
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        CurrentLocation.Text = $"Latitude: {location.Latitude}, Longitude: {location.Longitude}";
                    });

                    string url = $"http://71.163.166.96:3002/sendlocation?latitude={location.Latitude}&longitude={location.Longitude}&id=2456";

                    var apiService = new ApiService();
                    var result = await apiService.GetAsync<TestModel>(url);

                    if (result != null && !string.IsNullOrEmpty(result.id))
                    {
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            hours.Text = result.id;
                        });
                    }
                }
                else
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        CurrentLocation.Text = "Unable to get location";
                    });
                }
            });
        }
        catch (Exception ex)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                hours.Text = ex.Message;
            });
        }
    }
}

