using Microsoft.AspNetCore.Mvc;
using PetShop.Models;
using PetShop.Repositories;
using PetShop.Services;


namespace PetShop.Controllers
{
    public class AdminController : Controller
    {
        private readonly IRepository _repository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IPhotoService _photoService;
        private readonly ISelectListService _selectListService;
        public AdminController(IRepository repository, IWebHostEnvironment webHostEnvironment, IPhotoService photoService, ISelectListService selectListService)
        {
            _repository = repository;
            _webHostEnvironment = webHostEnvironment;
            _photoService = photoService;
            _selectListService = selectListService;
        }
        //This action shows the main unfiltered page of the admin section
        //It will bring up a view that will show an admin table with all animals from db
        [HttpGet]
        public IActionResult Index() //this action shows the main unfiltered page of the admin section
        {
            ViewBag.categoryList = _selectListService.GetCategoryListForFilter(_repository.GetCategories().ToList());
            return View(_repository.GetAnimals().ToList());
        }

        //This action shows filtered page of the admin section
        //It will bring up a view that will show an admin table with animals of only the selected category from db
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

        //This action will show the add animal view 
        //Creates an empty instance of animal class to be filled by the user and sent via its post counterpart back to the server
        [HttpGet]
        public IActionResult AddAnimal()
        {
            ViewBag.categoryList = _selectListService.GetCategoryList(_repository.GetCategories().ToList());     
            Animal newAnimal = new Animal();
            return View(newAnimal);
        }

        //This action attempts to send back the newly created animal as an entery to the database
        //It will check the validity of the model if the user filled all the inputs and filled them correctly it will activate the repo's create animal method and redirect back to index page of admin section
        //It will copy the name of the newly added image into the new animal entity pictureName to know which picture the new animal is linked to via a service
        //If the model is not valid the user will stay in the add page and be presented with the coresponding error messeges
        [HttpPost]
        public IActionResult AddAnimal(Animal newAnimal)
        {

            ViewBag.categoryList = _selectListService.GetCategoryList(_repository.GetCategories().ToList());

            if (ModelState.IsValid)
            {
                string picName = _photoService.GetPicName(newAnimal, _webHostEnvironment);
                newAnimal.PictureName = picName;
                _repository.CreateAnimal(newAnimal);
                return RedirectToAction("Index");
            }
            else
                return View("AddAnimal");

        }

        //This action will show the edit animal view 
        //Retrieves the specified animal from db to bew modefied by the user and sent via its post counterpart back to the server
        //If the user attempts access to this page with an invalid id he will be redirected to an error page
        [HttpGet]
        public IActionResult EditAnimal(int id)
        {
            var selectedAnimal = _repository.GetAnimals().FirstOrDefault(x => x.Id == id);

            if (selectedAnimal == null)
                return RedirectToAction("Error", "Home", new { msg = "Attempted to Edit an Animal that Does not exist!" });

            ViewBag.categoryList = _selectListService.GetCategoryList(_repository.GetCategories().ToList());

            return View(selectedAnimal);
        }

        //This action attempts to send back the updated animal to the database
        //If the user chnaged the animals picture its not mapped picture property will be filled so we update the mapped pictureName property to link the animal to the new image
        //We remove the validity check on picture property because on edit page it will not be empty, the system does not allow an option of a pictureless animal
        //If the model is valid we activate repo's update animal method and redirect back to the index page of admin section
        //If the model is not valid the user stays at the edit page and showed error messeges
        //If the user attempts access to this page with an invalid id he will be redirected to an error page
        [HttpPost]
        public IActionResult EditAnimal(Animal selectedAnimal)
        {
            ViewBag.categoryList = _selectListService.GetCategoryList(_repository.GetCategories().ToList());

            if (selectedAnimal == null)
                return RedirectToAction("Error", "Home", new { msg = "Attempted to add a comment to an Animal that Does not exist!" });

            if (selectedAnimal.Picture != null)
            {
                string picName = _photoService.GetPicName(selectedAnimal, _webHostEnvironment);
                selectedAnimal.PictureName = picName;
            }

            ModelState.Remove("Picture");
            if (ModelState.IsValid)
            {
                _repository.UpdateAnimal(selectedAnimal);
                return RedirectToAction("Index");
            }
            else
                return View("EditAnimal", selectedAnimal);

        }

        //Shows the delete page of a selected animal
        //If the page is accessed with an invalid id will be redirected to error page
        [HttpGet]
        public IActionResult DeleteAnimal(int id)
        {
            var animal = _repository.GetAnimals().FirstOrDefault(x => x.Id == id);

            if (animal == null)
                return RedirectToAction("Error", "Home", new { msg = "Attempted to Delete an Animal that Does not exist!" });

            return View(animal);
        }

        //Invokes repo's delete method on the given animal and redirects back to the index page of admin section
        [HttpPost]
        public IActionResult DeleteAnimal(Animal animal)
        {
            _repository.DeleteAnimal(animal);
            return RedirectToAction("Index");
        }
    }
}
