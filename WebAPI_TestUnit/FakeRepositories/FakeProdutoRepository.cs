using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using WebService.Repositories;

namespace WebServices_TestUnit.FakeRepositories
{
    class FakeProdutoRepository : IProdutoRepository
    {
        private List<Produto> ListProdutos;

        public FakeProdutoRepository()
        {
            ListProdutos = new List<Produto>()
            {
                new Produto { ProdutoId = 1, Nome = "Produto1" , Descricao = "descricao1", Preco = (decimal)20.00, EmpregadoId = 1 },
                new Produto { ProdutoId = 2, Nome = "Produto2" , Descricao = "descricao2", Preco = (decimal)72.00, EmpregadoId = 1 },
                new Produto { ProdutoId = 3, Nome = "Produto3" , Descricao = "descricao3", Preco = (decimal)34.00, EmpregadoId = 1 }
            }; ;
        }

        public IEnumerable<Produto> GetProdutos()
        {
            return ListProdutos;
        }

        public Produto GetProduto(int id)
        {
            foreach (var item in ListProdutos)
            {
                if (item.ProdutoId == id)
                {
                    return item;
                }
            }
            return null;
        }

        public Produto PostProduto(Produto newItem)
        {
            newItem.ProdutoId = 10;
            ListProdutos.Add(newItem);

            return newItem;
        }

        public Produto PutProduto(int id, Produto newItem)
        {
            throw new NotImplementedException();
        }

        public bool DeleteProduto(int id)
        {
            foreach (var item in ListProdutos)
            {
                if (item.ProdutoId == id)
                {
                    ListProdutos.Remove(item);
                    return true;
                }
            }
            return false;
        }

        public bool ProdutoExists(int id)
        {
            return true;
        }
    }
}
