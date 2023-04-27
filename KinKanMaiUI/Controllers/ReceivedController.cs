using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KinKanMaiUI.Controllers
{  
    public class ReceivedController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IReceivedRepository _receivedRepo;
        private readonly UserManager<IdentityUser> _userManager;
        public ReceivedController(ApplicationDbContext db, IReceivedRepository receivedRepo, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _receivedRepo = receivedRepo;
            _userManager = userManager;
        }

        public async Task<IActionResult> Receiveds()
        {
            var orders = await _receivedRepo.GetOrder();
            return View(orders);
        }

        public async Task<IActionResult> GetUserById(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            string username = user.UserName;

            return Ok(username);
        }

        public async Task<IActionResult> UpdateOrder(int orderId, int newStatus = 2)
        {
            try
            {
                bool isUpdateOrder = await _receivedRepo.UpdateOrders(orderId, newStatus);
                if (!isUpdateOrder)
                    throw new Exception("UpdateOrders failed");
                return RedirectToAction("Receiveds");
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

        public async Task<IActionResult> UpdateReceivedOrder(int orderId, int newStatus = 2)
        {
            try
            {
                bool isUpdateOrder = await _receivedRepo.UpdateOrders(orderId, newStatus);
                if (!isUpdateOrder)
                    throw new Exception("UpdateOrders failed");
                return RedirectToAction("UserReceived");
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

        public async Task<IActionResult> UserReceived()
        {
            var orders = await _receivedRepo.UserReceived();
            return View(orders);
        }
    }
}
