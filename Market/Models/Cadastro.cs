﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Market.Models
{
    public class Cadastro
    {
        [Key]
        public int IdCadastro { get; set; }

        [Required]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "CPF")]
        public int Cpf { get; set; }

        [Required]
        [Display(Name = "Data de Nascimento")]
        public DateTime DataNascimento { get; set; }

        [Required]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Display(Name = "Telefone")]
        public int Telefone { get; set; }

        [Display(Name = "Celular")]
        public int Celular { get; set; }

        [Display(Name = "Sexo")]
        public string Sexo { get; set; }

        [Required]
        [Display(Name = "Status do Cadastro")]
        public bool Status { get; set; }

        public int TipoDeUsuarioId { get; set; } //[ForeignKey] não precisa ser usada aqui.O EF é capaz de deduzir a chave estrangeira sozinho.
        public virtual TipoDeUsuario TipoDeUsuario { get; set; } //Especifique virtual para apontar para o EF que é ele que deve preencher a propriedade.



        public int EnderecoId { get; set; } 
        public virtual Endereco Endereco { get; set; }

    }
}
