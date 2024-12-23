using Microsoft.AspNetCore.Mvc;
using UsersApp.Services;

public class ForexServiceController : Controller
{
    private readonly ForexService _forexService;
    public ForexServiceController(ForexService forexService)
    {
        _forexService = forexService;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var rates = await _forexService.GetExchangeRatesAsync();
            return View(rates);
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = ex.Message;
            return View(new List<ForexRate>());
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetMYRRate()
    {
        try
        {
            var myrRate = await _forexService.GetMYRRateAsync();
            if (myrRate == null)
                return NotFound("MYR rate not found");
            return Json(new { rate = myrRate.Buy });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}