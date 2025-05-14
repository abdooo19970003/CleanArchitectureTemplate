using CleanArc.Domain.Common;
using CleanArc.Domain.Common.Enums;
using CleanArc.Domain.Events;

namespace CleanArc.Domain.Entities
{
    public class User : Entity
    {
        public User(
            string fname,
            string lname,
            string username,
            string password,
            string avatar = null,
            UserRole role = UserRole.User
            )
        {
            FirstName = fname.Trim();
            LastName = lname.Trim();
            UserName = username.Trim();
            Password = hashPassword(password.Trim());
            Avatar = avatar;
            Role = role;
            this.AddDomainEvent(new UserCreatedDomainEvent(this.Id));
        }
        public User()
        {
            this.AddDomainEvent(new UserCreatedDomainEvent(this.Id));
        }

        private string hashPassword(string password)
        {
            throw new NotImplementedException();
        }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Avatar { get; set; }
        public UserRole Role { get; set; } = UserRole.User;

    }
}
