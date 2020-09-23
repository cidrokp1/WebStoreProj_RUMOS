using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Repositories
{
    public interface IProdutoRepository
    {

        IEnumerable<Produto> GetProdutos();

        Produto GetProduto(int id);

        Produto PostProduto(Produto newItem);

        Produto PutProduto(int id, Produto newItem);

        bool DeleteProduto(int id);

        bool ProdutoExists(int id);

    }
}
