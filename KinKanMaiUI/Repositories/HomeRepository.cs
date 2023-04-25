using KinKanMaiUI.Data;
using KinKanMaiUI.Models;
using Microsoft.EntityFrameworkCore;

namespace KinKanMaiUI.Repositories
{
    public class HomeRepository : IHomeRepository
    {
        private readonly ApplicationDbContext _db;
        public HomeRepository(ApplicationDbContext db) 
        { 
            _db = db;
        }

        public async Task<IEnumerable<Shop>> Shops()
        {
            return await _db.Shops.ToListAsync();
        }

        public async Task<IEnumerable<Menu>> GetMenus(string sTerm="",int shopId = 0)
        {
            sTerm = sTerm.ToLower();
            IEnumerable<Menu> menus = await (from menu in _db.Menus
                        join Shop in _db.Shops
                        on menu.ShopId equals Shop.Id
                        where string.IsNullOrWhiteSpace(sTerm) || (menu!=null && menu.MenuName.ToLower().StartsWith(sTerm))
                        select new Menu
                        {
                            Id = menu.Id,
                            Image = menu.Image,
                            MenuName = menu.MenuName,
                            ShopId = menu.ShopId,
                            Price = menu.Price,
                            ShopName = Shop.MenuName
                        }
                        ).ToListAsync();
            if (shopId > 0)
            {
                menus = menus.Where(a => a.ShopId ==shopId).ToList();
            }
            return menus;
        }
    }
}
