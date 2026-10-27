using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Day3.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public object Action { get; set; }
        public List<Product> Products{ get; set; }
    }
}
