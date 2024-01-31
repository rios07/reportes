using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.interfaces
{
    public interface IServiceHelper
    {
        public abstract T GetRequest<T>(string servicio, string metodo, string[] args, bool? ssl = null);
        public abstract string GetStrRequest<T>(string servicio, string metodo, string[] args, bool? ssl = null);
        public abstract T PostRequest<T>(string servicio, string metodo, object sender, bool? ssl = null);
        public abstract Task<T> Post<T>(Uri webApiUrl, object sender);
        //public static abstract T Post<T>(string servicio, string metodo, object sender);
    }
}
