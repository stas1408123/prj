using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlantMarket.Common.Models;

namespace PlantMarket.Infrastructure.Data
{
    public class PlantMarketContext : DbContext
    {
        public PlantMarketContext(DbContextOptions<PlantMarketContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<ShopCartItem> ShopCarItem { get; set; }

        public DbSet<ShopCart> ShopCarts { get; set; }

        public DbSet<Plant> Plants { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderedPlant> OrderedPlant { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<AccessData> AccessDatas { get; set; }
   

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            Category category1 = new Category
            {
                Id = 1,
                Name = "Сад",
                Description = "Великолепные растения для вашего заднего участка"
            };

            Category category2 = new Category
            {
                Id = 2,
                Name = "Огород",
                Description = "Лучший выбор для вашего огорода"
            };

            modelBuilder.Entity<Plant>().HasData
                (
                new Plant
                {
                    Id = 1,
                    Name = "Клубника",
                    ShortDescription = "Красная ягода",
                    LongDescription = "Самая вкусная ягодка",
                    IsFavourite = false,
                    IsAvailable = true,
                    //PictureLink = "/img/klub.jpg",
                    Price = 5,
                    CategoryId = 1,
                },
                new Plant
                {
                    Id = 2,
                    Name = "Земляника",
                    ShortDescription = "Меьше клубники,но не менее вкусная",
                    LongDescription = "",
                    IsFavourite = false,
                    IsAvailable = true,
                    //PictureLink = "/img/zeml.jpg",
                    Category = category1,
                    Price = 10,
                    CategoryId = 1,
                },
                new Plant
                {
                    Id = 3,
                    Name = "Черника",
                    ShortDescription = "Прямо из леса",
                    LongDescription = "Самая вкусная ягодка",
                    IsFavourite = false,
                    IsAvailable = true,
                    //PictureLink = "/img/chernika.jpg",
                    Category = category1,
                    Price = 20,
                    CategoryId = 1,
                },
                new Plant
                {
                    Id = 4,
                    Name = "Магнолия",
                    ShortDescription = "Самый яркий",
                    LongDescription = "Почувствуй настроение лета",
                    IsFavourite = true,
                    IsAvailable = true,
                    //PictureLink = "/img/magnoliya.jpg",
                    Category = category2,
                    Price = 300,
                    CategoryId = 2,
                },
                new Plant
                {
                    Id = 5,
                    Name = "Лаванда",
                    ShortDescription = "Аромат прованса",
                    LongDescription = "Почувствуй настроение лета",
                    IsFavourite = true,
                    IsAvailable = true,
                    //PictureLink = "/img/lavanda.jpg",
                    Category = category2,
                    Price = 150,
                    CategoryId = 2,
                },
                new Plant
                {
                    Id = 6,
                    Name = "Гортензия",
                    ShortDescription = "Великолепный цветок",
                    LongDescription = "Почувствуй настроение лета",
                    IsFavourite = true,
                    IsAvailable = true,
                    //PictureLink = "/img/gortenziya.jpg",
                    Price = 200,
                    Category = category2,
                    CategoryId = 2,
                }
                ); 


            base.OnModelCreating(modelBuilder);
        }*/
    }
}
