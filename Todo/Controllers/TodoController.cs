using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.Context;

namespace Todo.Controllers
{
    [Authorize]
    public class TodoController : Controller
    {
        private readonly TodoContext context;


        // Request Object TodoContext
        public TodoController(TodoContext todoContext)
        {
            this.context = todoContext;
        }
        // R

        // R
        [AllowAnonymous]
        public IActionResult Index()
        {
            var todos = context.Todos.Include(t => t.Category).ToList();
            return View(todos);
        }

        // C

        public IActionResult NewTodo()
        {
            ViewData["cate"] = context.Categories.ToList();
            return View();
        }

        public IActionResult SaveNewTodo(Todo.Models.Todo newtodo)
        {
            if (ModelState.IsValid)
            {
                newtodo.CreatedDate = DateTime.Now;
                context.Todos.Add(newtodo);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewData["cate"] = context.Categories.ToList();
            return View("NewTodo", newtodo);
        }


        // U

        public IActionResult EditTodo(int id)
        {
            var todo = context.Todos.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return View("Invalid");
            }
            ViewData["cate"] = context.Categories.ToList();
            return View("NewTodo", todo);
        }

        public IActionResult SaveUpdatedTodo(int id, Todo.Models.Todo Modifiedtodo)
        {
            var todo = context.Todos.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return View("Invalid");
            }
            if (ModelState.IsValid)
            {
                todo.Name = Modifiedtodo.Name;
                todo.Description = Modifiedtodo.Description;
                todo.CategoryId = Modifiedtodo.CategoryId;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["cate"] = context.Categories.ToList();
            return View("NewTodo", Modifiedtodo);
        }

        //D
        public ActionResult DeleteTodo(int id)
        {
            var todo = context.Todos.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return View("Invalid");
            }
            context.Remove(todo);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult ToggleTodo(int id)
        {
            var todo = context.Todos.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return View("Invalid");
            }
            todo.IsDone = !todo.IsDone;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
