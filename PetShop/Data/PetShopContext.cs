using Microsoft.EntityFrameworkCore;
using PetShop.Models;

namespace PetShop.Data
{
    public class PetShopContext : DbContext //The context class used to communicate with pet shop DB
    {
        public PetShopContext(DbContextOptions<PetShopContext> options) : base(options){}

        //The tables we want to create based on the models
        public DbSet<Animal>? Animals { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Comment>? Comments { get; set; }

        //Here we define how the various tables will be filled on init
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>().HasData(
                new { Id = 1, Name = "Dog", Age = 1, Description = "The dog is a domesticated descendant of the wolf. " +
                                                                    "Also called the domestic dog, it is derived from the extinct Pleistocene wolf," +
                                                                    "and the modern wolf is the dog's nearest living relative", PictureName = "dog.png", CategoryId = 1 },
                new { Id = 2, Name = "Cat", Age = 3, Description = "The cat is a domestic species of small carnivorous mammal." +
                                                                   "It is the only domesticated species in the family Felidae and is " +
                                                                   "commonly referred to as the domestic cat or house cat to distinguish it from the wild members of the family.", PictureName = "cat.png", CategoryId = 1 },
                new { Id = 3, Name = "Hamster", Age = 5, Description = "Hamsters are rodents belonging to the subfamily Cricetinae, which contains 19 species " +
                                                                        "classified in seven genera.They have become established as popular small pets.", PictureName = "hamster.png", CategoryId = 1 },
                new { Id = 4, Name = "Turtle", Age = 7, Description = "Turtles are an order of reptiles known as Testudines, " +
                                                                      "characterized by a special shell developed mainly from their ribs. " +
                                                                      "Modern turtles are divided into two major groups, the Pleurodira (side necked turtles) " +
                                                                      "and Cryptodira (hidden necked turtles), which differ in the way the head retracts. ", PictureName = "turtle.png", CategoryId = 3 },
                new { Id = 5, Name = "Lizard", Age = 2, Description = "Lizards are a widespread group of squamate reptiles, " +
                                                                      "with over 7,000 species,ranging across all continents except Antarctica, as well as most oceanic island chains. " +
                                                                      "The group is paraphyletic since it excludes the snakes and Amphisbaenia although some lizards " +
                                                                      "are more closely related to these two excluded groups than they are to other lizards.", PictureName = "lizard.png", CategoryId = 3 },
                new { Id = 6, Name = "Hedgehog", Age = 2, Description = "A hedgehog is a spiny mammal of the subfamily Erinaceinae, " +
                                                                        "in the eulipotyphlan family Erinaceidae. There are seventeen species of hedgehog in five genera found " +
                                                                        "throughout parts of Europe, Asia, and Africa, and in New Zealand by introduction. There are no hedgehogs native to Australia and no living species native to the Americas. ", PictureName = "Hedgehog.png", CategoryId = 1 },
                new { Id = 7, Name = "Goldfish", Age = 2, Description = "The goldfish (Carassius auratus) is a freshwater fish in the family Cyprinidae of order Cypriniformes. " +
                                                                        "It is commonly kept as a pet in indoor aquariums, and is one of the most popular aquarium fish. Goldfish released into the wild have become an invasive " +
                                                                        "pest in parts of North America.", PictureName = "goldfish.png", CategoryId = 2 }
            );

            modelBuilder.Entity<Category>().HasData(
                new { Id = 1, Name = "Mammel" },
                new { Id = 2, Name = "Aquatic" },
                new { Id = 3, Name = "Reptile" },
                new { Id = 4, Name = "Avian" }
                );
            modelBuilder.Entity<Comment>().HasData(
                new { Id = 1, Content = "Wow What a cute dog!", AnimalId = 1 },
                new { Id = 2, Content = "Wow cute doggo!", AnimalId = 1 },
                new { Id = 3, Content = "Looks Scary!", AnimalId = 1 },
                new { Id = 4, Content = "...", AnimalId = 1 },
                new { Id = 5, Content = "Kitte!!!!!!", AnimalId = 2 },
                new { Id = 6, Content = "Meow", AnimalId = 2 },
                new { Id = 7, Content = "I luv Turtlesss", AnimalId = 4 },
                new { Id = 8, Content = "Lizards are not cute :(", AnimalId = 5 }
                );
        }
    }
}
