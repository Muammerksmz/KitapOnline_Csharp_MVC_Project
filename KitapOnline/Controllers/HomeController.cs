// HomeController.cs
using KitapOnline.Models;
using KitapOnline.Models.ViewModels;
using KitapOnline.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;

namespace KitapOnline.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBookService _bookService;

        public HomeController(ILogger<HomeController> logger, IBookService bookService)
        {
            _logger = logger;
            _bookService = bookService;
        }

        public IActionResult Index()
        {
            var books = _bookService.GetAll();

            var viewModelList = new List<HomePageBookViewModel>();
            foreach (var book in books)
            {
                var viewModel = new HomePageBookViewModel
                {
                    Id = book.Id,
                    Title = book.Title,
                    AuthorName = book.AuthorName,
                    PubhlisherName = book.PublisherName,
                    GenreName = book.GenreName
                };
                viewModelList.Add(viewModel);
            }

            if (User.Identity.IsAuthenticated)
            {
                ViewBag.user = User.Identity.Name;
            }
                
            return View(viewModelList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
