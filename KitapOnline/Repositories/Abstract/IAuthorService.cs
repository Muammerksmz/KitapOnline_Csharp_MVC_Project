using KitapOnline.Models.Domain;

namespace KitapOnline.Repositories.Abstract
{
    public interface IAuthorService
    {
        bool Add(Author model);
        bool Update(Author model);
        bool Delete(int id);
        Author FindById(int id);
        IEnumerable<Author> GetAll();

        // Yazarın isminin varlığını kontrol etmek için yeni metot
        bool IsAuthorNameExists(string name);
    }
}
