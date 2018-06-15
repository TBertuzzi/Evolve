using System;
using System.Collections.Generic;
using System.Text;

namespace Evolve.Repository.Repository
{
    public interface ILiteDBBase
    {
        int Id { get; set; }
        DateTime Atualizacao { get; set; }
    }
}
