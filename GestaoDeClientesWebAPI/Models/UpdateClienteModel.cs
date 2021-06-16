using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoDeClientesWebAPI.Models
{
    public class UpdateClienteModel : IModel
    {
        //[Required(ErrorMessage = "Id é obrigatório")]
        public int? Id { get; set; }

        //[MaxLength(ErrorMessage = "Nome deve conter no máximo 50 caracteres")]
        public string Nome { get; set; }

        //[StringLength(11, ErrorMessage = "CPF deve conter 11 dígitos")]
        public string CPF { get; set; }
        public int? TipoClienteID { get; set; }
        public string Sexo { get; set; }
        public int? SituacaoClienteID { get; set; }
    }
}
