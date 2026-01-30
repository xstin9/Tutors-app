using Database;
using Microsoft.Maui;
using Microsoft.Maui.ApplicationModel.Communication;
using Microsoft.Maui.Controls.PlatformConfiguration;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml.Linq;



namespace Thusong_Tutors;


public partial class BookingPage : ContentPage
{
    private DatabaseService _dbService;
    private Appointment Booking = null;
    int userId = Preferences.Get("UserId", 0);
    public BookingPage()
	{
		InitializeComponent();
        InitAsync(); // offload heavy work
        _dbService = new DatabaseService();

    }
    protected async override void OnAppearing()
    {
       
        base.OnAppearing();

        Appointment _booking = await _dbService.GetClientID(Preferences.Get("UserId", 0)); // checks if on the system via user ID
        if (_booking == null)
        {
            await DisplayAlert("Complete Booking", "Book your appointement with success NOW!", "OK");
            BookingLayout.IsVisible = true;
            ConfirmLayout.IsVisible = false;

        }
        else
        {
            BookingLayout.IsVisible= false;
            ConfirmLayout.IsVisible= true;
        }


        



            
    }

    private async void InitAsync()
	{
        try
        {
            // Delay for simulation
            await Task.Delay(500);

            // Simulate loading options
            var modules = new List<string> { "CMPG111", "CMPG212", "CMPG324", "COS110", "COS132", "CSC1015F", "CSC2005F", "CSC3002S", "COMS2001A", "COS226", "COMS1016A", "IFM1A10", "IFM1B10", "IFM2A10" };

            ModulePicker.ItemsSource = modules;

            myDatePicker.MinimumDate = DateTime.Today;
            myDatePicker.MaximumDate = DateTime.Today.AddDays(14);

            
            
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }

        
    }

    private void MyDatePicker_DateSelected(object sender, DateChangedEventArgs e)
    {
        DateTime selectedDate = e.NewDate;
        DateTime eventDate = selectedDate.AddDays(2);
        DayOfWeek eventDay = eventDate.DayOfWeek;

        if (eventDay == DayOfWeek.Saturday || eventDay == DayOfWeek.Sunday)
        {
            DisplayAlert("Invalid Booking", "The event will fall on a weekend. Please select an earlier date.", "OK");

            // Revert to the previous date
            myDatePicker.Date = e.OldDate;
        }

    }

    private async void OnTutor_SelectedIndex(object sender, EventArgs e)
    {
        
        


    }
    private async void BookOn_Clicked(object sender, EventArgs e)
    {
        // also delete the appointment,add , change 
        //and display the date confirmed 

        if ((TutorsList.SelectedIndex == -1))
        {
            await DisplayAlert("Error ", "Please fill in all fields ", "ok");

            return;
        }
        string name = TutorsList.SelectedItem.ToString();
        string module = ModulePicker.SelectedItem.ToString();
        DateTime date = myDatePicker.Date.AddDays(2);
        string timeslot = myTimeSlotPicker.SelectedItem.ToString();
        string dateDisplayed = myDatePicker.Date.AddDays(2).ToString("dd/MMM/yyyy");
        int book_ID = 0;

        for (int i = 0; i < 100000; i++)
        {
             book_ID = i;
        }
        await _dbService.AddBooking(name, module, date, Preferences.Get("UserId", 0));
        
        BookingLayout.IsVisible = false;
       ConfirmLayout.IsVisible = true;
      await _dbService.GetAllAsync();


        // use id to get the name
        // 

        string userName = Preferences.Get("UserName", "Guest");
        
        


        await DisplayAlert("Successfully Booked", " Hello "+userName+" you have successfully booked a tutor session with " +name+" \n Scheduled for "+dateDisplayed+" at "+timeslot+"\nFor the module "+ module + "\n We email will contact you soon to confirm the venue"+"\nThank you for booking with Thusong!", "OK");
        // Appointment user = await _dbService.GetUserID();

        // update version 

        // if they want to change their date or cancel 
       

        // cancel 
        
        // get the name of tutor and store it 
    }

    private async void Btnbacktologin_Clicked(object sender, EventArgs e)
    {
      
        await Navigation.PushAsync(new MainPage());
    }

    private async void BtnView_Clicked(object sender, EventArgs e)
    {

        string email = Preferences.Get("Email", null);

        if (string.IsNullOrEmpty(email))
        {
            await DisplayAlert("Error", "No email found in preferences.", "OK");
            return;
        }

        Appointment booking = await _dbService.GetClientID(Preferences.Get("UserId", 0));

        if (booking == null)
        {
            await DisplayAlert("No Booking", "No booking found for this email.", "OK");
            return;
        }

        // Display in label
        myBooking.Text = $"Tutor: {booking.Tutor_name}\nModule: {booking.Module}\nDate: {booking.Date:dddd, dd MMM yyyy}";


    }
}