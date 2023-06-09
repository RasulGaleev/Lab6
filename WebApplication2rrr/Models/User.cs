using System.ComponentModel.DataAnnotations;

namespace WebApplication2rrr.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string Name { get; set; }
        public bool Sex { get; set; }
        public string NumberTelephone { get; set; }
    }
}
