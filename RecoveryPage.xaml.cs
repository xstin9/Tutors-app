using System.Threading.Tasks;
using Thusong_Tutors;
using Database;

namespace Thusong_Tutors;

public partial class RecoveryPage : ContentPage
{
    private DatabaseService _dbService;
    public RecoveryPage()
	{
		InitializeComponent();
        _dbService = new DatabaseService();
    }
    private User _recoveryUser = null; // Store user temporarily
    private async void OnRecoverPasswordClicked(object sender, EventArgs e)
    {
        string enteredEmail = txtEmail.Text.Trim();
         _recoveryUser = await _dbService.GetbyEmail(enteredEmail);

        if (_recoveryUser == null)
        {
            await DisplayAlert("Error", "User not found.", "OK");
            return;
        }

        // Show the password reset section
        resetLayout.IsVisible = true;
        resetLayout.IsVisible = true;
        txtNewPassword.Text = "";
        txtConfirmPassword.Text = "";

        await DisplayAlert("Found", "User verified. You can now reset your password.", "OK");

    }
    private async void OnResetPasswordClicked(object sender, EventArgs e)
    {
        string newPassword = txtNewPassword.Text;
        string confirmPassword = txtConfirmPassword.Text;

        if (string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(confirmPassword))
        {
            await DisplayAlert("Error", "Please fill in both fields.", "OK");
            return;
        }

        if (newPassword != confirmPassword)
        {
            await DisplayAlert("Error", "Passwords do not match.", "OK");
            return;
        }

        // Hash the new password
        string hashed = PasswordHelper.HashPassword(newPassword);

        // Update in database (you need to implement this method)
        _recoveryUser.HashedPassword = hashed;
        await _dbService.UpdateUser(_recoveryUser); // You must implement this update method

        await DisplayAlert("Successfully Reset", "Password has been reset.", "OK");

        // Optionally reset UI
        resetLayout.IsVisible = false;
        txtNewPassword.Text = string.Empty;
        txtConfirmPassword.Text = string.Empty;
        txtEmail.Text= "";
        await Navigation.PushAsync(new MainPage());

    }
}