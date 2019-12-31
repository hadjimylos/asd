namespace pmo.Controllers {
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// return action results/pages within this controller
    /// </summary>

    public class NavController : Controller {
        
        public IActionResult Home() {
            return View();
        }
    }
}