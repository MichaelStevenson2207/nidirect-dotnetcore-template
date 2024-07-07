using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using nidirect_app_frontend.Models;
using nidirect_app_frontend.ViewModels;
using Stripe.Checkout;

namespace nidirect_app_frontend.Controllers
{
    public class PayController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;
        private const string SectionName = "Pay";

        public PayController(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult GovUkPay()
        {
            BaseViewModel model = new BaseViewModel
            {
                SectionName = SectionName,
                TitleTagName = "Gov UK pay"
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> GovUkPay(BaseViewModel model)
        {
            model.SectionName = SectionName;
            model.TitleTagName = "Gov UK pay";

            string baseUrl = $"{this.Request.Scheme}://{this.Request.Host}/pay/completed";

            Payment payment = new Payment
            {
                Amount = 6250,
                Reference = DateTime.Now.ToString(new CultureInfo("en-GB")),
                Description = "Demo gov pay",
                ReturnUrl = baseUrl
            };

            var client = _clientFactory.CreateClient("GovPay");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _configuration["govPaySecretKey"]);
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            var response = await client.PostAsync("payments", new StringContent(JsonConvert.SerializeObject(payment), Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                using (HttpContent content = response.Content)
                {
                    var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var dataObj = JObject.Parse(responseBody);

                    var nextUrl = dataObj["_links"]["next_url"]["href"].ToString();

                    // Creating cookie of the paymentUrl / Payment Id, Ideally we'd want to store this in a DB with
                    // assiociated user but for demo just save to cookies. As this info is needed in the Completed action 
                    // to work out status of payment from govpay, and any future reference to this payment such as refunds etc.

                    Response.Cookies.Append(
                        "paymentUrl",
                        dataObj["_links"]["self"]["href"].ToString(),
                        new CookieOptions()
                        {
                            Path = "/",
                            HttpOnly = true,
                            Secure = true
                        }
                    );

                    return Redirect(nextUrl);
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Completed(BaseViewModel model)
        {
            model.SectionName = SectionName;
            model.TitleTagName = "Completed";

            //reading the cookie so we can make a call to see the status of the payment from gov pay.
            string paymentUrl = Request.Cookies["paymentUrl"];

            var client = _clientFactory.CreateClient("GovPay");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _configuration["govPaySecretKey"]);
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            var response = await client.GetAsync(paymentUrl);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var dataObj = JObject.Parse(responseBody);

                var status = dataObj["state"]["status"].ToString();
                ViewData["status"] = status;

                //if not success we assume payment has failed
                if (status != "success")
                {
                    var message = dataObj["state"]["message"].ToString();
                    var code = dataObj["state"]["code"].ToString();
                    ModelState.AddModelError(code, message);
                }
                else
                {
                    // Flash message of success.
                    TempData["Success"] = "Payment has been received with thanks";
                }

                Response.Cookies.Delete("paymentUrl");
            }
            return View(model);
        }


        [HttpGet]
        public IActionResult Stripe()
        {
            BaseViewModel model = new BaseViewModel
            {
                SectionName = "Stripe",
                TitleTagName = "Stripe"
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Stripe(BaseViewModel model)
        {
            // Get callback url
            var domain = $"{this.Request.Scheme}://{this.Request.Host}/";

            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
                {
                  new SessionLineItemOptions
                  {
                    // replace this with the `price` of the product you want to sell.
                    // Set up the products and prices in the Dashboard of the stipe site.
                    // For each price you require an Id from the dashboard to display the price info
                    Price = _configuration["stripePriceId"],
                    Quantity = 1,
                  },
                },
                PaymentMethodTypes = new List<string>
                {
                  "card"
                },
                Mode = "payment",
                SuccessUrl = domain + "pay/stripesuccess",
                CancelUrl = domain + "pay/stripecancel",
            };
            var service = new SessionService();

            Session session = service.Create(options);

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }

        public IActionResult StripeSuccess()
        {
            BaseViewModel model = new BaseViewModel
            {
                SectionName = "Stripe",
                TitleTagName = "Success"
            };

            // Store success in the database
            return View(model);
        }

        public IActionResult StripeCancel()
        {
            BaseViewModel model = new BaseViewModel
            {
                SectionName = "Stripe",
                TitleTagName = "Cancelled"
            };

            return View(model);
        }
    }
}