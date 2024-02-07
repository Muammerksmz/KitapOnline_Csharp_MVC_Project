using System.ComponentModel.DataAnnotations;

namespace KitapOnline.Models.Domain
{
    public class Publisher
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Lütfen yayımcı adını giriniz.")]
        public string PublisherName { get; set; }
    }
}
