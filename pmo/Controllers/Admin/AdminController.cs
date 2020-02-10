using Microsoft.AspNetCore.Mvc;

namespace pmo.Controllers {
    [Route("admin")]
    public class AdminController : Controller {
        private readonly string path = "~/Views/Admin/Admin";

        public IActionResult Index() {
            return View($"{path}/Index.cshtml");
        }
    }
}