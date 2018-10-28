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

        // A relação entre Cadastro e TipoDeUsuario é de 1 para N, então você precisa dizer isso ao EF.
        //public virtual ICollection<Cadastro> Cadastros { get; set; }

    }
}

