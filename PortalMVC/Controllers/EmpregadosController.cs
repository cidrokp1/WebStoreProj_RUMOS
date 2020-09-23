using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace PortalMVC.Controllers
{
    [Authorize]
    public class EmpregadosController : Controller
    {
        public const string endpoint = "http://localhost:63233";


        // GET: Empregados
        public async Task<ActionResult> Index()
        {
            HttpClient client = new HttpClient();
            var RespostaHTTP = await client.GetAsync(endpoint + "/api/Empregados");

            if (RespostaHTTP.IsSuccessStatusCode)
            {
                var dadosJSON = await RespostaHTTP.Content.ReadAsStringAsync();
                var obj = JsonConvert.DeserializeObject<List<Empregado>>(dadosJSON);
                return View(obj);
            }
            else
            {
                return Content("An error has occurred");
            }

           
        }

        // GET: Empregados/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return await ViewWithObjInfo(id);
        }

        // GET: Empregados/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Empregados/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create(IFormCollection collection)
        public async Task<ActionResult> Create([Bind("EmpregadoId,Nome,Email")] Empregado empregado)
        {
            try
            {
                int id = await CreateNew(empregado);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Empregados/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            return await ViewWithObjInfo(id);
        }

        // POST: Empregados/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("EmpregadoId,Nome,Email")] Empregado empregado)
        {
            try
            {
                empregado.EmpregadoId = id; 
                HttpClient client = new HttpClient();
                var jsonObj = System.Text.Json.JsonSerializer.Serialize(empregado);
                var content = new StringContent(jsonObj, Encoding.UTF8, "application/json");
                var RespostaHTTP = await client.PutAsync(endpoint + "/api/Empregados/" + id, content);
                var dadosJSON = await RespostaHTTP.Content.ReadAsStringAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        // GET: Empregados/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            return  await ViewWithObjInfo(id);
        }

   

        // POST: Empregados/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                HttpClient client = new HttpClient();
                var RespostaHTTP = await client.DeleteAsync(endpoint + "/api/Empregados/" + id);
                var dadosJSON = await RespostaHTTP.Content.ReadAsStringAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        private async Task<ActionResult> ViewWithObjInfo(int id)
        {
            HttpClient client = new HttpClient();
            var RespostaHTTP = await client.GetAsync(endpoint + "/api/Empregados/" + id);

            if (RespostaHTTP.IsSuccessStatusCode)
            {
                var dadosJSON = await RespostaHTTP.Content.ReadAsStringAsync();
                var obj = JsonConvert.DeserializeObject<Empregado>(dadosJSON);
                return View(obj);
            }
            else
            {
                return Content("An error has occurred");
            }
        }


        public static async Task<int> CreateNew(Empregado newEmpregado)
        {
            HttpClient client = new HttpClient();
            var jsonObj = System.Text.Json.JsonSerializer.Serialize(newEmpregado);
            var content = new StringContent(jsonObj, Encoding.UTF8, "application/json");
            var RespostaHTTP = await client.PostAsync(endpoint + "/api/Empregados", content);
            var dadosJSON = await RespostaHTTP.Content.ReadAsStringAsync();
            return int.Parse(dadosJSON);
        }


        public static async Task<int> GetUserId(string email)
        {
            int id;
            try
            {
                HttpClient client = new HttpClient();
                var RespostaHTTP = await client.GetAsync(endpoint + "/api/Empregados/GetEmpregadoId/"+ email);
                var dadosJSON = await RespostaHTTP.Content.ReadAsStringAsync();
                id = int.Parse(dadosJSON);
            }
            catch
            {
                id = -1;
            }
            return id;
        }
    }
}
