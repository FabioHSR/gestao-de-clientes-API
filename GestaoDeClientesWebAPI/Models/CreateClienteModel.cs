using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoDeClientesWebAPI.Models
{
    public class CreateClienteModel : IModel
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        [MaxLength(ErrorMessage = "Nome deve conter no máximo 50 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "CPF é obrigatório")]
        [StringLength(11, ErrorMessage = "CPF deve conter 11 dígitos")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "TipoClienteID é obrigatório")]
        public int? TipoClienteID { get; set; }

        [Required(ErrorMessage = "Sexo é obrigatório")]
        public string Sexo { get; set; }

        [Required(ErrorMessage = "SituacaoClienteID é obrigatório")]
        public int? SituacaoClienteID { get; set; }
    }
}
