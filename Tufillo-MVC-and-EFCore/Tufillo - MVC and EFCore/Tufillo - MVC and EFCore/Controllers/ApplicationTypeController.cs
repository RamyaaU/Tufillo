using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Tufillo.Infrastructure.Data;
using Tufillo.Infrastructure.Models;

namespace Tufillo___MVC_and_EFCore.Controllers
{
    public class ApplicationTypeController : Controller
    {
        private readonly TufilloContext _dbContext;

        public ApplicationTypeController(TufilloContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            IEnumerable<ApplicationType> objects = _dbContext.ApplicationType;
            return View(objects);
        }

        //GET - Create 
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Add Category
        /// </summary>
        /// <param name="addCategory"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ApplicationType addAppType)
        {
            //this method checks if all the rules defined in view is valid,
            //if it is valid
            //then it goes inside the method and executes
            if (ModelState.IsValid)
            {
                _dbContext.ApplicationType.Add(addAppType);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(addAppType);
        }

        //GET - Edit
        //why id is being passed here because from the ui page, 
        //we are passing the object as id 
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var obj = _dbContext.ApplicationType.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="addCategory"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditApplicationType(ApplicationType editAppType)
        {
            //this method checks if all the rules defined in view is valid,
            //if it is valid
            //then it goes inside the method and executes
            if (ModelState.IsValid)
            {
                _dbContext.ApplicationType.Update(editAppType);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(editAppType);
        }

        //GET - Delete
        //why id is being passed here because from the ui page, 
        //we are passing the object as id 
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var obj = _dbContext.ApplicationType.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteApplicationType(int? id)
        {
            var obj = _dbContext.Category.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _dbContext.Category.Remove(obj);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
