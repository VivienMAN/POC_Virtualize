using Blazorise;
using Bogus;
using static Bogus.DataSets.Name;

namespace BlazorApp5.Data
{
    public class User
    {
        public User(int userId, string ssn)
        {
            this.Id = userId;
            this.SSN = ssn;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string SomethingUnique { get; set; }
        public Guid SomeGuid { get; set; }

        public string Avatar { get; set; }
        public Guid CartId { get; set; }
        public string SSN { get; set; }
        public string imageBase64 { get; set; }
        public Gender Gender { get; set; }




        public static List<User> getUsers(int n, int u)
        {
            var userIds = u;

            var testUsers = new Faker<User>()
            //Optional: Call for objects that have complex initialization
            .CustomInstantiator(f => new User(userIds++, f.Random.Replace("###-##-####")))

            //Basic rules using built-in generators
            .RuleFor(u => u.FirstName, f => f.Name.FirstName())
            .RuleFor(u => u.LastName, f => f.Name.LastName())
            .RuleFor(u => u.Avatar, f => f.Internet.Avatar())
            .RuleFor(u => u.UserName, (f, u) => f.Internet.UserName(u.FirstName, u.LastName))
            .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
            .RuleFor(u => u.SomethingUnique, f => $"Value {f.UniqueIndex}")
            .RuleFor(u => u.SomeGuid, Guid.NewGuid)

            //Use an enum outside scope.
            .RuleFor(u => u.Gender, f => f.PickRandom<Gender>())
            //Use a method outside scope.
            .RuleFor(u => u.CartId, f => Guid.NewGuid())
            //Compound property with context, use the first/last name properties
            .RuleFor(u => u.FullName, (f, u) => u.FirstName + " " + u.LastName)
            .RuleFor(u => u.imageBase64, f => f.Image.PicsumUrl(500, 500));






            var user = testUsers.Generate(n);


            return user;
        }

    }
}
