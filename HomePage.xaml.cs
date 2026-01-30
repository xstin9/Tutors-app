using System.Threading.Tasks;

namespace Thusong_Tutors;

public partial class HomePage : ContentPage
{
	//bool _menuVisible = false;
	public HomePage()
	{
		InitializeComponent();
	
	}

   

    private async void BtnBookApp_Clicked(object sender, EventArgs e)
    {
		await Navigation.PushAsync(new BookingPage());

    }

    

}