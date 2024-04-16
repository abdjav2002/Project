using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreMvcFull.Controllers
{
  public class QAController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }
  }
}
