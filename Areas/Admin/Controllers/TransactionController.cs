using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UsersApp.Areas.Admin.Interface;
using UsersApp.Areas.Admin.ViewModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UsersApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TransactionController : Controller
    {
        private readonly ITransaction _Transaction;
        public TransactionController(ITransaction Transaction)
        {
            _Transaction = Transaction;
            
        }
        public async Task<IActionResult> Index(string? from, string? to)
        {
            if (!string.IsNullOrEmpty(from) && !string.IsNullOrEmpty(to))
            {
                if (DateTime.Parse(from) > DateTime.Parse(to))
                {
                    TempData["error"] = "मिति बाट मिति सम्म भन्दा सानो भयो |";
                }
            }
            return View(await _Transaction.GetAllDetailsViewModel(from,to));
        }
        public async Task<IActionResult> Transfer(int id = 0)
        {
            return View(await _Transaction.GetPaymentDetailsById(id));
        }
        [HttpPost]
        public async Task<IActionResult> Transfer(TransactionDetailsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _Transaction.InsertPaymentDetails(model);
                if (response)
                {
                    return RedirectToAction("TransferSuccess", "Transaction", new { area = "Admin" });
                }
            }
            return View(model);

        }
        public IActionResult TransferSuccess()
        {
            return View();
        }                    
        public async Task<IActionResult> Print(int id)
        {
            return View(await _Transaction.GetPaymentDetailsById(id));
        }

    }
}
