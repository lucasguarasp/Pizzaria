using Market.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Market.ViewModels
{
    public class ViewModelProduto
    {
        //public Produto Produto { get; set; }

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

        [Required]
        [Display(Name = "Categoria do produto")]
        public int CategoriaId { get; set; }

        [Display(Name = "Tamanho do produto")]
        public int? TamanhoId { get; set; }
                
        [Display(Name = "Quantidade")]
        public double? Quantidade { get; set; }

        //public IEnumerable<Categoria> Categorias { get; set; }
        //public IEnumerable<Insumo> Insumos { get; set; }
    }
}
