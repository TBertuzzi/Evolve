using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Dapper.Contrib.Extensions;
using Dapper;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Evolve.Repository.DataBase;

namespace Evolve.Repository.Repository
{
    public class Repository<T> : IRepository<T>
    {
        private Conexao _conexao;
        public Repository(Conexao conexao)
        {
            _conexao = conexao;
        }

        #region DAPPER

        public async Task AtualizarAsync(T item)
        {
            using (DbConnection conexao = _conexao.DataBaseConnection)
            {
                StringBuilder sql = new StringBuilder();

                Dictionary<string, object> parametros = new Dictionary<string, object>();

                sql.AppendLine($"update {typeof(T).Name} set ");

                foreach (var P in item.GetType().GetProperties())
                {
                    if (item.GetType().GetProperty(P.Name) != null)
                    {
                        var dapperAttribute = P.CustomAttributes.Count() > 0 ? P.CustomAttributes.ToList()[0].AttributeType.ToString() : "";
                        if (P.Name.ToUpper() != "ID" && dapperAttribute.ToUpper() != "NFLUXO.REPOSITORY.ATTRIBUTES.DAPPERIGNORE")
                        {
                            sql.Append($" {P.Name} = @{P.Name},");
                        }
                        parametros.Add($"@{P.Name}", item.GetType().GetProperty(P.Name).GetValue(item));
                    }
                }

                sql.Remove(sql.Length - 1, 1);
                sql.AppendLine(" where id = @ID");

                await conexao.ExecuteAsync(sql.ToString(), parametros);
            }
        }

        public void Atualizar(T item)
            => AtualizarAsync(item).Wait();


        public async Task<int> InserirAsync(T item, bool identity)
        {
            using (DbConnection conexao = _conexao.DataBaseConnection)
            {
                StringBuilder sql = new StringBuilder();

                Dictionary<string, object> parametros = new Dictionary<string, object>();

                sql.AppendLine($"insert into {typeof(T).Name} values (");

                foreach (var P in item.GetType().GetProperties())
                {
                    if (item.GetType().GetProperty(P.Name) != null)
                    {
                        var dapperAttribute = P.CustomAttributes.Count() > 0 ? P.CustomAttributes.ToList()[0].AttributeType.ToString() : "";
                        if (P.Name.ToUpper() != "ID" && dapperAttribute.ToUpper() != "NFLUXO.REPOSITORY.ATTRIBUTES.DAPPERIGNORE")
                        {
                            sql.Append($"@{P.Name},");
                            parametros.Add($"@{P.Name}", item.GetType().GetProperty(P.Name).GetValue(item));
                        }
                    }
                }

                sql.Remove(sql.Length - 1, 1);
                sql.AppendLine(");");

                if (identity)
                {
                    sql.AppendLine("SELECT CAST(SCOPE_IDENTITY() as int);");
                    return conexao.QuerySingleOrDefault<int>(sql.ToString(), parametros);
                }
                else
                {
                    return await conexao.ExecuteAsync(sql.ToString(), parametros);
                }
            }
        }

        public int Inserir(T item, bool identity) =>
            InserirAsync(item, identity).Result;


        public async Task<IEnumerable<T>> ObterAsync()
        {
            using (DbConnection conexao = _conexao.DataBaseConnection)
            {
                return await conexao.QueryAsync<T>($"Select * from {typeof(T).Name} ");
            }
        }

        public IEnumerable<T> Obter()
            => ObterAsync().Result;


        public async Task<IEnumerable<T>> ObterAsync(string sql, Dictionary<string, object> parametros)
        {

            using (DbConnection conexao = _conexao.DataBaseConnection)
            {
                return await conexao.QueryAsync<T>(sql, parametros);
            }
        }

        public IEnumerable<T> Obter(string sql, Dictionary<string, object> parametros)
            => ObterAsync(sql, parametros).Result;


        public async Task<T> ObterPorIDAsync(object id)
        {
            using (DbConnection conexao = _conexao.DataBaseConnection)
            {
                return await conexao.QueryFirstOrDefaultAsync<T>($"Select * from {typeof(T).Name} where id = @ID ", new { ID = id });
            }
        }

        public T ObterPorID(object id)
            => ObterPorIDAsync(id).Result;


        public async Task DeletarAsync(object id)
        {
            using (DbConnection conexao = _conexao.DataBaseConnection)
            {
                StringBuilder sql = new StringBuilder();

                Dictionary<string, object> parametros = new Dictionary<string, object>
                {
                    { "@ID", id }
                };

                sql.AppendLine($"delete from {typeof(T).Name} where id = @ID");

                await conexao.ExecuteAsync(sql.ToString(), parametros);
            }
        }

        public void Deletar(T item)
            => DeletarAsync(item).Wait();

        public object ExecuteScalar(string sql, Dictionary<string, object> parametros)
            => ExecuteScalarAsync(sql, parametros).Result;


        public async Task<object> ExecuteScalarAsync(string sql, Dictionary<string, object> parametros)
        {
            using (DbConnection conexao = _conexao.DataBaseConnection)
            {
                return await conexao.ExecuteScalarAsync(sql, parametros);
            }
        }

        #endregion

        #region ADO


        public DataSet ObterDataSet(string sql, Dictionary<string, object> parametros)
        {
            using (DbConnection conexao = _conexao.DataBaseConnection)
            {
                using (DbCommand Cmd = _conexao.GetCommand(sql, conexao))
                {
                    Cmd.Parameters.Clear();
                    Cmd.CommandTimeout = 120;

                    foreach (KeyValuePair<string, object> parametro in parametros)
                    {
                        Cmd.Parameters.Add(_conexao.GetParameter(parametro));
                    }

                    using (DbDataAdapter da = _conexao.GetDataAdpter())
                    {
                        da.SelectCommand = Cmd;

                        using (DataSet ds = new DataSet())
                        {
                            da.Fill(ds);
                            return ds;
                        }
                    }
                }
            }
        }

        public async Task<int> ExecutaQueryAsync(string sql, Dictionary<string, object> parametros)
        {
            using (DbConnection conexao = _conexao.DataBaseConnection)
            {
                return await conexao.ExecuteAsync(sql, parametros);
            }
        }

        public int ExecutaQuery(string sql, Dictionary<string, object> parametros)
            => ExecutaQueryAsync(sql, parametros).Result;

        #endregion

    }
}
