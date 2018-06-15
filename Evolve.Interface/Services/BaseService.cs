using System;
using System.Collections.Generic;
using System.Text;

namespace Evolve.Interface.Services
{
    public class BaseService
    {
        public static T CastModel<T>(object Data)
        {
            T RET = (T)Activator.CreateInstance(typeof(T));

            foreach (var P in RET.GetType().GetProperties())
            {
                if (Data.GetType().GetProperty(P.Name) != null)
                    P.SetValue(RET, Data.GetType().GetProperty(P.Name).GetValue(Data));
            }

            return RET;
        }
    }
}
