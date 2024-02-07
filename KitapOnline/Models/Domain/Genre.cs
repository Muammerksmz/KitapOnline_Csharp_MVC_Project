using System.ComponentModel.DataAnnotations;

namespace KitapOnline.Models.Domain
{
    public class Genre
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Lütfen tür adını giriniz.")]
        public string Name { get; set; }
    }
}
