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
        string apiUrl = "http://71.163.166.96:3002/test";

        var result = await apiService.GetAsync<TestModel>(apiUrl);

        if (result != null && !string.IsNullOrEmpty(result.Message))
        {
            test.Text = result.Message;
        }
    }
}
