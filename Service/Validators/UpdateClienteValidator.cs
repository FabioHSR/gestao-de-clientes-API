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
    public class UpdateClienteValidator<T> : BaseClienteValidator<T>, IBaseValidator<T> where T : ClienteModel
    {
        private IBaseRepository<TipoCliente> _tipoClienteRepository = new TipoClienteRepository<TipoCliente>();


        public override List<ValidationResult> Validate(T obj)
        {
            List<ValidationResult> errors = base.Validate(obj);
            ValidaID(obj.Id, ref errors);
            return errors;
        }

        private void ValidaID(int? id, ref List<ValidationResult> errors)
        {
            if (id == null)
                errors.Add(new ValidationResult
                {
                    MemberName = "id",
                    ErrorMessage = "ID é obrigatório"
                });
        }
    }
}
