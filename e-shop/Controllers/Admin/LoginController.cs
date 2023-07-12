using e_shop.Models;
using Microsoft.AspNetCore.Mvc;

namespace e_shop.Controllers.Admin
{
    public class LoginController : Controller
    {
        private readonly MyContext _context;

        public LoginController(MyContext context)
        {
            _context = context;
        }

        public IActionResult Index(string next)
        {
            return View(new LoginModel() { Next = next });
        }

        [HttpPost]
        public IActionResult Index(LoginModel model)
        {
            if (IsValid(model))
            {
                this.HttpContext.Session.SetString("login", model.Username);

                if (string.IsNullOrWhiteSpace(model.Next))
                    model.Next = "Index:Home";

                string[] parts = model.Next.Split(':');

                return RedirectToAction(parts[0], parts[1]);
            }

            this.ViewBag.Message = "Zadali jste špatné údaje, zkuste to znovu.";

            return View(model);
        }

        public IActionResult Logout()
        {
            this.HttpContext.Session.Remove("login");

            return RedirectToAction("Index", "Login");
        }

        private bool IsValid(LoginModel model)
        {
            Administrator administrator = _context.Administrators.Where(a => a.Username == model.Username && a.Password == model.Password).FirstOrDefault();

            return administrator != null;
        }
    }
}
