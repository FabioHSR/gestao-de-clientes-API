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
    public class TipoClienteRepository<TEntity> : BaseRepository<TEntity>, IBaseRepository<TEntity> where TEntity : TipoCliente
    {
        public TipoClienteRepository()
        {
            nomeTabela = "TiposCliente";
        }

        protected override TEntity MontaModel(DataRow registro)
        {
            TipoCliente tipoCliente = new TipoCliente()
            {
                Id = (int)registro["Id"],
                Descricao = registro["Descricao"].ToString(),
            };
            return (TEntity)tipoCliente;
        }
    }
}
