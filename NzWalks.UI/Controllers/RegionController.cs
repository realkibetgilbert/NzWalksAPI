using Microsoft.AspNetCore.Mvc;

namespace NzWalks.UI.Controllers
{
    public class RegionController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RegionController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                ///get  regions with ..........from controllers.....and...display here...
                var client = _httpClientFactory.CreateClient();

                var result = await client.GetAsync("https://localhost:7270/api/region");

                result.EnsureSuccessStatusCode();

                var res = await result.Content.ReadAsStringAsync();

                ViewBag.Responce = res;   

            }
            catch (Exception)
            {

                throw;
            }



            return View();
        }
    }
}
