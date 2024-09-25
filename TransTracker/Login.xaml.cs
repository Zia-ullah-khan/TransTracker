using Newtonsoft.Json;
using TransTracker.Models;

namespace TransTracker;

public partial class Login : ContentPage
{
    public Login()
    {
        InitializeComponent();
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await FetchData();
    }
    public async Task FetchData()
    {
        var apiService = new ApiService();
        string apiUrl = "http://localhost:3002/test";

        var result = await apiService.GetAsync<messageModel>(apiUrl);

        if (result != null && !string.IsNullOrEmpty(result.Message))
        {
            test.Text = result.Message;
        }
    }
    private async void startTracking(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new startTracking());
    }
}
