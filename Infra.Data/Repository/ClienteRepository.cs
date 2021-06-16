using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Domain.Entities;
using Infra.Data.Context;

namespace Infra.Data.Repositoriy
{
    public class ClienteRepository<TEntity> : BaseRepository<TEntity>, IBaseRepository<TEntity> where TEntity : Cliente
    {
        public ClienteRepository()
        {
            nomeTabela = "Clientes";
        }
        public bool CheckIfUniqueCPF(string cpf)
        {
            var sqlParams = new SqlParameter[] { new SqlParameter("cpf", cpf) };
            var tabela = SQLServerContext.ExecutaProcSelect($"spUniqueClientesCPF", sqlParams);

            if (tabela.Rows.Count > 0)
                return (Convert.ToInt32(tabela.Rows[0][0])==0);
            else return false;
        }
        protected override TEntity MontaModel(DataRow registro)
        {
            Cliente cliente = new Cliente()
            {
                Id = (int)registro["Id"],
                Nome = registro["Nome"].ToString(),
                CPF = registro["CPF"].ToString(),
                TipoClienteID = (int)registro["TipoClienteID"],
                Sexo = registro["Sexo"].ToString(),
                SituacaoClienteID = (int)registro["SituacaoClienteID"]
            };
            return (TEntity)cliente;
        }
    }
}
