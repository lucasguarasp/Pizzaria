using Market.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Market.ViewModels
{
    public class ViewModelUsuarioLogin
    {
        [NotMapped]
        [Required]
        [MinLength(4)]
        public string Login { get; set; }
        [Required]
        [MinLength(6)]
        [MaxLength(30)]
        [DataType(DataType.Password)]
        public string Senha { get; set; }        

    }
}
