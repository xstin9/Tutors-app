using Database;

using Thusong_Tutors;

namespace Thusong_Tutors
{
    public partial class MainPage : ContentPage
    {
        private DatabaseService _dbService;

        public MainPage()
        {
            InitializeComponent();

            _dbService = new DatabaseService();
        }

       private async void BtnLogin_Clicked (object sender, EventArgs e)
        {
            string enteredEmail = txtUserName.Text;
            string enteredPassword = txtPassword.Text;

            // Get user by email
            User client = await _dbService.GetbyEmail(enteredEmail);


            if (client == null)
            {
                await DisplayAlert("Error", "User not found.", "OK");
                return;
            }

            try
            {
                // Verify the entered password against the stored hash
                bool isPasswordValid = PasswordHelper.VerifyPassword(enteredPassword, client.HashedPassword);

                if (!isPasswordValid)
                {
                    await DisplayAlert("Error", "Invalid password.", "OK");
                    return;
                }

                // Login success
                await DisplayAlert("Login Successfull!", "Hello " + client.Name, "OK");
                Preferences.Set("UserName", client.Name); // sets the name global
                Preferences.Set("UserId", client.Id);

                //clear entries 
                txtUserName.Text = "";
                txtPassword.Text = "";

                await Navigation.PushAsync(new HomePage());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Something went wrong: " + ex.Message, "OK");
            }
        }
            






           
        

        private async void BtnForgot_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RecoveryPage());
        }

        private async void BtnSignUp_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistrationPage());
        }
    }
}
