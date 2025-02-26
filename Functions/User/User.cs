using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Voxerra_API.Functions.User
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; }/* = null!;*/
        public string AvatarSourceName { get; set; }
        public string AvatarVersion { get; set; }
        public bool IsOnline { get; set; } 
        public DateTime LastLogonTime { get; set; }
        public string CreationYear { get; set; }
        public string Token { get; set; } = null!;
    }
}
