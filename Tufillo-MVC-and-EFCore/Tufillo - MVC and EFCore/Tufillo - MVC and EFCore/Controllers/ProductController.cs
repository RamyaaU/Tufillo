using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Tufillo.Infrastructure.Data;
using Tufillo___MVC_and_EFCore;
using Tufillo.Infrastructure.Models;
using Tufillo___MVC_and_EFCore.Models.ViewModels;
using Tufillo.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;

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
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                if (productViewModel.Product.Id == 0)
                {
                    //create
                    //string uploadPath = webRootPath + Image.ImagePath;
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
                    //one issue faced here is EFCore is
                    //the same product will be updated and retrieved
                    //as both of them has same keys
                    //so to fix this - AsNoTrackingmethod is used
                    var objFromDb = _dbContext.Product.AsNoTracking().FirstOrDefault(u => u.Id == productViewModel.Product.Id);

                    if (files.Count > 0)
                    {
                        string uploadPath = webRootPath + ImageConstant.ImagePath;
                        string fileName = Guid.NewGuid().ToString();
                        string fileExtension = Path.GetExtension(files[0].FileName);

                        //checks the oldimage
                        var oldImage = Path.Combine(uploadPath, objFromDb.Image);
                        //if oldimage present deletes teh oldimage
                        if (System.IO.File.Exists(oldImage))
                        {
                            System.IO.File.Delete(oldImage);
                        }

                        //updates with new image
                        using (var fileStream = new FileStream(Path.Combine(uploadPath, fileName + fileExtension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }

                        productViewModel.Product.Image = fileName + fileExtension;
                    }
                    else
                    {
                        //if the image is not updated, but other info is updated then
                        //then the old image will be saved unmodified.
                        productViewModel.Product.Image = objFromDb.Image;
                    }
                    _dbContext.Update(productViewModel.Product);
                }

                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            productViewModel.CategorySelectList = _dbContext.Category.Select(
                i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
            return View(productViewModel);
        }

    }
}
