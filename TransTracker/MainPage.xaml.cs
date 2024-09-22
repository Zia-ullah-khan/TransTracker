namespace TransTracker
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private async void login(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Login());
        }
        private async void withoutLogin(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AskForTransID());
        }
    }
}
