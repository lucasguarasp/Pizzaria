﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Market.Models
{
    public class Endereco
    {
        [Key]
        public int IdEndereco { get; set; }

        [Required]
        [Display(Name = "CEP")]
        public int Cep { get; set; }

        [Required]
        [Display(Name = "Rua")]
        public string Rua { get; set; }

        [Display(Name = "Número")]
        public int Numero { get; set; }         

        [Required]
        [Display(Name = "Cidade")]
        public string Cidade { get; set; }

        [Required]
        [Display(Name = "Bairro")]
        public string Bairro { get; set; }

        [Display(Name = "Complemento")]
        public int Complemento { get; set; }

        public virtual ICollection<Cadastro> Cadastros { get; set; }
    }
}
