using System.ComponentModel.DataAnnotations;

namespace SafeCam.ViewModels
{
    public class LoginVM
    {
        [Required]
        [MaxLength(20)]
        public string UserNameorEmailAddress{ get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
