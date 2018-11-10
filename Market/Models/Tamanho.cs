using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Market.Models
{
    public class Tamanho
    {
        [Key]
        public int IdTamanho { get; set; }

        [Required]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        public int? CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }

    }
}
