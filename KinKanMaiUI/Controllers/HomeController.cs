using KinKanMaiUI.Data;
using KinKanMaiUI.Models;
using KinKanMaiUI.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace KinKanMaiUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeRepository _homeRepository;

        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db, IHomeRepository homeRepository)
        {
            _logger = logger;
            _db = db;
            _homeRepository = homeRepository;
        }
        [Authorize]
        public async Task<IActionResult> Index(string sterm="",int shopId=0)
        {
            if (User.Identity.IsAuthenticated && (User.IsInRole("Customer") || User.IsInRole("Admin")))
            {
                IEnumerable<Menu> menus = await _homeRepository.GetMenus(sterm, shopId);
                IEnumerable<Shop> shops = await _homeRepository.Shops();
                MenuDisplayModel menuModel = new MenuDisplayModel
                {
                    Menus = menus,
                    Shops = shops,
                    Sterm = sterm,
                    ShopId = shopId
                };
                return View(menuModel);
            }
            else if (User.Identity.IsAuthenticated && User.IsInRole("Rider"))
            {
                return RedirectToAction("Receiveds", "Received");
            }
            else
            {
                return Redirect("/Identity/Account/Login");
            }
            
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
