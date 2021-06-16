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
    public class BaseClienteValidator<T> : IBaseValidator<T> where T : ClienteModel
    {
        private ClienteRepository<Cliente> _clientesRepository = new ClienteRepository<Cliente>();
        private TipoClienteRepository<TipoCliente> _tipoClienteRepository = new TipoClienteRepository<TipoCliente>();
        private SituacaoClienteRepository<SituacaoCliente> _situacaoClienteRepository = new SituacaoClienteRepository<SituacaoCliente>();

        public virtual List<ValidationResult> Validate(T obj)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (obj.Nome != null)
                ValidaNome(obj.Nome, ref errors);

            if (obj.CPF != null)
                ValidaCPF(obj, ref errors);

            if (obj.Sexo != null)
                ValidaSexo(obj.Sexo, ref errors);

            if (obj.TipoClienteID != null)
                ValidaTipoClienteID(obj.TipoClienteID, ref errors);

            if (obj.SituacaoClienteID != null)
                ValidaSituacaoClienteID(obj.SituacaoClienteID, ref errors);

            return errors;
        }
        private void ValidaNome(string nome, ref List<ValidationResult> errors)
        {
            if (nome.Length < 2 || nome.Length > 50)
                errors.Add(new ValidationResult
                {
                    MemberName = "nome",
                    ErrorMessage = "O campo Nome deve conter entre 2 e 50 caracteres"
                });
        }
        private void ValidaCPF(T obj, ref List<ValidationResult> errors)
        {
            if (!ValidaCPF(obj.CPF))
            {
                errors.Add(new ValidationResult
                {
                    MemberName = "cpf",
                    ErrorMessage = "Campo CPF Inválido, forneça um CPF válido de 11 dígitos sem pontos e traços"
                });
                return;
            }

            bool isUnique = _clientesRepository.CheckIfUniqueCPF(obj.CPF);

            if (isUnique)
                return;

            if (!isUnique && obj.Id == null)
            {
                errors.Add(new ValidationResult
                {
                    MemberName = "cpf",
                    ErrorMessage = "Já existe um cadastro para esse CPF"
                });
            }

            if (!isUnique && obj.Id != null)
            {
                var oldClient = _clientesRepository.Buscar((int)obj.Id);

                if(oldClient.CPF != obj.CPF)
                    errors.Add(new ValidationResult
                    {
                        MemberName = "cpf",
                        ErrorMessage = "Já existe um cadastro para esse CPF"
                    });
            }
        }
        private void ValidaSexo(string sexo, ref List<ValidationResult> errors)
        {
            if (sexo != "F" && sexo != "M")
                errors.Add(new ValidationResult
                {
                    MemberName = "sexo",
                    ErrorMessage = "Campo Sexo inválido. Opções válidas: [M]/[F]"
                });
        }
        private void ValidaTipoClienteID(int? tipoClienteID, ref List<ValidationResult> errors)
        {
            var tipoCliente = _tipoClienteRepository.Buscar((int)tipoClienteID);
            if (tipoCliente == null)
            {
                errors.Add(new ValidationResult
                {
                    MemberName = "tipoClienteID",
                    ErrorMessage = "Campo TipoClienteID inválido."
                });
            }
        }
        private void ValidaSituacaoClienteID(int? situacaoClienteID, ref List<ValidationResult> errors)
        {
            var situacaoCliente = _situacaoClienteRepository.Buscar((int)situacaoClienteID);
            if (situacaoCliente == null)
            {
                errors.Add(new ValidationResult
                {
                    MemberName = "situacaoClienteID",
                    ErrorMessage = "Campo SituacaoClienteID inválido."
                });
            }
        }
        private static bool ValidaCPF(string cpf)
        {
            if (ContemNaoNumericos(cpf))
                return false;

            if (cpf.Length > 11)
                return false;

            while (cpf.Length != 11)
                cpf = '0' + cpf;

            bool igual = true;
            for (int i = 1; i < 11 && igual; i++)
                if (cpf[i] != cpf[0])
                    igual = false;

            if (igual || cpf == "12345678909")
                return false;

            int[] numeros = new int[11];

            for (int i = 0; i < 11; i++)
                numeros[i] = int.Parse(cpf[i].ToString());

            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];

            int resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                    return false;
            }
            else if (numeros[9] != 11 - resultado)
                return false;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += (11 - i) * numeros[i];

            resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)
                    return false;
            }
            else
                if (numeros[10] != 11 - resultado)
                return false;

            return true;
        }
        public static string RemoveNaoNumericos(string text)
        {
            Regex reg = new Regex(@"[^0-9]");
            string ret = reg.Replace(text, string.Empty);
            return ret;
        }
        public static bool ContemNaoNumericos(string text)
        {
            Regex reg = new Regex(@"[^0-9]");
            return reg.IsMatch(text);
        }
    }
}
