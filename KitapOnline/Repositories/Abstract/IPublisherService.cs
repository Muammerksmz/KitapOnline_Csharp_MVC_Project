using KitapOnline.Models.Domain;

namespace KitapOnline.Repositories.Abstract
{
    public interface IPublisherService
    {
        bool Add(Publisher model);
        bool Update(Publisher model);
        bool Delete(int id);
        Publisher FindById(int id);
        IEnumerable<Publisher> GetAll();

        // Yayımcının isminin varlığını kontrol etmek için yeni metot
        bool IsPublisherNameExists(string name);
    }
}
