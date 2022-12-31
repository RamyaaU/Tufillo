using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;
using System.Collections.Generic;
using Tufillo.Infrastructure.Models;

namespace Tufillo___MVC_and_EFCore.Models.ViewModels
{
    public class ProductViewModel
    {
        public Product Product { get; set; }

        public IEnumerable<SelectListItem> CategorySelectList { get; set; }
    }
}
