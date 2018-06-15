using Evolve.Helpers;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evolve.Repository.Repository
{
    public class LiteDBConnection<T> where T : ILiteDBBase
    {
        public LiteRepository _liteRepository;
        public LiteDBConnection()
        {

            _liteRepository = new LiteRepository(Constantes.LITEDBNAME);
        }

        public virtual IEnumerable<T> ObterTodos()
        {
            var games = _liteRepository.Query<T>().ToEnumerable();
            return games;
        }

        public virtual void Inserir(T item)
        {
            _liteRepository.Insert<T>(item);
        }

        public virtual void Atualizar(T item)
        {
            _liteRepository.Update<T>(item);
        }

        public virtual T ObterPorID(int id)
        {
            var objeto = _liteRepository.Query<T>().Where(x => x.Id == id).ToEnumerable().FirstOrDefault();
            return objeto;
        }
    }
}
