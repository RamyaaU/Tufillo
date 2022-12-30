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

        /// <summary>
        /// this method displays all the products in the view
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            IEnumerable<Product> product = _dbContext.Product;

            foreach ( var obj in product )
            {
                obj.Category = _dbContext.Category.FirstOrDefault(x => x.Id == obj.CategoryId);
            }

            return View(product);
        }

        //NOTE - UPSERT is a common method for both edit and delete
        //GET method for both update and insert in single view
        public IActionResult UpSert(int? id)
        {
            Product product = new Product();
            if (id == null)
            {
                //this is for create
                return View(product);
            }
            else
            {
                product = _dbContext.Product.Find(id);
                if(product == null)
                {
                    return NotFound();
                }
                return View(product);
            }
        }

        //POST method for both update and insert in single view
        [ValidateAntiForgeryToken]
        public IActionResult UpSert(Product product)
        {
            return View();
        }

    }
}
