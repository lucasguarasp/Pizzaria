using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Market.Models
{
    public class HistoricoInsumo
    {
        [Key]
        public int IdHistoricoInsumo { get; set; }

        [Required]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required]
        [Display(Name = "Quantidade")]
        public double Quantidade { get; set; }

        [Display(Name = "Preço do Insumo")]
        public double PrecoInsumo { get; set; }        

        [Required]
        [Display(Name = "Data de Adição do Insumo")]
        public DateTime DataAdicao { get; set; }
    }
}
