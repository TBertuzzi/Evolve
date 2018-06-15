using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
namespace Evolve.Repository.Repository
{
    internal interface IRepository<T>
    {
        //Objetos Genericos para Utilização do Dapper
        IEnumerable<T> Obter();
        Task<IEnumerable<T>> ObterAsync();
        IEnumerable<T> Obter(string sql, Dictionary<string, object> parametros);
        Task<IEnumerable<T>> ObterAsync(string sql, Dictionary<string, object> parametros);
        T ObterPorID(object id);
        Task<T> ObterPorIDAsync(object id);
        int Inserir(T item, bool identity);
        Task<int> InserirAsync(T item, bool identity);
        void Atualizar(T item);
        Task AtualizarAsync(T item);
        object ExecuteScalar(string sql, Dictionary<string, object> parametros);
        Task<object> ExecuteScalarAsync(string sql, Dictionary<string, object> parametros);

        //Dataset padrão do ADO.net
        DataSet ObterDataSet(string sql, Dictionary<string, object> parametros);
        int ExecutaQuery(string sql, Dictionary<string, object> parametros);
    }
}
