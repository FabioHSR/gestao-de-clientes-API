using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Domain.Entities;
using Infra.Data.Context;

namespace Infra.Data.Repositoriy
{
    public class SituacaoClienteRepository<TEntity> : BaseRepository<TEntity>, IBaseRepository<TEntity> where TEntity : SituacaoCliente
    {
        public SituacaoClienteRepository()
        {
            nomeTabela = "SituacoesCliente";
        }

        protected override TEntity MontaModel(DataRow registro)
        {
            SituacaoCliente entidade = new SituacaoCliente()
            {
                Id = (int)registro["Id"],
                Descricao = registro["Descricao"].ToString(),
            };
            return (TEntity)entidade;
        }
    }
}
