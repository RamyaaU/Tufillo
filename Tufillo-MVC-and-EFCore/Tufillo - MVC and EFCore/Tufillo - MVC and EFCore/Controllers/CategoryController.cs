using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using Tufillo.Infrastructure.Data;
using Tufillo___MVC_and_EFCore.Models;

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
    }
}
