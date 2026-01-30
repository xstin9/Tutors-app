using SQLite;
using System.IO;
using System.Collections.Generic;
using Database;
using System.Threading.Tasks;
using Thusong_Tutors;

public class DatabaseService
{
    private SQLiteAsyncConnection _database;

    public DatabaseService()
    {
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "ClientsTable.db");
        _database = new SQLiteAsyncConnection(dbPath);
        _database.CreateTableAsync<User>().Wait();
        _database.CreateTableAsync<Appointment>().Wait();
    }

    public async Task AddUser(string name, string surname, string email, string hash )
    {
        
        var user = new User { Name = name, Surname = surname, Email= email, HashedPassword= hash};
        await _database.InsertAsync(user);
    }
    public async Task AddBooking(string tutor_name, string module, DateTime date,int user_ID)
    {
        var booking = new Appointment { Tutor_name= tutor_name, Module = module, Date=date,UserID=user_ID };
        await _database.InsertAsync(booking);
    }
    public async Task<int> UpdateUser(User user)
    {
        try
        {
            return await _database.UpdateAsync(user);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating user: {ex.Message}");
            return 0;
        }
    }
    public async Task<int> UpdateBooking(Appointment booking)
    {
        try
        {
            return await _database.UpdateAsync(booking);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating user: {ex.Message}");
            return 0;
        }
    }
    public async Task<int> DeleteBooking(Appointment booking)
    {
        try
        {
            return await _database.DeleteAsync(booking);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting appointment: {ex.Message}");
            return 0;
        }
    }



    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _database.Table<User>().ToListAsync();
        
    }
    public async Task<List<Appointment>> GetAllAsync() // display full datebase 
    {
        return await _database.Table<Appointment>().ToListAsync();

    }

    public async Task<User> GetbyEmail(String email)
    {
        return await _database.Table<User>().Where(x=>x.Email.Contains(email)).FirstOrDefaultAsync();
    }
    public async Task<User> GetUserID(int id)
    {
        return await _database.Table<User>().Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Appointment> GetClientID(int id)
    {
        return await _database.Table<Appointment>().Where(x => x.UserID == id).FirstOrDefaultAsync();
    }
   

    public async Task<User> GetbyHash(String hash)
    {
        return await _database.Table<User>().Where(x => x.HashedPassword.Contains(hash)).FirstOrDefaultAsync();
    }

    //public async Task<List<User>> GetbyName(String name)
    //{
    //    return await _database.Table<User>().Where(x => x.Name.Contains(name)).ToListAsync();
    //}


}

