using PetShop.Models;

namespace PetShop.Services
{
    public interface IPhotoService
    {
        public string GetPicName(Animal animal, IWebHostEnvironment env);

    }
}
