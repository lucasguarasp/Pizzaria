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
        public Produto Produto { get; set; }

        public IEnumerable<Categoria> Categorias { get; set; }

        public IEnumerable<Insumo> Insumos { get; set; }
    }
}
