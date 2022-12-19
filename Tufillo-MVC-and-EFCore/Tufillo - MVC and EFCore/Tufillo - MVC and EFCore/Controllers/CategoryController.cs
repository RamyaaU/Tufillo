using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using Tufillo.Infrastructure.Data;
using Tufillo.Infrastructure.Models;

namespace Tufillo___MVC_and_EFCore.Controllers
{
    public class CategoryController : Controller
    {
        private readonly TufilloContext _dbContext;

        public CategoryController(TufilloContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objects = _dbContext.Category;
            return View(objects);
        }

        //GET - Create 
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category addCategory)
        {
            //this method checks if all the rules defined in view is valid,
            //if it is valid
            //then it goes inside the method and executes
            if (ModelState.IsValid)
            {
                _dbContext.Category.Add(addCategory);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(addCategory);
        }
    }
}
