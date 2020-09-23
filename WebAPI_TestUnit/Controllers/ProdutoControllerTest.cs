using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebService.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using WebService.Repositories;
using DAL.Models;
using WebServices_TestUnit.FakeRepositories;
using Microsoft.AspNetCore.Mvc;

namespace WebService.Controllers.Tests
{
    [TestClass()]
    public class ProdutoControllerTest
    {

        [TestMethod]
        public void IndexModelShouldContainAllProducts()
        {
            // Arrange
            IProdutoRepository fakeShirtRepository = new FakeProdutoRepository();
            ProdutosController produtosController = new ProdutosController(fakeShirtRepository);
            // Act
            var list = produtosController.GetProdutos() as List<Produto>;
            // Assert
            Assert.AreEqual(list.Count, 3);
        }

      
        [TestMethod]
        public void GetProductShouldContainTheRightProduct()
        {
            // Arrange
            IProdutoRepository fakeShirtRepository = new FakeProdutoRepository();
            ProdutosController produtosController = new ProdutosController(fakeShirtRepository);
            // Act
            var item = produtosController.GetProduto(1) as Produto;
            //// Assert
            Assert.AreEqual(item.ProdutoId, 1);

        }


        [TestMethod]
        public void GetRightAmountOfProductsAfterDelete()
        {
            // Arrange
            IProdutoRepository fakeShirtRepository = new FakeProdutoRepository();
            ProdutosController produtosController = new ProdutosController(fakeShirtRepository);
            // Act
            var list = produtosController.GetProdutos() as List<Produto>;
            int numProdutos = list.Count;
            produtosController.DeleteProduto(1);
            list = produtosController.GetProdutos() as List<Produto>;
            int numProdutosAfterDelete = list.Count;

            //// Assert
            Assert.AreEqual(numProdutos-numProdutosAfterDelete, 1);

        }



        [TestMethod]
        public void GetRightProductIdAfterPostProduct()
        {
            // Arrange
            IProdutoRepository fakeShirtRepository = new FakeProdutoRepository();
            ProdutosController produtosController = new ProdutosController(fakeShirtRepository);
            // Act
            var novo = produtosController.PostProduto(new Produto { Nome = "Produto10", Descricao = "descricao10", Preco = (decimal)80.00, EmpregadoId = 1 }) as ActionResult<Produto>;
           
            //// Assert
            Assert.AreEqual(novo.Value.ProdutoId, 10);

        }
    }
}