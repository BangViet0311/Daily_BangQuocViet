using Day3.Models;

namespace Day3.Data
{
    public class DataSeeds
    {
        public static List<Category> categories = new List<Category>()
        {
            new Category { Id = 1, Name = "Laptop" },
            new Category { Id = 2, Name = "Phone" },
            new Category { Id = 3, Name = "Tablet" }
        };

        public static List<Product> products = new List<Product>()
        {
            new Product
            {
                Id = 1,
                Name = "Dell XPS 13",
                Price = 1200,
                Description = "Laptop mỏng nhẹ",
                CategoryId = 1
            },
            new Product
            {
                Id = 2,
                Name = "iPhone 15",
                Price = 999,
                Description = "Điện thoại Apple",
                CategoryId = 2
            },
            new Product
            {
                Id = 3,
                Name = "iPad Air",
                Price = 599,
                Description = "Máy tính bảng",
                CategoryId = 3
            }
        };
    }
}
