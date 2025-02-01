using Bogus;
using Microsoft.EntityFrameworkCore;
using Users.Core.Entities;
using Users.DataAccess.Data;
using Users.DataAccess.Repositories;

public class Program
{
    public static async Task Main(string[] args)
    {
        //var connectionString = "Server=Sadman;Database=UserDB;Trusted_Connection=True;TrustServerCertificate=True;";

        //var dbContext = GetDBContext(connectionString);

        var users = GetFakeUsers(1);

        //dbContext.Users.AddRangeAsync(users);
        //dbContext.SaveChanges();

        //Console.WriteLine("SQL Database SEEDING COMPLETED SUCCESSFULLY");
        // Initialize the repository with a file path

        string filePath = "users.json";
        var jsonContext = new JsonContext(filePath);
        var userRepository = new UserJsonRepository(filePath, jsonContext);
        var unitofwork = new JsonUnitOfWork(jsonContext);
        await userRepository.SaveAsync(GetFakeUser());
        await unitofwork.SaveChangesAsync();
    }


    private static DatabaseContext GetDBContext(string connectionString)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
        optionsBuilder.UseSqlServer(connectionString);
        var options = optionsBuilder.Options;
        var context = new DatabaseContext(options);

        return context;
    }

    private static User GetFakeUser()
    {
        var userFaker = GetUserFakerRule();

        var contactFaker = GetContactFakerRule();

        var roleFaker = GetRoleFakerRule();

        var user = userFaker.Generate();
        var contact = contactFaker.Generate();
        var role = roleFaker.Generate();

        user.Contact = contact;
        user.Role = role;

        return user;
    }

    private static List<User> GetFakeUsers(int count)
    {
        var userFaker = GetUserFakerRule();

        var contactFaker = GetContactFakerRule();

        var roleFaker = GetRoleFakerRule();

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

    private static Faker<User> GetUserFakerRule()
    {
        return new Faker<User>()
          .RuleFor(u => u.FirstName, f => f.Name.FirstName())
          .RuleFor(u => u.LastName, f => f.Name.LastName())
          .RuleFor(u => u.Company, f => f.Company.CompanyName())
          .RuleFor(u => u.Gender, f => f.PickRandom(new[] { "M", "F", "O" }))
          .RuleFor(u => u.Active, f => f.Random.Bool());
    }

    private static Faker<Role> GetRoleFakerRule()
    {
        return new Faker<Role>()
           .RuleFor(c => c.Name, f => f.PickRandom(new[] { "Admin", "Manager", "User", "Editor", "Viewer" }));
    }

    private static Faker<Contact> GetContactFakerRule()
    {
        return new Faker<Contact>()
            .RuleFor(c => c.Phone, f => f.Phone.PhoneNumber("###########"))
            .RuleFor(c => c.City, f => f.Address.City())
            .RuleFor(c => c.Address, f => f.Address.FullAddress())
            .RuleFor(c => c.Country, f => f.Address.Country());
    }
}
