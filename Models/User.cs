

using cargosiklink.Models.Enum;

namespace cargosiklink.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string UserCode { get; set; }
        public string City { get; set; }
        public Roles Role { get; set; }
        public User(string name, 
                    string password, 
                    string email, 
                    string phone, 
                    string userCode, 
                    string city, 
                    Roles role)
        {
            Id = Guid.NewGuid();
            Name= name;
            Password= password;
            Email= email;
            Phone= phone;
            UserCode= userCode;
            City= city;
            Role = role; 
        }
    }
}
