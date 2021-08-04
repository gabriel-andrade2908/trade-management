using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;

namespace GerenciamentoComercio_Domain.Utils.APIMessage
{
    public class APIMessage
    {
        public HttpStatusCode StatusCode { get; set; }
        public List<string> Content { get; set; }
        public object ContentObj { get; set; }

        public APIMessage() { }
        public APIMessage(HttpStatusCode statusCode, object content)
        {
            StatusCode = statusCode;
            ContentObj = content;
        }


        public APIMessage(HttpStatusCode statusCode, List<string> content)
        {
            StatusCode = statusCode;
            Content = content;
        }



        public string ToContentString() => JsonConvert.SerializeObject(Content);
    }
}
