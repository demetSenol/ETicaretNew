using ETicaretNew.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Configuration;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.Services.AddDbContext<DbContext>(options =>
//{
//    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon"));
//});


////farklý bir yol deneyecem
//public void ConfigureServices(IServiceCollection services) 
//{
//	// Kimlik doðrulama için DbContext'i yapýlandýrýn
//	services.AddDbContext<EticaretContext>(options =>
//options.UseSqlServer(Configuration.GetConnectionString("Data Source=DESKTOPI6AM0E5;Initial Catalog=ETicaret;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False")));

//	// Kimlik doðrulama hizmetlerini yapýlandýrýn
//	services.AddIdentity<IdentityUser, IdentityRole()
//		.AddEntityFrameworkStores<EticaretContext>()
//			.AddDefaultTokenProviders();
//}


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
{
	x.Cookie.Name = "LoginCookie";
	x.LoginPath = "/UserLogin/Login";
	x.LogoutPath = "/UserLogin/Logout";
	x.ReturnUrlParameter = "returnUrl";
	x.ExpireTimeSpan = TimeSpan.FromMinutes(2);

	//login sayfalarý karýþmamasý için eklendi
	x.Events = new CookieAuthenticationEvents
	{
		OnRedirectToLogin = context =>
		{
			// Redirect yapýlacak controller ve action'ý belirleme
			if (context.Request.Path.StartsWithSegments("/Admin")) // yolun /Admin ile baþlayýp baþlamadýðýný kontrol eder
			{
				context.Response.Redirect("/Login/Index"); //öyle ise admin login'e 
			}
			else
			{
				context.Response.Redirect("/UserLogin/Login"); //deðilse kullanýcý login'e gider
			}
			return Task.CompletedTask;
		}
	};
	});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();




app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
