using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Context;
using Todo.Models;

namespace Todo.Controllers
{
    [Authorize(Policy = "Admin")]
    public class CategoryController : Controller
    {

        private readonly TodoContext context;


        // Request Object TodoContext
        public CategoryController(TodoContext todoContext)
        {
            this.context = todoContext;
        }
        // R
        public IActionResult Index()
        {

            var categories = context.Categories.ToList();
            return View(categories);
        }

        // C
        public IActionResult NewCategory()
        {
            return View();
        }

        public ActionResult SaveNewCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                context.Categories.Add(category);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("NewCategory");
        }

        // U
        public IActionResult EditCategory(int id)
        {
            var category = context.Categories.FirstOrDefault(c => c.Id == id);
            if (category is null)
            {
                return View("Invalid");
            }
            return View("NewCategory", category);
        }

        public IActionResult SaveEditCategory(int id, Category category)
        {
            if (ModelState.IsValid)
            {
                var Editedcategory = context.Categories.FirstOrDefault(c => c.Id == id);
                Editedcategory.Name = category.Name;
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("NewCategory", category);

        }

        // D
        public IActionResult DeleteCategory(int id)
        {
            var cate = context.Categories.FirstOrDefault(c => c.Id == id);
            if (cate is null)
            {
                return View("Invalid");
            }
            context.Remove(cate);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
