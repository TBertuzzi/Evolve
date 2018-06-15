using System;
using System.Collections.Generic;
using System.Text;

namespace Evolve.Helpers.Security
{
    public class Token
    {
        #region Propriedades Publicas

        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public ApiIdentifier ApiIdentifier { get; set; }
        public bool Authenticated { get; set; }
        public string Created { get; set; }
        public string Expiration { get; set; }
        public string AccessToken { get; set; }
        public string Message { get; set; }
        public bool IsExpirado => ValidarExpirado();

        #endregion

        #region Metodos Privados

        private bool ValidarExpirado()
        {
            if (string.IsNullOrEmpty(Expiration))
                return true;

            DateTime dataExpiracao;
            var isSuccess = DateTime.TryParse(Expiration, out dataExpiracao);

            if (!isSuccess)
                return true;

            if (DateTime.Now > dataExpiracao)
                return true;

            return false;
        }

        #endregion
    }
}
