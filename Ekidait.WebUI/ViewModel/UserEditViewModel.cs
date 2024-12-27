using System.ComponentModel.DataAnnotations;

namespace Ekidait.WebUI.Models
{
    public class UserEditViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Adı")]
        public string Name { get; set; }
        [Display(Name = "Soyadı")]
        public string Surname { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Şifre")]
        public string Password { get; set; }
    }
}
