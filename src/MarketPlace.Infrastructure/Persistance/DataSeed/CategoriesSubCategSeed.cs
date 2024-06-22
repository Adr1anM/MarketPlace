using MarketPlace.Domain;
using MarketPlace.Domain.Models;
using MarketPlace.Infrastructure.Persistance.Context;


namespace MarketPlace.Infrastructure.DataSeed
{
    public class CategoriesSubCategSeed
    {
        public static async Task Seed(ArtMarketPlaceDbContext context)
        {
                  
            if (!context.CategorySubcategories.Any())
            {
                
                var categories = context.Categories.ToList();
      
                var categorySubCategories = new List<CategorySubcategory>();

               
                foreach (var category in categories)
                {
                    switch (category.Name)
                    {
                        case "Paint":
                            categorySubCategories.AddRange(new List<CategorySubcategory>
                            {
                                new CategorySubcategory { CategoryId = category.Id, SubCategoryId = 1 }, // Paint -> Abstract
                                new CategorySubcategory { CategoryId = category.Id, SubCategoryId = 2 }, // Paint -> Contemporary
                                new CategorySubcategory { CategoryId = category.Id, SubCategoryId = 3 }, // Paint -> Landscape
                                new CategorySubcategory { CategoryId = category.Id, SubCategoryId = 4 }, // Paint -> Religious
                                new CategorySubcategory { CategoryId = category.Id, SubCategoryId = 5 }, // Paint -> Figurative
                                new CategorySubcategory { CategoryId = category.Id, SubCategoryId = 12 } // Paint -> Oil
                            });
                            break;
                        case "Drawing":
                            categorySubCategories.AddRange(new List<CategorySubcategory>
                            {
                                new CategorySubcategory { CategoryId = category.Id, SubCategoryId = 11 } // Drawing -> Pencil
                            });
                            break;
                        case "Sculpture":
                            categorySubCategories.AddRange(new List<CategorySubcategory>
                            {
                                new CategorySubcategory { CategoryId = category.Id, SubCategoryId = 1 }, // Sculpture -> Abstract
                                new CategorySubcategory { CategoryId = category.Id, SubCategoryId = 2 }, // Sculpture -> Contemporary
                                new CategorySubcategory { CategoryId = category.Id, SubCategoryId = 5 }, // Sculpture -> Figurative
                            });
                            break;
                        case "Photography":
                            categorySubCategories.AddRange(new List<CategorySubcategory>
                            {
                                new CategorySubcategory { CategoryId = category.Id, SubCategoryId = 6 }, // Photography -> Portrait
                                new CategorySubcategory { CategoryId = category.Id, SubCategoryId = 7 }  // Photography -> Street
                            });
                            break;
                        case "Ceramics":
                            categorySubCategories.AddRange(new List<CategorySubcategory>
                            {
                                new CategorySubcategory { CategoryId = category.Id, SubCategoryId = 8 } // Ceramics -> Ceramics
                            });
                            break;
                        case "Crafts":
                            categorySubCategories.AddRange(new List<CategorySubcategory>
                            {
                                new CategorySubcategory { CategoryId = category.Id, SubCategoryId = 9 }, // Crafts -> Textile
                                new CategorySubcategory { CategoryId = category.Id, SubCategoryId = 10 } // Crafts -> Woodworking
                            });
                            break;
                        default:
                           
                            break;
                    }
                }

                context.CategorySubcategories.AddRange(categorySubCategories);
                await context.SaveChangesAsync();
            }
        }
    }
}
