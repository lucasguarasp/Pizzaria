using Market.Models;
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
        [MinLength(8)]
        [MaxLength(30)]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        public Cadastro Entrar(ApplicationDbContext _db)
        {
            return _db.Cadastros.Count(u => u.Email.Equals(Login) || u.Cpf.Equals(Login)) > 0 ? _db.Cadastros.Single(u => u.Email.Equals(Login) || u.Cpf.Equals(Login)) : null;
        }

    }
}
