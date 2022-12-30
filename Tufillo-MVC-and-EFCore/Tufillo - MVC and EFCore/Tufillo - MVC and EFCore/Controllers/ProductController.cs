using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tufillo.Infrastructure.Data;
using Tufillo.Infrastructure.Models;

namespace Tufillo___MVC_and_EFCore.Controllers
{
    public class ProductController : Controller
    {
        private readonly TufilloContext _dbContext;

        public ProductController(TufilloContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> product = _dbContext.Product;

            foreach ( var obj in product )
            {
                obj.Category = _dbContext.Category.FirstOrDefault(x => x.Id == obj.CategoryId);
            }

            return View(product);
        }
    }
}
