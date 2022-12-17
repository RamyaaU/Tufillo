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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ApplicationType applicationType)
        {
            _dbContext.ApplicationType.Add(applicationType);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
