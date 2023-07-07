using Microsoft.EntityFrameworkCore;
using PetShop.Data;
using PetShop.Models;

namespace PetShop.Repositories
{
    public class PetShopRepository : IRepository
    {
        private PetShopContext _context;

        public PetShopRepository(PetShopContext context)
        {
            _context = context;
        }

        //Create Operations
        public void CreateAnimal(Animal animal)
        {
            _context.Animals!.Add(animal);
            _context.SaveChanges();
        }


        //Read Operations
        public IQueryable<Animal> GetAnimals()
        {
            return _context.Animals!.Include(a => a.Category).Include(a => a.Comments);
        }
        public IQueryable<Animal> GetTop2Animals()
        {
            var top2 = _context.Animals!.Include(a => a.Category).Include(a => a.Comments).OrderByDescending(a => a.Comments!.Count()).Take(2);
            return top2;
        }
        public IQueryable<Category> GetCategories()
        {
            return _context.Categories!;
        }
        public IQueryable<Comment> GetComments()
        {
            return _context.Comments!;
        }
        public void CreateComment(Comment newComment)
        {
                _context.Add(newComment);
                _context.SaveChanges();
        }


        //Update Operations
        public void UpdateAnimal(Animal animal)
        {
            _context.Animals!.Attach(animal);
            _context.Entry(animal).State= EntityState.Modified;
            _context.SaveChanges();
        }


        //Delete Operations
        public void DeleteAnimal(Animal animal)
        {
            _context.Animals!.Remove(animal);
            _context.SaveChanges();
        }
    }
}
