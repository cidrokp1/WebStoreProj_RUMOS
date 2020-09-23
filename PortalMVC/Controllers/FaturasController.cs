using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace PortalMVC.Controllers
{
    [Authorize]
    public class FaturasController : Controller
    {
        private IWebHostEnvironment Environment;
        public const string endpoint = "http://localhost:63233";


        public FaturasController(IWebHostEnvironment _environment)
        {
            Environment = _environment;
        }


        // GET: Faturas
        public async Task<ActionResult> Index()
        {
            HttpClient client = new HttpClient();
            var RespostaHTTP = await client.GetAsync(endpoint + "/api/Faturas");
            var dadosJSON = await RespostaHTTP.Content.ReadAsStringAsync();

            var obj = JsonConvert.DeserializeObject<List<Fatura>>(dadosJSON);
            return View(obj);
        }


        // GET: Faturas/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return await ViewWithObjInfo(id);
        }

        // GET: Faturas/DownloadExampleXml/
        public FileResult DownloadExampleXml()
        {
            string wwwPath = this.Environment.WebRootPath;

            byte[] fileBytes = System.IO.File.ReadAllBytes(wwwPath + @"\faturas.xml");
            string fileName = "faturas.xml";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Xml, fileName);
        }

        // POST: Faturas/UploadFile/
        public async Task<ActionResult> UploadFile(IFormFile file)
        {
            try
            {
                // Create an instance of the XmlSerializer.
                XmlSerializer serializer = new XmlSerializer(typeof(List<Fatura>));
                // Declare an object variable of the type to be deserialized.
                List<Fatura> faturas;

                using (Stream reader = file.OpenReadStream())
                {
                    // Call the Deserialize method to restore the object's state.
                    faturas = (List<Fatura>)serializer.Deserialize(reader);
                    HttpClient client = new HttpClient();

                    var jsonObj = System.Text.Json.JsonSerializer.Serialize(faturas);
                    var content = new StringContent(jsonObj, Encoding.UTF8, "application/json");
                    var RespostaHTTP = await client.PostAsync(endpoint + "/api/Faturas", content);
                    if (RespostaHTTP.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        TempData["message"] = "Ocorreu um erro na inserção das faturas.\nXml encontra-se corretamente estruturado mas os seus dados contêm erros. Verifique se o ProdutoId das LinhasDeFaturas existem na Base de Dados.";
                    }
                    else
                    {
                        TempData["message"] = "Foram inseridas " + faturas.Count() + " faturas com sucesso.";
                    }

                }
            }
            catch (Exception ex)
            {
                TempData["message"] = "Ocorreu um erro na inserção das faturas. \nInnerException: " + ex.InnerException.ToString();
            }

            
            return RedirectToAction("Index");
        }


        // GET: Faturas/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            return await ViewWithObjInfo(id);
        }

       
        // POST: Faturas/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                HttpClient client = new HttpClient();
                var RespostaHTTP = await client.DeleteAsync(endpoint + "/api/Faturas/" + id);
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
            var RespostaHTTP = await client.GetAsync(endpoint + "/api/Faturas/" + id);

            if (RespostaHTTP.IsSuccessStatusCode)
            {
                var dadosJSON = await RespostaHTTP.Content.ReadAsStringAsync();
                var obj = JsonConvert.DeserializeObject<Fatura>(dadosJSON);
                return View(obj);
            }
            else
            {
                return Content("An error has occurred");
            }
        }
    }

   
    
}
