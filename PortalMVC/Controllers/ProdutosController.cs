using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace PortalMVC.Controllers
{
    [Authorize]
    public class ProdutosController : Controller
    {
        public const string endpoint = "http://localhost:63233";

        // GET: Produtos
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            HttpClient client = new HttpClient();
            var RespostaHTTP = await client.GetAsync(endpoint + "/api/Produtos");

            if (RespostaHTTP.IsSuccessStatusCode)
            {
                var dadosJSON = await RespostaHTTP.Content.ReadAsStringAsync();
                var obj = JsonConvert.DeserializeObject<List<Produto>>(dadosJSON);
                return View(obj);
            }
            else
            {
                return Content("An error has occurred");
            }

        }



        // GET: Produtos/Details/5
        [AllowAnonymous]
        public async Task<ActionResult> Details(int id)
        {
            return await ViewWithObjInfo(id);
        }


        // GET: Produtos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Produtos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("ProdutoId,Nome,Descricao,Preco,UrlImagem,EmpregadoId")] Produto produto)
        {
            try
            {
                //email do user logado
                var email = User.FindFirstValue(ClaimTypes.Name);

                //Passa o id do Empregado logado para o produto a ser criado (caso este empregado exista na BD)
                int empregadoId = await EmpregadosController.GetUserId(email);
                if (empregadoId != -1)
                {
                    produto.EmpregadoId = empregadoId;
                }
                //caso o empregado nao exista, cria uma entrada na BD para esse empregado e passa o seu id para o empregado do produto a ser criado
                else
                {
                    Empregado newEmpregado = new Empregado { Email = email, Nome = email };
                    produto.EmpregadoId = await EmpregadosController.CreateNew(newEmpregado);
                }

                HttpClient client = new HttpClient();
                var jsonObj = System.Text.Json.JsonSerializer.Serialize(produto);
                var content = new StringContent(jsonObj, Encoding.UTF8, "application/json");
                var RespostaHTTP = await client.PostAsync(endpoint + "/api/Produtos", content);
                var dadosJSON = await RespostaHTTP.Content.ReadAsStringAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        // GET: Produtos/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var email = User.FindFirstValue(ClaimTypes.Name);
            bool isValid = await ProdutosController.IsAuthorOfProduct(email, id);
            if (isValid)
            {
                return await ViewWithObjInfo(id);
            }
            TempData["message"] = "Não tem permissões para alterar o respectivo produto.";
            return RedirectToAction(nameof(Index));
        }



        // POST: Produtos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("ProdutoId,Nome,Descricao,Preco,UrlImagem,EmpregadoId")] Produto produto)
        {
            try
            {
                var email = User.FindFirstValue(ClaimTypes.Name);

                int empregadoId = await EmpregadosController.GetUserId(email);
                if (empregadoId != -1)
                {
                    produto.EmpregadoId = empregadoId;
                }
                produto.ProdutoId = id;
                HttpClient client = new HttpClient();
                var jsonObj = System.Text.Json.JsonSerializer.Serialize(produto);
                var content = new StringContent(jsonObj, Encoding.UTF8, "application/json");
                var RespostaHTTP = await client.PutAsync(endpoint + "/api/Produtos/" + id, content);
                var dadosJSON = await RespostaHTTP.Content.ReadAsStringAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        // GET: Produtos/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var email = User.FindFirstValue(ClaimTypes.Name);
            bool isValid = await ProdutosController.IsAuthorOfProduct(email, id);
            if (isValid)
            {
                return await ViewWithObjInfo(id);
            }
            TempData["message"] = "Não tem permissões para apagar o respectivo produto.";
            return RedirectToAction(nameof(Index));
        }


        // POST: Produtos/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id ,IFormCollection collection)
        {
            try
            {
                HttpClient client = new HttpClient();
                var RespostaHTTP = await client.DeleteAsync(endpoint + "/api/Produtos/"+id);
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
            var RespostaHTTP = await client.GetAsync(endpoint + "/api/Produtos/" + id);

            if (RespostaHTTP.IsSuccessStatusCode)
            {
                var dadosJSON = await RespostaHTTP.Content.ReadAsStringAsync();
                var obj = JsonConvert.DeserializeObject<Produto>(dadosJSON);
                return View(obj);
            }
            else
            {
                return Content("An error has occurred");
            }
        }


        private static async Task<bool> IsAuthorOfProduct(string email, int id)
        {
            HttpClient client = new HttpClient();
            var RespostaHTTP = await client.GetAsync(endpoint + "/api/Produtos/" + id);
            var dadosJSON = await RespostaHTTP.Content.ReadAsStringAsync();

            var obj = JsonConvert.DeserializeObject<Produto>(dadosJSON);
            
            if (obj.Empregado.Email.Equals(email))
            {
                return true;
            }
            return false;
        }

    }
}
