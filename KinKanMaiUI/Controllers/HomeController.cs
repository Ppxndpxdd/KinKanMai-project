using KinKanMaiUI.Data;
using KinKanMaiUI.Models;
using KinKanMaiUI.Models.DTOs;
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

        public async Task<IActionResult> Index(string sterm="",int shopId=0)
        {
            IEnumerable<Menu> menus = await _homeRepository.GetMenus(sterm,shopId);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Menu obj)
        {

            if (ModelState.IsValid)
            {
                _db.Menus.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
