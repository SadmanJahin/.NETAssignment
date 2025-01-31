using Bogus;
using Microsoft.EntityFrameworkCore;
using Users.Core.Entities;
using Users.Core.Enums;
using Users.DataAccess.Data;

public class Program
{
    public static void Main(string[] args)
    {
        var connectionString = "Server=Sadman;Database=UserDB;Trusted_Connection=True;TrustServerCertificate=True;";

        var dbContext = GetDBContext(connectionString);

        var users = GetFakeUsers(500);

        dbContext.Users.AddRangeAsync(users);
        dbContext.SaveChanges();
    }


    private static DatabaseContext GetDBContext(string connectionString)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
        optionsBuilder.UseSqlServer(connectionString);
        var options = optionsBuilder.Options;
        var context = new DatabaseContext(options);

        return context;
    }

    private static List<User> GetFakeUsers(int count)
    {
        var userFaker = new Faker<User>()
          .RuleFor(u => u.FirstName, f => f.Name.FirstName())  
          .RuleFor(u => u.LastName, f => f.Name.LastName()) 
          .RuleFor(u => u.Company, f => f.Company.CompanyName())  
          .RuleFor(u => u.Gender, f => f.PickRandom<Gender>())  
          .RuleFor(u => u.Active, f => f.Random.Bool());  

        var contactFaker = new Faker<Contact>()
            .RuleFor(c => c.Phone, f => f.Phone.PhoneNumber("###########"))  
            .RuleFor(c => c.City, f => f.Address.City())  
            .RuleFor(c => c.Address, f => f.Address.FullAddress())  
            .RuleFor(c => c.Country, f => f.Address.Country());  

        var roleFaker = new Faker<Role>()
           .RuleFor(c => c.Name, f => f.PickRandom(new[] { "Admin", "Manager", "User", "Editor", "Viewer" }));

        var users = new List<User>();

     
        for (int i = 0; i < count; i++)
        {
            var user = userFaker.Generate();
            var contact = contactFaker.Generate();
            var role = roleFaker.Generate();

            user.Contact = contact;
            user.Role = role;

            users.Add(user);
        }

        return users;
    }
}