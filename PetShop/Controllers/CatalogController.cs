using Microsoft.AspNetCore.Mvc;
using PetShop.Models;
using PetShop.Repositories;
using PetShop.Services;

namespace WebApplication1.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IRepository _repository;
        private readonly ISelectListService _selectListService;
        public CatalogController(IRepository repository, ISelectListService selectListService)
        {
            _repository = repository;
            _selectListService = selectListService;
        }
        //This action shows the main unfiltered page of the catalog section
        //It will bring up a view that will show an catalog table with all animals from db
        public IActionResult Index()
        {
            ViewBag.categoryList = _selectListService.GetCategoryListForFilter(_repository.GetCategories().ToList());
            return View(_repository.GetAnimals().ToList());
        }

        //This action shows filtered page of the catalog section
        //It will bring up a view that will show an catalog table with animals of only the selected category from db
        //If all is selected it will redirect to the unfiltered action 
        [HttpPost]
        public IActionResult Index(string categoryName)
        {
            ViewBag.categoryList = _selectListService.GetCategoryListForFilter(_repository.GetCategories().ToList());

            bool isNumber = int.TryParse(categoryName, out int categoryNumber);

            if (string.IsNullOrWhiteSpace(categoryName) || categoryName == "All" || !isNumber)
                return RedirectToAction("Index");
            else
                    return View(_repository.GetAnimals().Where(x => x.CategoryId == categoryNumber).ToList());

        }

        //Brings up the details page for the selected animal
        //If attempting to access the page with an invalid id will be redirected to error page
        public IActionResult Details(int id)
        {
            var selectedAnimal = _repository.GetAnimals().FirstOrDefault(a => a.Id == id);

            if (selectedAnimal == null)
                return RedirectToAction("Error","Home", new {msg = "Attempted to view details of an Animal that Does not exist!"});

            return View(selectedAnimal);
        }

        //This action send a newly added comment by the user form the selected animal
        //Checks if the added comment is not empty if not invokes repo's add comment method
        //Redirects user back to the details page of the given animal id via Details action
        [HttpPost]
        public IActionResult AddComment(string Comment, int id)
        {

            if (!string.IsNullOrWhiteSpace(Comment))
            {
                Comment newComment = new(){Content = Comment,AnimalId = id};
                _repository.CreateComment(newComment);
            }

            return RedirectToAction("Details", new { Id = id });

        }
    }
}
