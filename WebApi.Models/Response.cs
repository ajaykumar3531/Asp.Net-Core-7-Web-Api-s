using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Models
{
  public class Response
    {
        public string StatusCode {  get; set; }
        public string Message { get; set; }
        public object Data {  get; set; }

        public string Token { get; set; }
        public Response(string Code,string StatusMessage) { 
           
            StatusCode = Code;
            Message = StatusMessage;
            Data = string.Empty;
            Token = string.Empty;
        }

        public Response(String code, string StatusMessage,object data) {

            StatusCode = code;
            Message = StatusMessage;
            Data = data;
            Token =string.Empty;
        }

        public Response(String code, string StatusMessage, object data,string token)
        {

            StatusCode = code;
            Message = StatusMessage;
            Data = data;
            Token =token;
        }

    }
}
