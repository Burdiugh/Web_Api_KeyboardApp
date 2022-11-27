using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helpers
{
    [Serializable]
    public class HttpException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public HttpException(HttpStatusCode status)
        {
            this.StatusCode = status;
        }
        public HttpException(HttpStatusCode status, string message) : base(message)
        {
            this.StatusCode = status;
        }
        public HttpException(HttpStatusCode status, string message, Exception inner) : base(message, inner)
        {
            this.StatusCode = status;
        }
        protected HttpException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
