using PetShop.Models;

namespace PetShop.Repositories
{
    public interface IRepository
    {
        IQueryable<Animal> GetAnimals();
        IQueryable<Animal> GetTop2Animals();
        void CreateAnimal(Animal animal);
        void UpdateAnimal(Animal animal);
        void DeleteAnimal(Animal animal);

        IQueryable<Category> GetCategories();
        IQueryable<Comment> GetComments();
        void CreateComment(Comment newComment);
    }
}
