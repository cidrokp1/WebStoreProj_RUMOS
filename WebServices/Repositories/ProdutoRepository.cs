using DAL.Data;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly DAL.Data.BD _context;

        public ProdutoRepository(BD context)
        {
            _context = context;
        }

        IEnumerable<Produto> IProdutoRepository.GetProdutos()
        {
            return  _context.Produtos
              .Include(produtos => produtos.Empregado)
              .ToList();
        }


        Produto IProdutoRepository.GetProduto(int id)
        {
            var produto =  _context.Produtos
                         .Include(produtos => produtos.Empregado)
                         .FirstOrDefault(produto => produto.ProdutoId == id);

            return produto;
        }


        public Produto PostProduto(Produto newItem)
        {
            _context.Add(newItem);
            int entries = _context.SaveChanges();
            if (entries > 0)
            {
                return newItem;
            }
            else
            {
                return null;
            }
        }

        public Produto PutProduto(int id, Produto produto)
        {
            if (id != produto.ProdutoId)
            {
                return null;
            }
            _context.Entry(produto).State = EntityState.Modified;

            try
            {
                 _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutoExists(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
            return null;
        }


        public bool DeleteProduto(int id)
        {
            var shirt = _context.Produtos.SingleOrDefault(m => m.ProdutoId == id);
            _context.Produtos.Remove(shirt);
            int entries = _context.SaveChanges();
            if (entries > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ProdutoExists(int id)
        {
            return _context.Produtos.Any(e => e.ProdutoId == id);
        }


    }
}
