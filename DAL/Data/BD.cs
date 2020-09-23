using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Data
{
    public class BD : DbContext
    {


        public BD(DbContextOptions<BD> options)
            : base(options)
        {
        }

        public BD()
        {
        }

        public DbSet<Empregado> Empregados { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Fatura> Faturas { get; set; }
        public DbSet<LinhasDeFatura> LinhasDeFaturas { get; set; }




        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=WebStore_Rumos_Proj;Trusted_Connection=True;MultipleActiveResultSets=true");


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Empregado>().HasData(
               new Empregado { Email = "kevin.lourenco@hotmail.com", EmpregadoId = 1, Nome = "Kevin Lourenco" },
               new Empregado { Email = "cenas@hotmail.com", EmpregadoId = 2, Nome = "Teste" }
            );
            modelBuilder.Entity<Produto>().HasData(
               new Produto { ProdutoId = 1, Nome = "24 LÁPIS FABER CASTELL", Descricao = "Proteção contra quebra graças ao SV. Pode ser afiado com qualquer apontador de boa qualidade.", UrlImagem = "https://craftelier.com/media/catalog/product/cache/a566829c02a992af82b1c47a69471133/c/a/caja_met_lica_24_l_pices_faber_castell_3_1.jpg", Preco = (decimal)14.19, EmpregadoId = 1 },
               new Produto { ProdutoId = 1002, Nome = "Mala Homem", Descricao = "Jovem, urbana e colorida, é a mais recente coleção de mochilas American Tourister. Tu escolhes como a usas: para as tuas viagens ou para levares o teu portátil para a faculdade ou escritório.", UrlImagem = "https://www.americantourister.pt/op/image/?cb=98948&h=71523&st=4", Preco = (decimal)60.00, EmpregadoId = 2 },
               new Produto { ProdutoId = 1003, Nome = "Mala Mulher", Descricao = "Mala de mulher grande para viagens. Disponível já este outono. Não perca esta oportunidade", UrlImagem = "https://mo-online.com/on/demandware.static/-/Sites-master-catalog/default/dw67831ef1/images/hires/0e69a81a-10b6-407d-af58-43d8ea0dfaa4.jpg", Preco = (decimal)20.00, EmpregadoId = 1 },
               new Produto { ProdutoId = 1004, Nome = "Caderno", Descricao = "Faça Caderno capa rígida Personalizado online. Adicione um design criativo utilizando um dos nosso templates ou faça upload do seu design.", Preco = (decimal)7.99, UrlImagem = "https://www.360imprimir.pt/caderno-a-folhas-lisas-transf-x-mm-cdbg.png", EmpregadoId = 2 },
               new Produto { ProdutoId = 1005, Nome = "Estojo", Descricao = "O estojo Eastpak Oval Single é ideal para transportar todas as suas ferramentas de estudo, graças ao seu grande compartimento individual com muito espaço. ", UrlImagem = "https://media.deporvillage.com/w_1800,f_auto,q_auto,c_pad,b_white/product/eap-ek717008.jpg", Preco = (decimal)20.00, EmpregadoId = 2 }

            );


            

        }




    }
}

 