using Microsoft.EntityFrameworkCore;

namespace wet_api.Data
{
    [Index(nameof(Email), IsUnique = true)]
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
    }
}
