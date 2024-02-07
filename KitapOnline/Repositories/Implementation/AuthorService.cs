using KitapOnline.Models.Domain;
using KitapOnline.Repositories.Abstract;

namespace KitapOnline.Repositories.Implementation
{
    public class AuthorService : IAuthorService
    {
        private readonly ApplicationDbContext context;
        public AuthorService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public bool Add(Author model)
        {
            try
            {
                context.Author.Add(model);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var data = this.FindById(id);
                if (data == null)
                    return false;
                context.Author.Remove(data);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Author FindById(int id)
        {
            return context.Author.Find(id);
        }

        public IEnumerable<Author> GetAll()
        {
            return context.Author.ToList();
        }

        public bool Update(Author model)
        {
            try
            {
                context.Author.Update(model);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        // Yazarın isminin varlığını kontrol etmek için yeni metot
        public bool IsAuthorNameExists(string name)
        {
            return context.Author.Any(a => a.AuthorName == name);
        }
    }
}
