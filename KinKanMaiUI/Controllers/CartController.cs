using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KinKanMaiUI.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepo;

        public CartController(ICartRepository cartRepo) 
        {
            _cartRepo = cartRepo;
        }

        public async Task<IActionResult> AddItem(int menuId,int qty=1,int redirect = 0)
        {
            var cartCount = await _cartRepo.AddItem(menuId, qty);
            if (redirect == 0)
                return Ok(cartCount);
            return RedirectToAction("GetUserCart");
        }

        public async Task<IActionResult> RemovedItem(int menuId)
        {
            var cartCount = await _cartRepo.RemovedItem(menuId);
            return RedirectToAction("GetUserCart");
        }

        public async Task<IActionResult> GetUserCart()
        {
            var cart = await _cartRepo.GetUserCart();
            return View(cart);
        }
        public async Task<IActionResult> GetTotalItemInCart() 
        {
            int cartItem = await _cartRepo.GetCartItemCount();
            return Ok(cartItem);
        }
    }
}
