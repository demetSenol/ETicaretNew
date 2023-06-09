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

 void ConfigureServices(IServiceCollection services)
{
	services.AddSession();// Oturum y�netimini etkinle�tirme
}

void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
{
	app.UseSession();// Oturum y�neticisini etkinle�tirme
}


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
{
	x.Cookie.Name = "LoginCookie";
	x.LoginPath = "/UserLogin/Login"; //oturum a��lmad��� durumda y�nlendirilece�i giri� sayfas�n�n url'si
	x.LogoutPath = "/UserLogin/Logout"; //��k�� url'si
	x.ReturnUrlParameter = "returnUrl";
	x.ExpireTimeSpan = TimeSpan.FromMinutes(2);

	//login sayfalar� kar��mamas� i�in eklendi
	x.Events = new CookieAuthenticationEvents
	{
		OnRedirectToLogin = context =>
		{
			// Redirect yap�lacak controller ve action'� belirleme
			if (context.Request.Path.StartsWithSegments("/Admin")) // istek yolunun /Admin ile ba�lay�p ba�lamad���n� kontrol eder
			{
				context.Response.Redirect("/Login/Index"); //�yle ise admin login'e 
			}
			else
			{
				context.Response.Redirect("/UserLogin/Login"); //de�ilse kullan�c� login'e gider
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
