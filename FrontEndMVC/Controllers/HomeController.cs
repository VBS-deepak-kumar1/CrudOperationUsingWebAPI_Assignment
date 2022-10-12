using DataAccessLayer;
using FrontEndMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrontEndMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
    
        public async Task<IActionResult> Index()
        {
            List<Duser> dusers = new List<Duser>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44375/");
            HttpResponseMessage response = await client.GetAsync("api/Duser");
            if (response.IsSuccessStatusCode)
            {
                var results = response.Content.ReadAsStringAsync().Result;
                dusers = JsonConvert.DeserializeObject<List<Duser>>(results);
                    
            }
            
            return View(dusers);
        }
        public async Task<IActionResult> Details(int Id)
        {
            Duser dusers = await GetUserByID(Id);
            return View(dusers);
        }

        private static async Task<Duser> GetUserByID(int Id)
        {
            Duser dusers = new Duser();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44375/");
            HttpResponseMessage response = await client.GetAsync($"api/Duser/{Id}");
            if (response.IsSuccessStatusCode)
            {
                var results = response.Content.ReadAsStringAsync().Result;
                dusers = JsonConvert.DeserializeObject<Duser>(results);
            }

            return dusers;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Duser duser)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44375/");
            HttpResponseMessage response = await client.PostAsJsonAsync<Duser>("api/Duser", duser);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();

        }
        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44375/");
            var response = await client.DeleteAsync($"api/Duser/{Id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");

            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            Duser duser =await GetUserByID(Id);
            return View(duser);
        }
        [HttpPost]
        public async Task<IActionResult>Edit(Duser duser)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44375/");
            HttpResponseMessage response = await client.PutAsJsonAsync<Duser>($"api/Duser/{duser.DuserID}", duser);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
