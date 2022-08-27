using AirLines.ApiModel;
using AirLines.customMiddleware;
using AirLines.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Airlines.Controllers
{
    public class OperatorController : Controller
    {
        private readonly IMapper _mapper;
        Uri baseAddress = new Uri("https://localhost:7087/");
        private readonly HttpClient client;
        [CustomAuthorize("Operator")]
        public OperatorController(IMapper mapper)
        {
            _mapper = mapper;
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        // GET: Operator
        [CustomAuthorize("Operator")]
        public IActionResult Index()
        {
            ViewData["UserName"] = HttpContext.Session.GetString("Name");
            HttpResponseMessage response = client.GetAsync(baseAddress + "Airlines/GetAirlines").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                var list = JsonConvert.DeserializeObject<List<AirLineApiModel>>(data);
                var modellist = _mapper.Map<List<AirlineViewModel>>(list);
                modellist = modellist.OrderBy(m => m.Name).ToList();
                return View(modellist);
            }
            else
                return View();
        }
        //// GET: Operator/Create
        public IActionResult Create()
        {
            ViewData["UserName"] = HttpContext.Session.GetString("Name");
            return View();
        }
        [CustomAuthorize("Operator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,FromCity,ToCity,Fare")] AirlineViewModel model)
        {
            if (ModelState.IsValid)
            {

                var apimodel = _mapper.Map<AirLineApiModel>(model);
                var stringdata = JsonConvert.SerializeObject(apimodel);
                var contentData = new StringContent(stringdata, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PostAsync(baseAddress + "Airlines/CreateAirLines", contentData).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["UserName"] = HttpContext.Session.GetString("Name");
            return View(model);
        }

        // GET: Operator/Edit/5
        [CustomAuthorize("Operator")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            HttpResponseMessage response = client.GetAsync(baseAddress + "Airlines/GetAirLineById/Id?id=" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;

                string stringData = response.Content.ReadAsStringAsync().Result;
                AirLineApiModel model = JsonConvert.DeserializeObject<AirLineApiModel>(stringData)!;
                var airlines = _mapper.Map<AirlineViewModel>(model);
                ViewData["UserName"] = HttpContext.Session.GetString("Name");
                return View(airlines);
            }
            return View();
        }
        [CustomAuthorize("Operator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("Id,Name,FromCity,ToCity,Fare")] AirlineViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model != null)
                {
                    var apimodel = _mapper.Map<AirLineApiModel>(model);
                    var stringdata = JsonConvert.SerializeObject(apimodel);
                    var contentData = new StringContent(stringdata, System.Text.Encoding.UTF8, "application/json");

                    HttpResponseMessage response = client.PutAsync(baseAddress + "Airlines/UpdateAirLines", contentData).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        ViewData["UserName"] = HttpContext.Session.GetString("Name");
                        return RedirectToAction(nameof(Index));
                    }
                    else
                        return View();
                }
                else
                    return NoContent();
            }
            ViewData["UserName"] = HttpContext.Session.GetString("Name");
            return View(model);
        }
        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = client.DeleteAsync(baseAddress + "AirLines/DeleteAirlines?id=" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return Ok();
            }
        }
    }
}
