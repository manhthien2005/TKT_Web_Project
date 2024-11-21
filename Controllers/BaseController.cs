using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Tiki_Web_Project.Controllers
{
    public class BaseController : Controller
    {
        public BaseController(IHttpContextAccessor httpContextAccessor)
        {
            var username = httpContextAccessor.HttpContext?.Session.GetString("Username");
            ViewData["Username"] = username; 
        }

        public IActionResult HomePage()
        {
            return View("HomePage");
        }

        public IActionResult Layout()
        {
            return View("Layout");
        }
    }
}
