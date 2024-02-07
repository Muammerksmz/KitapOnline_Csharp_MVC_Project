using Microsoft.AspNetCore.Mvc;
using KitapOnline.Models.Domain;
using KitapOnline.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;

namespace KitapOnline.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PublisherController : Controller
    {
        private readonly IPublisherService service;
        public PublisherController(IPublisherService service)
        {
            this.service = service;
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = $"{ApplicationRoles.Admin}")]
        public IActionResult Add(Publisher model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Yayımcının aynı isimde kayıtlı olup olmadığını kontrol et
            if (service.IsPublisherNameExists(model.PublisherName))
            {
                TempData["msg"] = "Bu yayımcı zaten kayıtlı.";
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
        public IActionResult Update(Publisher model)
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
