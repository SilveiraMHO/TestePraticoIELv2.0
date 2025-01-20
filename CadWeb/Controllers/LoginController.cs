using CadWeb.Context;
using CadWeb.Extensions;
using CadWeb.Helpers;
using CadWeb.Models;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace CadWeb.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IValidator<LoginViewModel> _validator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginController(IValidator<LoginViewModel> validator, AppDbContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _validator = validator;
            _httpContextAccessor = contextAccessor;
        }

        public IActionResult Index(int? id)
        {
            if (id == 0)
            {
                _httpContextAccessor.HttpContext.Session.Clear();
            }

            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel viewModel)
        {
            try
            {
                ValidationResult result = _validator.Validate(viewModel);

                if (!result.IsValid)
                {
                    result.AddToModelState(this.ModelState);
                    return View(viewModel);
                }

                var usuario = _context.AppUser.FirstOrDefault(x => x.Usuario == viewModel.Usuario);

                if (usuario is null)
                {
                    ViewData["ErroLogin"] = "Usuário incorreto.";
                    return View(viewModel);
                }

                bool senhaCorreta = PasswordHasher.VerifyPassword(viewModel.Senha, usuario.SenhaHash, usuario.Salt);

                if (!senhaCorreta)
                {
                    ViewData["ErroLogin"] = "Senha incorreta.";
                    return View(viewModel);
                }

                //Inserindo usuário no HttpContext.
                _httpContextAccessor.HttpContext.Session.SetString(AppUserSession.USER_ID, usuario.Id.ToString());
                _httpContextAccessor.HttpContext.Session.SetString(AppUserSession.USER_NAME, usuario.Usuario);
                _httpContextAccessor.HttpContext.Session.SetInt32(AppUserSession.USER_IS_LOGGED, 1);

                return RedirectToAction("Index", "Home");

            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View("Index");
            }
        }
    }
}
