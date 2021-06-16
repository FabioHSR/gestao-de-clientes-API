using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class CreateClienteModel
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public int? TipoClienteID { get; set; }
        public string Sexo { get; set; }
        public int? SituacaoClienteID { get; set; }
    }
}
