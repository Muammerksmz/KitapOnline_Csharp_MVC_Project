using Microsoft.AspNetCore.Mvc;
using KitapOnline.Models.Domain;
using KitapOnline.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;

namespace KitapOnline.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GenreController : Controller
    {
        private readonly IGenreService service;
        public GenreController(IGenreService service)
        {
            this.service = service;
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = $"{ApplicationRoles.Admin}")]
        public IActionResult Add(Genre model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Türün aynı isimde kayıtlı olup olmadığını kontrol et
            if (service.IsGenreNameExists(model.Name))
            {
                TempData["msg"] = "Bu tür zaten kayıtlı.";
                return View(model);
            }

            var result = service.Add(model);
            if (result)
            {
                TempData["msg"] = "Başarıyla eklendi";
                return RedirectToAction(nameof(Add));
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

        [HttpPost]
        [Authorize(Roles = $"{ApplicationRoles.Admin}")]
        public IActionResult Update(Genre model)
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
