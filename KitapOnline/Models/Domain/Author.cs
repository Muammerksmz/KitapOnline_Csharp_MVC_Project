using System.ComponentModel.DataAnnotations;

namespace KitapOnline.Models.Domain
{
    public class Author
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Lütfen yazar adını giriniz.")]
        public string AuthorName { get; set; }
    }
}
