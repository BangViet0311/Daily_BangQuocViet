using Day3.Data;
using Day3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Day3.Controllers
{
    public class ProductController : Controller
    {


        // GET: ProductController
        [HttpGet]
        public ActionResult Index(string searchString,string sortOrder, int page=1)
        {
            var product = DataSeeds.products.AsQueryable();
            if(!string.IsNullOrEmpty(searchString))
            {
                product = product.Where(p => p.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }
            ViewBag.SearchString = searchString;
            ViewBag.SortOrder = sortOrder;

            ViewBag.IdSort = sortOrder == "id_desc" ? "" : "id_desc";
            ViewBag.NameSort = sortOrder == "name_az" ? "name_za" : "name_az";
            switch (sortOrder)
            {
                case "id_desc":
                    product = product.OrderByDescending(p => p.Id);
                    break;
                case "name_az":
                    product = product.OrderBy(p => p.Name);
                    break;
                case "name_za":
                    product = product.OrderByDescending(p => p.Name);
                    break;
               default:
                    product = product.OrderBy(p => p.Id);
                    break;
            }
            int pageSize = 3;
            int totalProducts = product.Count();
            int totalPages = Math.Max(1, (int)Math.Ceiling((double)totalProducts / pageSize));
            page = Math.Clamp(page, 1, totalPages);
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            var pageProduct = product
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
            return View(pageProduct.Select(ToviewModel).ToList());
            
        }
        
        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            var product = DataSeeds.products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // GET: ProductController/Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(DataSeeds.categories, "Id", "Name");
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            product.Id = DataSeeds.products.Count + 1;
            DataSeeds.products.Add(product);
            return RedirectToAction(nameof(Index));
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            var EditProduct = DataSeeds.products.FirstOrDefault(p => p.Id == id);
            if (EditProduct == null)
            {
                return NotFound();
            }

            return View(EditProduct);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            var existingProduct = DataSeeds.products.FirstOrDefault(p => p.Id == product.Id);
            if (existingProduct == null)
            {
                return NotFound();
            }
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.Description = product.Description;
            existingProduct.CategoryId = product.CategoryId;
            return RedirectToAction(nameof(Index));
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            var product = DataSeeds.products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: ProductController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var product = DataSeeds.products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            DataSeeds.products.Remove(product);
            return RedirectToAction(nameof(Index));
        }
        private static ProductViewModel ToviewModel(Product product)
        {
            return new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                CategoryName = DataSeeds.categories.FirstOrDefault(c => c.Id == product.CategoryId)?.Name ?? string.Empty
            };
        }
    }
}
