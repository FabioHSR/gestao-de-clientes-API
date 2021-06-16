using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class TipoCliente : BaseEntity
    {
        public string Descricao { get; set; }
    }
}
