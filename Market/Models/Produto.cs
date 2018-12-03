using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Market.Models
{
    public class Produto
    {
        [Key]
        public int IdProduto { get; set; }

        [Required]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required]
        [Display(Name = "Valor do produto")]
        public double Valor { get; set; }

        [Required]
        [Display(Name = "Descrição do produto")]
        public string Descricao { get; set; }
        
        [Display(Name = "Foto do produto")]
        public string Foto { get; set; }        
        
        [Display(Name = "Categoria do produto")]
        public int CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }        

        [Display(Name = "Tamanho do produto")]
        public int? TamanhoId { get; set; }
        public virtual Medida Tamanho { get; set; }

    }
}
