using Microsoft.AspNetCore.Mvc;
using KitapOnline.Models.Domain;
using KitapOnline.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;


namespace KitapOnline.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthorController : Controller
    {
        private readonly IAuthorService service;
        public AuthorController(IAuthorService service)
        {
            this.service = service;
        }
        [Authorize(Roles = $"{ApplicationRoles.Admin}")]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize(Roles = $"{ApplicationRoles.Admin}")]
        [HttpPost]
        public IActionResult Add(Author model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Yazarın aynı isimde kayıtlı olup olmadığını kontrol et
            if (service.IsAuthorNameExists(model.AuthorName))
            {
                TempData["msg"] = "Bu yazar zaten kayıtlı.";
                return View(model);
            }

            var result = service.Add(model);
            if (result)
            {
                TempData["msg"] = "Başarıyla eklendi";
                return RedirectToAction();
            }
            TempData["msg"] = "Error has occured on server side";
            return View(model);
        }

        [Authorize(Roles = $"{ApplicationRoles.Admin}")]
        public IActionResult Update(int id)
        {
            var record = service.FindById(id);
            return View(record);
        }
        [Authorize(Roles = $"{ApplicationRoles.Admin}")]
        [HttpPost]
        public IActionResult Update(Author model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = service.Update(model);
            if (result)
            {
                return RedirectToAction("GetAll");
            }
            TempData["msg"] = "Error has occured on server side";
            return View(model);
        }

        [Authorize(Roles = $"{ApplicationRoles.Admin}")]
        public IActionResult Delete(int id)
        {

            var result = service.Delete(id);
            return RedirectToAction("GetAll");
        }
        [Authorize(Roles = $"{ApplicationRoles.Admin}")]
        public IActionResult GetAll()
        {

            var data = service.GetAll();
            return View(data);
        }
    }
}
