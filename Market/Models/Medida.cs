using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Market.Models
{
    public class Medida
    {
        [Key]
        public int IdMedida { get; set; }

        [Required]
        [Display(Name = "Nome da medida")]
        public string Nome { get; set; }

        [Required]
        [Display(Name = "Sigla")]
        public string Sigla { get; set; }

        public int CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }
    }
}
