using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Market.Models
{
    public class TipoDeUsuario
    {
        [Key]
        public int IdTipoDeUsuario { get; set; }

        [Required]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }


    }
}

