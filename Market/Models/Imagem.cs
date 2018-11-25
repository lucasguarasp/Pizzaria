using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Models
{
    public class Imagem
    {
        public int ImagemId { get; set; }
        public string Nome { get; set; }
        public string Caminho { get; set; }
        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime DataUpload { get; set; }
    }
}
