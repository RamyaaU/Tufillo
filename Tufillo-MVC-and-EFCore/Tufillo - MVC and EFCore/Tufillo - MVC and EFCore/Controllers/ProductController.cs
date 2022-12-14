using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Tufillo.Infrastructure.Constants;
using Tufillo.Infrastructure.Data;
using Tufillo.Infrastructure.Models;
using Tufillo___MVC_and_EFCore.Models.ViewModels;

namespace Tufillo___MVC_and_EFCore.Controllers
{
    public class ProductController : Controller
    {
        private readonly TufilloContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(TufilloContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// this method displays all the products in the view
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            IEnumerable<Product> product = _dbContext.Product;

            foreach (var obj in product)
            {
                obj.Category = _dbContext.Category.FirstOrDefault(x => x.Id == obj.CategoryId);
            }

            return View(product);
        }

        //NOTE - UPSERT is a common method for both edit and delete
        //GET method for both update and insert in single view
        public IActionResult Upsert(int? id)
        {
            ////this specifically retreives data from category model 
            //IEnumerable<SelectListItem> CategoryDropdown = _dbContext.Category.Select(
            //    i => new SelectListItem
            //    {
            //        Text = i.Name,
            //        Value = i.Id.ToString()
            //    });

            ////passing the data to view
            ////ViewBag.CategoryDropdown = CategoryDropdown;

            ////passing the same data through viewdata
            //ViewData["CategoryDropdown"] = CategoryDropdown;

            //instantiate viewmodel and call the items in viewmodel
            ProductViewModel product = new ProductViewModel()
            {
                Product = new Product(),
                CategorySelectList = _dbContext.Category.Select(
                i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };

            if (id == null)
            {
                //this is for create
                return View(product);
            }
            else
            {
                product.Product = _dbContext.Product.Find(id);
                if (product.Product == null)
                {
                    return NotFound();
                }
                return View(product);
            }
        }

        [HttpPost]
        //POST method for both update and insert in single view
        [ValidateAntiForgeryToken]
        public IActionResult UpSert(ProductViewModel productViewModel)
        {
            if(ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                if(productViewModel.Product.Id == 0)
                {
                    //create
                    string uploadPath = webRootPath + ImageConstant.ImagePath;
                    string fileName = Guid.NewGuid().ToString();
                    string fileExtension = Path.GetExtension(files[0].FileName);


                    using (var fileStream = new FileStream(Path.Combine(uploadPath, fileName + fileExtension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    productViewModel.Product.Image = fileName + fileExtension;

                    _dbContext.Product.Add(productViewModel.Product);
                }
                else
                {
                    //update
                }

                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}
