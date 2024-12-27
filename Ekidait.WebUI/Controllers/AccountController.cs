using Ekidait.Core.Entities;
using Ekidait.Service.Abstract;
using Ekidait.WebUI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization; //Login
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims; //login

namespace Ekidait.WebUI.Controllers
{
    public class AccountController : Controller
    {
        //private readonly DatabaseContext _context;

        //public AccountController(DatabaseContext context)
        //{
        //    _context = context;
        //}
        private readonly IService<AppUser> _service;

        public AccountController(IService<AppUser> service)
        {
            _service = service;
        }

        [Authorize]
        public  async Task<IActionResult> Index()
        {
            AppUser user=await _service.GetAsync(x=>x.userGuid.ToString() == HttpContext.User.FindFirst("UserGuid").Value);
            if (user == null)
            {
                return NotFound();
            }
            var model = new UserEditViewModel()
            {
                Email = user.Email,
                Id = user.Id,
                Name = user.Name,
                Password = user.Password,
                Surname = user.Surname,
            };
            return View(model);
        }
        [HttpPost,Authorize]
        public async Task<IActionResult> Index(UserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    AppUser user = await _service.GetAsync(x => x.userGuid.ToString() == HttpContext.User.FindFirst("UserGuid").Value);
                    if (user is not null)
                    {
                        user.Surname = model.Surname;
                        user.Name = model.Name;
                        user.Password = model.Password;
                        user.Email = model.Email;
                        _service.Update(user);
                        var sonuc = _service.SaveChanges();
                        if (sonuc > 0)
                        {
                            TempData["Message"] = @"<div class=""alert alert-success alert-dismissible fade show"" role=""alert"">
                      <strong>Bilgileriniz Güncellenmiştir!</strong> 
                      <button type=""button"" class=""btn-close"" data-bs-dismiss=""alert"" aria-label=""Close""></button>
                    </div>";
                            // await MailHelper.SendMailAsync(contact);
                            return RedirectToAction("Index");
                        }
                    }

                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            return View();
        }
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public  async Task<IActionResult> SignInAsync(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
               try
                { 
                    var account = await _service.GetAsync(x => x.Email == loginViewModel.Email & x.Password == loginViewModel.Password & x.IsActive); 
                    if (account == null)
                    {
                        ModelState.AddModelError("", "Giriş Başarısız!");
                    }
                    else
                    {
                        var claims = new List<Claim>()
                        {
                            new(ClaimTypes.Name, account.Name),
                            new(ClaimTypes.Role, account.IsAdmin ? "Admin" : "Customer"),
                            new(ClaimTypes.Email, account.Email),
                            new("UserId", account.Id.ToString()),
                            new("userGuid", account.userGuid.ToString()),
                        };
                        var userIdentity = new ClaimsIdentity(claims, "Login");
                        ClaimsPrincipal userPrincipal = new ClaimsPrincipal(userIdentity);
                        await HttpContext.SignInAsync(userPrincipal);
                    return Redirect(string.IsNullOrEmpty(loginViewModel.ReturnUrl) ? "/" : loginViewModel.ReturnUrl);
                    }
                }
                catch (Exception hata)
                {
                    //loglama
                    ModelState.AddModelError("","Hata oluştu!");
                }
            }
            return View(loginViewModel);
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUpAsync(AppUser appUser)
        {
            appUser.IsAdmin= false;
            appUser.IsActive= true;
            if (ModelState.IsValid)
            {
               await _service.AddAsync(appUser);
                await _service.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(appUser);
        }
        public async Task<IActionResult> SignOutAsync()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("SignIn");
        }
    }
}
