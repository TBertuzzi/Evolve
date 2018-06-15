using Evolve.Helpers.Security;
using Evolve.Repository.Utils;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;


namespace Evolve.Repository.DataBase
{
    public class Conexao
    {
        public string StringConexao { get; set; }
        public TipoDataBase Database { get; set; }
        public string ChaveCriptografia { get; set; }

        internal DbConnection DataBaseConnection
        {
            get
            {
                switch (Database)
                {
                    case TipoDataBase.SQLServer:
                        return new SqlConnection(Criptografia.Descriptografar(this.StringConexao, this.ChaveCriptografia));
                    case TipoDataBase.Oracle:
                        return null;
                    default: return null;
                }
            }
        }

        internal DbCommand GetCommand(string sql, DbConnection conexao)
        {
            switch (Database)
            {
                case TipoDataBase.SQLServer:
                    return new SqlCommand(sql, (SqlConnection)conexao);
                case TipoDataBase.Oracle:
                    return null;
                default: return null;
            }
        }

        internal DbDataAdapter GetDataAdpter()
        {
            switch (Database)
            {
                case TipoDataBase.SQLServer:
                    return new SqlDataAdapter();
                case TipoDataBase.Oracle:
                    return null;
                default: return null;
            }
        }

        internal object GetParameter(KeyValuePair<string, object> parametro)
        {
            switch (Database)
            {
                case TipoDataBase.SQLServer:
                    return new SqlParameter($"@{parametro.Key}", parametro.Value);
                case TipoDataBase.Oracle:
                    return null;
                default: return null;
            }
        }
    }
}
