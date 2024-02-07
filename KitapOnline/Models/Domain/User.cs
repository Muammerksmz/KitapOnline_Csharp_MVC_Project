using System.ComponentModel.DataAnnotations;

namespace KitapOnline.Models.Domain
{
    public enum Role
    {
        Admin,
        Normal,
    }
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Lütfen kullanıcı adını giriniz.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Lütfen şifrenizi giriniz.")]
        public string Password{ get; set; }

        public Role Role { get; set; }
       
    }
}

