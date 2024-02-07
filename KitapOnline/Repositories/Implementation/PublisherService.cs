using KitapOnline.Models.Domain;
using KitapOnline.Repositories.Abstract;

namespace KitapOnline.Repositories.Implementation
{
    public class PublisherService : IPublisherService
    {
        private readonly ApplicationDbContext context;
        public PublisherService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public bool Add(Publisher model)
        {
            try
            {
                context.Publisher.Add(model);
                context.SaveChanges();
                return true;
            }
            catch(Exception ex)
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
                context.Publisher.Remove(data);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Publisher FindById(int id)
        {
            return context.Publisher.Find(id);
        }

        public IEnumerable<Publisher> GetAll()
        {
            return context.Publisher.ToList();
        }

        public bool Update(Publisher model)
        {
            try
            {
                context.Publisher.Update(model);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        // Yayımcının isminin varlığını kontrol etmek için yeni metot
        public bool IsPublisherNameExists(string name)
        {
            return context.Publisher.Any(p => p.PublisherName == name);
        }

    }
}
