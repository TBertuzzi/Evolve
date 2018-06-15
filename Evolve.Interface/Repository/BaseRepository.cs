using System;
using System.Collections.Generic;
using System.Text;
using Evolve.Repository;

namespace Evolve.Interface.Repository
{
    public class BaseRepository<T>
    {
        public readonly Repository<T> Repository;

        public readonly IConfiguration _configuration;
        public BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;

            var conexao = new Conexao()
            {
                Database = (nFluxo.Repository.Utils.TipoDataBase)Convert.ToInt32(configuration.GetSection("ConnectionStrings:DataBaseNFluxo").Value),
                StringConexao = configuration.GetSection("ConnectionStrings:StringNFluxo").Value,
                ChaveCriptografia = configuration.GetSection("Parametros:ChaveCriptografia").Value
            };

            Repository = new Repository<T>(conexao);
        }
    }
}
