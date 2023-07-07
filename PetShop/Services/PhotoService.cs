using PetShop.Models;

namespace PetShop.Services
{
    public class PhotoService : IPhotoService
    {
        //Service used to update a given animals pictureName property with a newly added picture as well as upload the picture to img folder of wwwroot
        //We get the path of wwwroot via the IWebHostEnvironment instance combine it with the pictures name to form the file path
        //We create a new file in that location and copy it to animals picture property
        //Finally we return the new pictures name to link the animal with its new picture
        public string GetPicName(Animal animal, IWebHostEnvironment env)
        {
            string? picName = null;

            if (animal.Picture != null)
            {
                string uploadFolder = Path.Combine(env.WebRootPath, "img");
                picName = animal.Picture.FileName;
                string filePath = Path.Combine(uploadFolder, picName);
                using (var fileStream = new FileStream(filePath, FileMode.Create)) { animal.Picture.CopyTo(fileStream); }
            }
            return picName!;
        }
    }



}
