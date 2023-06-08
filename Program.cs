using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

// Set up services
builder.Services.AddHttpClient("PointerClient", p => p.BaseAddress = new Uri(builder.Configuration["pointerBaseApiAddress"]!));
builder.Services.AddHttpClient("GovPay", c => { c.BaseAddress = new Uri(builder.Configuration["govAddress"]!); });

//Set up consent cookie
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.ConsentCookie.Name = "NIDirectCookieTemplate";
    options.MinimumSameSitePolicy = SameSiteMode.None;
    options.Secure = CookieSecurePolicy.Always;
});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Added for Zap recommendation - X-Frame-Options Header Not Set
app.Use(async (context, next) =>
{
    context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
    context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
    context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");

    context.Response.GetTypedHeaders().CacheControl =
        new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
        {
            NoCache = true,
            NoStore = true,
        };
    context.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.Pragma] = new[] { "no-cache" };
    context.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.CacheControl] = new[] { "no-store, no-cache, must-revalidate, max-age=0" };

    await next();
});

// Set up your dev stripe account at https://dashboard.stripe.com/register

// Set your secret key. Remember to switch to your live secret key in production.
//// See your keys here: https://dashboard.stripe.com/apikeys
StripeConfiguration.ApiKey = builder.Configuration["stripeApiKey"];

if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();