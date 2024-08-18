using IcecreamMAUI.API.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace IcecreamMAUI.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Icecream> Icecreams { get; set; }
        public DbSet<IcecreamOption> IcecreamOptions { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IcecreamOption>()
                        .HasKey(io => new { io.IcecreamId, io.Flavour, io.Topping });

            AddSeedData(modelBuilder);
        }

        //add seeding data for our model
        private static void AddSeedData(ModelBuilder modelBuilder)
        {
            Icecream[] icecreams = [
                new Icecream { Id = 1, Name = "Vanilla Delight", Image = "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_0.jpg", Price = 6.25 },
                new Icecream { Id = 2, Name = "ChocoBerry Bliss", Image = "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_1.jpg", Price = 7.5 },
                new Icecream { Id = 3, Name = "Minty Cookie Crunch", Image = "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_2.jpg", Price = 8.75 },
                new Icecream { Id = 4, Name = "Strawberry Dream", Image = "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_3.jpg", Price = 6.95 },
                new Icecream { Id = 5, Name = "Cookie Dough Extravaganza", Image = "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_4.jpg", Price = 9.2 },
                new Icecream { Id = 6, Name = "Caramel Swirl Symphony", Image = "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_5.jpg", Price = 7.75 },
                new Icecream { Id = 7, Name = "Peanut Butter Paradise", Image = "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_6.jpg", Price = 8.5 },
                new Icecream { Id = 8, Name = "Mango Tango Tango", Image = "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_7.jpg", Price = 9.8 },
                new Icecream { Id = 9, Name = "Hazelnut Heaven", Image = "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_8.jpg", Price = 8.95 },
                new Icecream { Id = 10, Name = "Blueberry Bliss Bonanza", Image = "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_9.jpg", Price = 7.2 },
                new Icecream { Id = 11, Name = "Toffee Twist Temptation", Image = "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_10.jpg", Price = 7.95 },
                new Icecream { Id = 12, Name = "Rocky Road Revelry", Image = "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_11.jpg", Price = 9.5 },
                new Icecream { Id = 13, Name = "Passionfruit Paradise", Image = "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_12.jpg", Price = 8.75 },
                new Icecream { Id = 14, Name = "Cotton Candy Carnival", Image = "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_13.jpg", Price = 7.2 },
                new Icecream { Id = 15, Name = "Lemon Sorbet Serenity", Image = "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_14.jpg", Price = 6.9 },
                new Icecream { Id = 16, Name = "Maple Pecan Pleasure", Image = "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_15.jpg", Price = 8.25 },
                new Icecream { Id = 17, Name = "Raspberry Ripple Rhapsody", Image = "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_16.jpg", Price = 7.45 },
                new Icecream { Id = 18, Name = "Almond Joyful Jubilee", Image = "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_17.jpg", Price = 9.95 },
                new Icecream { Id = 19, Name = "Blue Lagoon Bliss", Image = "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_18.jpg", Price = 8.5 },
                new Icecream { Id = 20, Name = "Coconut Caramel Carnival", Image = "https://raw.githubusercontent.com/Abhayprince/Images-Icons/main/Icecreams/small/ic_19.jpg", Price = 7.8 }
            ];

            IcecreamOption[] icecreamOptions = [
                new IcecreamOption { IcecreamId = 1, Flavour = "Vanilla", Topping = "Default" },
                new IcecreamOption { IcecreamId = 1, Flavour = "Default", Topping = "Chocolate Sauce" },
                new IcecreamOption { IcecreamId = 1, Flavour = "Default", Topping = "Whipped Cream" },
                new IcecreamOption { IcecreamId = 2, Flavour = "Chocolate", Topping = "Default" },
                new IcecreamOption { IcecreamId = 2, Flavour = "Strawberry", Topping = "Default" },
                new IcecreamOption { IcecreamId = 2, Flavour = "Default", Topping = "Sprinkles" },
                new IcecreamOption { IcecreamId = 3, Flavour = "Mint Chocolate Chip", Topping = "Default" },
                new IcecreamOption { IcecreamId = 3, Flavour = "Default", Topping = "Cherries" },
                new IcecreamOption { IcecreamId = 3, Flavour = "Default", Topping = "Chocolate Sauce" },
                new IcecreamOption { IcecreamId = 4, Flavour = "Strawberry", Topping = "Default" },
                new IcecreamOption { IcecreamId = 4, Flavour = "Default", Topping = "Sprinkles" },
                new IcecreamOption { IcecreamId = 4, Flavour = "Default", Topping = "Whipped Cream" },
                new IcecreamOption { IcecreamId = 5, Flavour = "Cookie Dough", Topping = "Default" },
                new IcecreamOption { IcecreamId = 5, Flavour = "Default", Topping = "Chocolate Sauce" },
                new IcecreamOption { IcecreamId = 5, Flavour = "Default", Topping = "Cherries" },
                new IcecreamOption { IcecreamId = 6, Flavour = "Vanilla", Topping = "Default" },
                new IcecreamOption { IcecreamId = 6, Flavour = "Default", Topping = "Chocolate Sauce" },
                new IcecreamOption { IcecreamId = 6, Flavour = "Default", Topping = "Cherries" },
                new IcecreamOption { IcecreamId = 7, Flavour = "Chocolate", Topping = "Default" },
                new IcecreamOption { IcecreamId = 7, Flavour = "Default", Topping = "Whipped Cream" },
                new IcecreamOption { IcecreamId = 7, Flavour = "Default", Topping = "Sprinkles" },
                new IcecreamOption { IcecreamId = 8, Flavour = "Strawberry", Topping = "Default" },
                new IcecreamOption { IcecreamId = 8, Flavour = "Cookie Dough", Topping = "Default" },
                new IcecreamOption { IcecreamId = 8, Flavour = "Default", Topping = "Sprinkles" },
                new IcecreamOption { IcecreamId = 9, Flavour = "Mint Chocolate Chip", Topping = "Default" },
                new IcecreamOption { IcecreamId = 9, Flavour = "Default", Topping = "Chocolate Sauce" },
                new IcecreamOption { IcecreamId = 9, Flavour = "Default", Topping = "Whipped Cream" },
                new IcecreamOption { IcecreamId = 10, Flavour = "Chocolate", Topping = "Default" },
                new IcecreamOption { IcecreamId = 10, Flavour = "Strawberry", Topping = "Default" },
                new IcecreamOption { IcecreamId = 10, Flavour = "Default", Topping = "Cherries" },
                new IcecreamOption { IcecreamId = 11, Flavour = "Vanilla", Topping = "Default" },
                new IcecreamOption { IcecreamId = 11, Flavour = "Default", Topping = "Whipped Cream" },
                new IcecreamOption { IcecreamId = 11, Flavour = "Default", Topping = "Cherries" },
                new IcecreamOption { IcecreamId = 12, Flavour = "Chocolate", Topping = "Default" },
                new IcecreamOption { IcecreamId = 12, Flavour = "Mint Chocolate Chip", Topping = "Default" },
                new IcecreamOption { IcecreamId = 12, Flavour = "Default", Topping = "Chocolate Sauce" },
                new IcecreamOption { IcecreamId = 13, Flavour = "Strawberry", Topping = "Default" },
                new IcecreamOption { IcecreamId = 13, Flavour = "Default", Topping = "Sprinkles" },
                new IcecreamOption { IcecreamId = 13, Flavour = "Default", Topping = "Whipped Cream" },
                new IcecreamOption { IcecreamId = 14, Flavour = "Cookie Dough", Topping = "Default" },
                new IcecreamOption { IcecreamId = 14, Flavour = "Default", Topping = "Chocolate Sauce" },
                new IcecreamOption { IcecreamId = 14, Flavour = "Default", Topping = "Cherries" },
                new IcecreamOption { IcecreamId = 15, Flavour = "Vanilla", Topping = "Default" },
                new IcecreamOption { IcecreamId = 15, Flavour = "Strawberry", Topping = "Default" },
                new IcecreamOption { IcecreamId = 15, Flavour = "Default", Topping = "Sprinkles" },
                new IcecreamOption { IcecreamId = 16, Flavour = "Chocolate", Topping = "Default" },
                new IcecreamOption { IcecreamId = 16, Flavour = "Mint Chocolate Chip", Topping = "Default" },
                new IcecreamOption { IcecreamId = 16, Flavour = "Default", Topping = "Whipped Cream" },
                new IcecreamOption { IcecreamId = 17, Flavour = "Strawberry", Topping = "Default" },
                new IcecreamOption { IcecreamId = 17, Flavour = "Cookie Dough", Topping = "Default" },
                new IcecreamOption { IcecreamId = 17, Flavour = "Default", Topping = "Chocolate Sauce" },
                new IcecreamOption { IcecreamId = 18, Flavour = "Vanilla", Topping = "Default" },
                new IcecreamOption { IcecreamId = 18, Flavour = "Default", Topping = "Sprinkles" },
                new IcecreamOption { IcecreamId = 18, Flavour = "Default", Topping = "Cherries" },
                new IcecreamOption { IcecreamId = 19, Flavour = "Chocolate", Topping = "Default" },
                new IcecreamOption { IcecreamId = 19, Flavour = "Strawberry", Topping = "Default" },
                new IcecreamOption { IcecreamId = 19, Flavour = "Default", Topping = "Whipped Cream" },
                new IcecreamOption { IcecreamId = 20, Flavour = "Mint Chocolate Chip", Topping = "Default" },
                new IcecreamOption { IcecreamId = 20, Flavour = "Default", Topping = "Chocolate Sauce" },
                new IcecreamOption { IcecreamId = 20, Flavour = "Default", Topping = "Sprinkles" }
            ];

            modelBuilder.Entity<Icecream>()
                .HasData(icecreams);

            modelBuilder.Entity<IcecreamOption>()
                .HasData(icecreamOptions);
        }
    }
}
