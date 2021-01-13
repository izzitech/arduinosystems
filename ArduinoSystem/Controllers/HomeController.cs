using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ArduinoSystem.Models;
using Microsoft.AspNetCore.Identity;
using ArduinoSystem.Data;

namespace ArduinoSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _usrMgr;

        public HomeController(UserManager<ApplicationUser> userManager, ILogger<HomeController> logger)
        {
            _logger = logger;
            _usrMgr = userManager;
        }

        public IActionResult Index()
        {
            var model = new HomeViewModel();
            model.users = new List<HomeUserDto>();
            foreach(var user in _usrMgr.Users)
            {
                var dto = new HomeUserDto
                {
                    Id = user.Id,
                    Name = user.Name
                };
                model.users.Add(dto);
            }
                
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
