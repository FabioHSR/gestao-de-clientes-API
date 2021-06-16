using Domain.Entities;
using Domain.Models;
using FluentValidation;
using Infra.Data.Repositoriy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Service.Validators
{
    public class CreateClienteValidator<T> : BaseClienteValidator<T>, IBaseValidator<T> where T : ClienteModel
    {
        public override List<ValidationResult> Validate(T obj)
        {
            List<ValidationResult> errors = base.Validate(obj);

            ValidaNome(obj.Nome, ref errors);
            ValidaCPF(obj.CPF, ref errors);
            ValidaSexo(obj.Sexo, ref errors);
            ValidaTipoClienteID(obj.TipoClienteID, ref errors);
            ValidaSituacaoClienteID(obj.SituacaoClienteID, ref errors);

            return errors;
        }
        private void ValidaNome(string nome, ref List<ValidationResult> errors)
        {
            if (String.IsNullOrEmpty(nome))
                errors.Add(new ValidationResult
                {
                    MemberName = "nome",
                    ErrorMessage = "Campo Nome é obrigatório"
                });
        }
        private void ValidaCPF(string cpf, ref List<ValidationResult> errors)
        {
            if (String.IsNullOrEmpty(cpf))
                errors.Add(new ValidationResult
                {
                    MemberName = "cpf",
                    ErrorMessage = "Campo CPF é obrigatório"
                });
        }
        private void ValidaSexo(string sexo, ref List<ValidationResult> errors)
        {
            if (String.IsNullOrEmpty(sexo))
                errors.Add(new ValidationResult
                {
                    MemberName = "sexo",
                    ErrorMessage = "Campo Sexo é obrigatório"
                });
        }
        private void ValidaTipoClienteID(int? tipoClienteID, ref List<ValidationResult> errors)
        {
            if (tipoClienteID == null)
                errors.Add(new ValidationResult
                {
                    MemberName = "tipoClienteID",
                    ErrorMessage = "Campo TipoClienteID é obrigatório"
                });
        }
        private void ValidaSituacaoClienteID(int? situacaoClienteID, ref List<ValidationResult> errors)
        {
            if (situacaoClienteID == null)
                errors.Add(new ValidationResult
                {
                    MemberName = "situacaoClienteID",
                    ErrorMessage = "Campo SituacaoClienteID é obrigatório"
                });
        }
    }
}
