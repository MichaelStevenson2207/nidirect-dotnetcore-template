using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Stripe;

namespace nidirect_app_frontend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Set up services
            services.AddHttpClient("PointerClient", p => p.BaseAddress = new Uri(Configuration["pointerBaseApiAddress"]));
            services.AddHttpClient("GovPay", c => { c.BaseAddress = new Uri(Configuration["govAddress"]); });

            //Set up consent cookie
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.ConsentCookie.Name = "NIDirectCookieTemplate";
                options.MinimumSameSitePolicy = SameSiteMode.None;
                options.Secure = CookieSecurePolicy.Always;
            });

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
            StripeConfiguration.ApiKey = Configuration["stripeApiKey"];

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
        }
    }
}