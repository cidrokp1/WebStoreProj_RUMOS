using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace DAL.Models
{
    
    public class Fatura
    {

        public int FaturaId { get; set; }


        [Display(Name = "Número")]
        [Required(ErrorMessage = "Por favor insira o número da fatura.")]
        public string NumeroFatura { get; set; }


        [Display(Name = "Data")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Por favor insira uma data válida.")]
        public DateTime Data { get; set; }



        [DataType(DataType.Currency)]
        [Display(Name = "Preço Total")]
        public decimal PrecoTotal { get; set; }



        [Display(Name = "Empregado")]
        public int EmpregadoId { get; set; }
        public Empregado Empregado { get; set; }



        [Display(Name = "Linhas de Fatura")]
        public List<LinhasDeFatura> LinhasDeFatura { get; set; }


        public override string ToString() 
        {
            return "Fatura : \nData: " + this.Data +
                "\nNumFatura: " + this.NumeroFatura +
                "\nEmpregado: " + this.Empregado.Nome +
                 "\nPrecoTotal: " + this.PrecoTotal;

        }


    }
}
