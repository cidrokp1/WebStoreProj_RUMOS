using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class LinhasDeFatura
    {

        [Key]
        public int LinhasDeFaturaId { get; set; }


        [Display(Name = "Quantidade")]
        [Required(ErrorMessage = "Por favor insira uma quantidade válida.")]
        public int Quantidade { get; set; }



        [Display(Name = "Produto")]
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }



        [DataType(DataType.Currency)]
        [Display(Name = "Preço")]
        public decimal PrecoTotal { get; set; }



        
    }
}