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

		
		private readonly EticaretContext _context;

		public UserLoginController()
			
		{
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
            
                if (ModelState.IsValid)
                {
                    _context.Add(user); // yeni kullanıcı ekledik
                    await _context.SaveChangesAsync(); //asenkron olarak veritabanıyla iletişime geçip yaptığımız değişiklikleri kaydeder
                    return RedirectToAction(nameof(Login));//login sayfasına gönderir kaydı yapınca 
                }
                return View(user);//veriler doğru değilse aynı sayfaya tekrar gelir 

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
						claims.Add(new Claim(ClaimTypes.NameIdentifier, entity.Email)); //yeni claim nesnesinde kullanıcının kimlik bilgilerinden emailini tanımlamak içindir.
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
		//public IActionResult Logout()
		//{
		//	return View();
		//}
        public async Task<IActionResult> LogOut()
        {
			//kullanıcıyı belirtilen kimlik doğrulama şemasından çıkış yapmak için kullanılır
		   await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "UserLogin");
        }

    }
}
