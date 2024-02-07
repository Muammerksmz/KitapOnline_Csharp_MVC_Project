using KitapOnline.Models.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KitapOnline.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class AdminController : Controller
    {
        [Authorize(Roles = $"{ApplicationRoles.Admin}")]
        public IActionResult Index()
        {
            return View();
        }
    }
}