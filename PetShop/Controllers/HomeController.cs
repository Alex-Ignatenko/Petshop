using Microsoft.AspNetCore.Mvc;
using PetShop.Models;
using PetShop.Repositories;

namespace PetShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository _repository;

        public HomeController(IRepository repository)
        {
            _repository = repository;
        }
        //Shows the home page with the two most commented upon animals
        public IActionResult Index()
        {
            return View(_repository.GetTop2Animals().ToList());
        }

        //Shows the error page with the corresponding error messege
        //if one was not provided a generic error messege will be used
        public IActionResult Error(string msg="")
        {
            ErrorModel err = new ErrorModel(" ");

            if (string.IsNullOrWhiteSpace(msg))
                err.ErrorMsg = "An Unknown Error Has Accored!";
            else
                err.ErrorMsg = msg;

            return View(err);
        }
    }
}
