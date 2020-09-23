using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class Produto
    {
        [Key]
        public int ProdutoId { get; set; }


        [Display(Name = "Produto Pai")]
        public string ParentId { get; set; }


        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Por favor insira um nome para o produto.")]
        public string Nome { get; set; }


        [Display(Name = "Descrição")]
        public string Descricao { get; set; }


        [Display(Name = "Preço")]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Por favor insira valor em Euros.")]
        public decimal Preco { get; set; }


        [Display(Name = "Url da Imagem")]
        public string UrlImagem { get; set; }


        //Relacao one-to-many:   Uma Empregado tem muitos Produtos, mas um Produto só pode ter um Empregado.

        [Display(Name = "Criado Por")]
        public int EmpregadoId { get; set; }
        public Empregado Empregado { get; set; }


    }
}
