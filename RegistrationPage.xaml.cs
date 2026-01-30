
using Thusong_Tutors;
namespace Thusong_Tutors;

public partial class RegistrationPage : ContentPage
{
	private DatabaseService _dbService;
	public RegistrationPage()
	{
		InitializeComponent();
		_dbService = new DatabaseService();
	}

	private void OnUniversity_Clicked (object sender, EventArgs e)
	{

	}

    private async void RegisterOn_Clicked(object sender, EventArgs e)
    {
	//	PasswordHelper passwordHelper = new PasswordHelper();

		
		string name = txtName.Text;
		string surname = txtSurname.Text;
		string email = txtEmail.Text; 
		string con_pass = txtConfirmPass.Text;
		string pass = txtCreatePass.Text;


		if ( string.IsNullOrEmpty(name)|| string.IsNullOrEmpty(surname) || string.IsNullOrEmpty(pass)|| (UniPicker.SelectedIndex == -1))
		{
            await DisplayAlert("Error ", "Please fill in all fields ", "ok");
			return; 
        }
		if (con_pass!=pass)
		{
			await DisplayAlert("Error", "Passwords must be the same", "OK");
			return;
		}


		string hash = PasswordHelper.HashPassword(pass); // hashing 
		// creating the id 
	
		
        await _dbService.AddUser(name, surname, email, hash);
		await DisplayAlert("Successfully Registered", "You are now a registered student with Thusong Tutors ", "OK");
		// clear form 
		txtConfirmPass.Text = "";
		txtCreatePass.Text = "";
		txtEmail.Text = "";
		txtName.Text = "";
		txtSurname.Text = "";
		UniPicker.SelectedIndex = -1;

		await Navigation.PushAsync(new MainPage());





    }
}