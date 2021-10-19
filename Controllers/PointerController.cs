using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using nidirect_app_frontend.Models;
using nidirect_app_frontend.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace nidirect_app_frontend.Controllers
{
    public class PointerController : Controller
    {
        private readonly IHttpClientFactory _pointerClient;
        private readonly IConfiguration _configuration;
        private const string SectionName = "Pointer";

        public PointerController(IHttpClientFactory pointerClient, IConfiguration configuration)
        {
            _pointerClient = pointerClient;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Index()
        {
            PointerViewModel model = new PointerViewModel
            {
                SectionName = SectionName,
                TitleTagName = "What is your permanent address?"
            };

            return View(model);
        }

        [HttpGet]
        public async Task<JsonResult> GetAddressesAsync(string postCode)
        {
            var client = _pointerClient.CreateClient("PointerClient");

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CreateJwtToken());

            var result = await client.GetAsync("PostCodeSearch/" + postCode);

            List<Pointer> pointerAddresses = new List<Pointer>();

            if (result.IsSuccessStatusCode)
            {
                using (HttpContent content = result.Content)
                {
                    var resp = content.ReadAsStringAsync();
                    pointerAddresses = JsonConvert.DeserializeObject<IEnumerable<Pointer>>(resp.Result).ToList();
                }
            }

            return Json(pointerAddresses);
        }

        /// <summary>
        /// Creates a JWT token to pass to the pointer api to authenticate
        /// </summary>
        /// <returns></returns>
        private string CreateJwtToken()
        {
            var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var iat = Math.Round((DateTime.UtcNow - unixEpoch).TotalSeconds);

            var payload = new Dictionary<string, object>
            {
                { "iat", iat },
                { "kid", _configuration["pointerKid"] }
            };

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            var jwtToken = encoder.Encode(payload, _configuration["pointerSecret"]);
            return jwtToken;
        }
    }
}