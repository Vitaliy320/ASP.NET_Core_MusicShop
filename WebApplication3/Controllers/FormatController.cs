using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Controllers
{
    [FormatFilter]
    public abstract class FormatController : Controller
    {
        protected ActionResult FormatOrView(object model)
        {
            var filter = HttpContext.RequestServices.GetRequiredService<FormatFilter>();
            if (filter.GetFormat(ControllerContext) == null)
            {
                return View(model);
            }
            else
            {
                return new ObjectResult(model);
            }
        }
    }
}
