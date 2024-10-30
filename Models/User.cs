using System.ComponentModel.DataAnnotations;

namespace AuthApp.Models
{
    public class User
    {
        public string Uid { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        [Key]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
