namespace TransTracker;

public partial class Map : ContentPage
{
	public Map()
	{
		InitializeComponent();
	}

	private void Popup(object sender, EventArgs e)
	{
        TransIdPopup.IsVisible = false;
	}

}