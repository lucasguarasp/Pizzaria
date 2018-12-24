using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Market.Models
{
    //Essa classe foi adicionada devido um insumo ter 1 ou mais categorias
    public class InsumoHasCategoria
    {        
        [Key]
        public int IdInsumoHasCategoria { get; set; }

        //Fk Insumo
        public int InsumoId { get; set; }
        public virtual Insumo Insumo { get; set; }

        //Fk Categoria
        public int CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }
    }
}
