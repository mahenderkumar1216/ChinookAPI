using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShapingAPI.Controllers
{
    public class HomeController : ControllerBase
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return Ok();
        }

        public IActionResult Albums()
        {
            return Ok();
        }

        public IActionResult Artists()
        {
            return Ok();
        }

        public IActionResult Customers()
        {
            return Ok();
        }
    }
}
