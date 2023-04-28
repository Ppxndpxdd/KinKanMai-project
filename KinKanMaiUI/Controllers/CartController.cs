using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using static System.Net.Mime.MediaTypeNames;

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

        public async Task<IActionResult> RemoveItem(int menuId)
        {
            var cartCount = await _cartRepo.RemoveItem(menuId);
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

        public async Task<IActionResult> Checkout(string txt = "")
        {
            Console.WriteLine(txt);
            try
            {
                bool isCheckedOut = await _cartRepo.DoCheckout(txt);
                if (!isCheckedOut)
                    throw new Exception("Checkout failed");

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                // log the exception
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);

                // return an error message to the client
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong on the server side.");
            }
        }


    }
}
