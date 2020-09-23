using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL;
using DAL.Models;
using WebService.Repositories;

namespace WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoRepository _repository;


        public ProdutosController(IProdutoRepository repository)
        {
            _repository = repository;
        }


        // GET: api/Produtos
        [HttpGet]
        public IEnumerable<Produto> GetProdutos()
        {
            return _repository.GetProdutos();
        }


        // GET: api/Produtos/5
        [HttpGet("{id}")]
        public Produto GetProduto(int id)
        {
            return _repository.GetProduto(id);
        }


        // PUT: api/Produtos/5
        [HttpPut("{id}")]
        public Produto PutProduto(int id, Produto produto)
        {
            return _repository.PutProduto(id, produto);
        }

        // POST: api/Produtos
        [HttpPost]
        public ActionResult<Produto> PostProduto(Produto produto)
        {
            return _repository.PostProduto(produto);
        }

        // DELETE: api/Produtos/5
        [HttpDelete("{id}")]
        public bool DeleteProduto(int id)
        {
            return _repository.DeleteProduto(id);
        }

       
    }
}
