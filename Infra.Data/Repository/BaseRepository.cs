using Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;


namespace Infra.Data.Repositoriy
{
    public abstract class BaseRepository<TEntity>
    {
        public string nomeTabela { get; set; }
        public TEntity Inserir(TEntity obj)
        {
            var sqlParams = CriaParametros(obj);
            var tabela = SQLServerContext.ExecutaProcSelect($"spInsert{nomeTabela}", sqlParams.ToArray());

            if (tabela.Rows.Count > 0)
                return MontaModel(tabela.Rows[0]);
            else return default(TEntity);
        }
        public IEnumerable<TEntity> Listar()
        {
            var tabela = SQLServerContext.ExecutaProcSelect($"spGet{nomeTabela}", null);

            List<TEntity> lista = new List<TEntity>();
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaModel(registro));

            return lista;
        }
        public TEntity Buscar(int id)
        {
            var tabela = SQLServerContext.ExecutaProcSelect($"spGet{nomeTabela}ByID", new SqlParameter[] { new SqlParameter("ID", id) });

            if (tabela.Rows.Count > 0)
                return MontaModel(tabela.Rows[0]);
            else return default(TEntity);
        }
        public TEntity Atualizar(TEntity obj)
        {
            var sqlParams = CriaParametros(obj);
            var tabela = SQLServerContext.ExecutaProcSelect($"spUpdate{nomeTabela}", sqlParams.ToArray());

            if (tabela.Rows.Count > 0)
                return MontaModel(tabela.Rows[0]);
            else return default(TEntity);
        }
        public void Deletar(int id)
        {
            SQLServerContext.ExecutaProc($"spDelete{nomeTabela}ByID", new SqlParameter[] { new SqlParameter("ID", id) });
        }
        protected SqlParameter[] CriaParametros(TEntity obj)
        {
            if (obj == null) return null;

            Type t = obj.GetType();
            PropertyInfo[] props = t.GetProperties();
            var sqlParams = new List<SqlParameter>();
            foreach (var prop in props)
            {
                var value = prop.GetValue(obj);
                if (value != null)
                    sqlParams.Add(new SqlParameter(prop.Name, value));
            }
            return sqlParams.ToArray();
        }
        protected abstract TEntity MontaModel(DataRow registro);
    }
}
