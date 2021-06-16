using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Infra.Data.Repositoriy
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        TEntity Inserir(TEntity obj);

        TEntity Atualizar(TEntity obj);

        void Deletar(int id);

        IEnumerable<TEntity> Listar();

        TEntity Buscar(int id);
    }
}
