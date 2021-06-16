using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class Cliente: BaseEntity
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public int? TipoClienteID { get; set; }
        public string Sexo { get; set; }
        public int? SituacaoClienteID { get; set; }
    }
}
