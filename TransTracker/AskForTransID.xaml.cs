namespace TransTracker;

public partial class AskForTransID : ContentPage
{
	public AskForTransID()
	{
		InitializeComponent();
	}

    private async void Popup(object sender, EventArgs e)
    {
        string enteredBudId = TransID.Text;
        await Navigation.PushAsync(new Map(enteredBudId));
    }
}