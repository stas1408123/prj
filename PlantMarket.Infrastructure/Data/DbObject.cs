using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlantMarket.Common.Models;

namespace PlantMarket.Infrastructure.Data
{
    public class DbObject
    {
        public static void Initial(PlantMarketContext content)
        {

            if (!content.Categories.Any())
                content.Categories.AddRange(Categories.Select(c => c.Value));

            if (!content.Plants.Any())
                content.AddRange(
                    new Plant
                    {
                        Name = "Клубника",
                        ShortDescription = "Красная ягода",
                        LongDescription = "Самая вкусная ягодка",
                        IsFavourite = false,
                        IsAvailable = true,
                        //PictureLink = "/img/klub.jpg",
                        Price = 5,
                        Category = Categories["Огород"]
                    },
                    new Plant
                    {
                        Name = "Земляника",
                        ShortDescription = "Меьше клубники,но не менее вкусная",
                        LongDescription = "",
                        IsFavourite = false,
                        IsAvailable = true,
                        //PictureLink = "/img/zeml.jpg",
                        Price = 10,
                        Category = Categories["Огород"]
                    },
                    new Plant
                    {
                        Name = "Черника",
                        ShortDescription = "Прямо из леса",
                        LongDescription = "Самая вкусная ягодка",
                        IsFavourite = false,
                        IsAvailable = true,
                        //PictureLink = "/img/chernika.jpg",
                        Price = 20,
                        Category = Categories["Огород"]
                    },
                    new Plant
                    {
                        Name = "Магнолия",
                        ShortDescription = "Самый яркий",
                        LongDescription = "Почувствуй настроение лета",
                        IsFavourite = true,
                        IsAvailable = true,
                        //PictureLink = "/img/magnoliya.jpg",
                        Price = 300,
                        Category = Categories["Сад"]
                    },
                    new Plant
                    {
                        Name = "Лаванда",
                        ShortDescription = "Аромат прованса",
                        LongDescription = "Почувствуй настроение лета",
                        IsFavourite = true,
                        IsAvailable = true,
                        //PictureLink = "/img/lavanda.jpg",
                        Price = 150,
                        Category = Categories["Сад"]
                    },
                    new Plant
                    {
                        Name = "Гортензия",
                        ShortDescription = "Великолепный цветок",
                        LongDescription = "Почувствуй настроение лета",
                        IsFavourite = true,
                        IsAvailable = true,
                        //PictureLink = "/img/gortenziya.jpg",
                        Price = 200,
                        Category = Categories["Сад"]
                    }
                    );

            content.SaveChanges();
        }

        private static Dictionary<string, Category> category;
        public static Dictionary<string, Category> Categories
        {
            get
            {
                if (category == null)
                {
                    var list = new Category[]
                    {
                        new Category {Name = "Сад", Description ="Великолепные растения для вашего заднего участка" },
                        new Category { Name = "Огород", Description = "Лучший выбор для вашего огорода" }
                    };

                    category = new Dictionary<string, Category>();
                    foreach (Category element in list)
                    {
                        category.Add(element.Name, element);
                    }
                }

                return category;
            }
        }
    }
}
