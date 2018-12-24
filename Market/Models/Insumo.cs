using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Market.Models
{
    public class Insumo
    {
        [Key]
        public int IdInsumo { get; set; }

        [Required]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required]
        [Display(Name = "Quantidade")]
        public double Quantidade { get; set; }

        [Display(Name = "Preço do Insumo")]
        public double PrecoInsumo { get; set; }

        [Display(Name = "Esque máximo")]
        public double EstoqueMax { get; set; }       

    }
}
