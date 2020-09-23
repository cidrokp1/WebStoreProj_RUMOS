using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class Empregado
    {

        [Key]
        public int EmpregadoId { get; set; }


        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Por favor insira o nome do empregado.")]
        public string Nome { get; set; }


        [Display(Name = "Email")]
        [Required(ErrorMessage = "Por favor insira um email válido.")]
        public string Email { get; set; }

     
    }
}
