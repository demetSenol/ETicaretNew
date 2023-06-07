﻿using ETicaretNew.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace ETicaretNew.Controllers
{
	public class UserLoginController : Controller
	{
		//yeni kullanıcı oluşturur
		//kullanıcı oluşturma, düzenleme, silme, parola sıfırlama
			//private readonly UserManager<IdentityUser> _userManager;
			//private readonly SignInManager<IdentityUser> _signInManager;
		//kullanıcı oturumu yönetimi ve kimlik doğrulama işlevleri

		private readonly EticaretContext _context;

		public UserLoginController()
		{
			//_userManager = userManager;
			//_signInManager = signInManager;

			_context = new EticaretContext();
		}
		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromForm]User user)
		{
            //register işlemleri
            //if(ModelState.IsValid)
            //{
            //	var user = new IdentityUser 
            //	{ 
            //		UserName = model.Adi, 
            //		Email = model.Email 
            //	};
            //	var result = await _userManager.CreateAsync(user, model.Sifre);
            //	if (result.Succeeded)
            //	{
            //		await _signInManager.SignInAsync(user, isPersistent: false);
            //		return RedirectToAction("Index","Default");
            //	}
            //	foreach (var error in result.Errors)
            //	{
            //		ModelState.AddModelError("", error.Description);
            //	}
            //}
            //return View(model);
            
                if (ModelState.IsValid)
                {
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Login));
                }
                return View(user);
            

        }

        public IActionResult Login()
		{
			return View();
		}
		
		[HttpPost]
		public async Task<IActionResult> Login([FromForm] User entity)
		{

			try
			{
				if (ModelState.IsValid)//viewden gelen modelde bir sıkıntı var mı 
				{
					if (CheckUser(entity.Email, entity.Sifre))//user var mı yok mu diye kontrol ettik
					{//veriler doğru ise
						var claims = new List<Claim>();
						claims.Add(new Claim(ClaimTypes.NameIdentifier, entity.Email));
						ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
						ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
						await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
						return RedirectToAction("Index", "Default");
					}
					else //yoksa böyle bir hata alsın 
					{
						throw new Exception("User not found!");
					}
				}
				else
				{
					throw new Exception("Please check form data!");
				}
			}
			catch (Exception ex)
			{
				TempData["msg"] = ex.Message;
			}
			return View(entity);
		}
		private bool CheckUser(string email, string sifre)//kullanıcının olup olmadığını anlayabilmek için oluşturduğumuz metod
		{
			var user = _context.Users.FirstOrDefault(x => x.Email == email && x.Sifre == sifre);

			return user != null;
		}
		public IActionResult Logout()
		{
			return View();
		}

	}
}
