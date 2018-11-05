using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Market.Models
{
    public class ProdutoHasInsumo
    {
        [Key]
        public int IdProdutoHasInsumo { get; set; }

        [Required]
        [Display(Name = "Quantidade")]
        public double Quantidade { get; set; }

        //Fk Insumo
        public int InsumoId { get; set; }
        public virtual Insumo Insumo { get; set; }

        //Fk Produto
        public int ProdutoId { get; set; }
        public virtual Produto Produto { get; set; }
    }
}
