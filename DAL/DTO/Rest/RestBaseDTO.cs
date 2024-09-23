using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO.Rest
{
   
        public class RestBaseDTO<T>
        {
            public bool Success { get; set; }
            public object Message { get; set; }
            public T Data { get; set; }

            public RestBaseDTO()
            {
                Success = false;
                Message = string.Empty;
                Data = default;
            }

            public void SetMessage(string message)
            {
                Message = message;
            }

            public void SetMessage(Dictionary<string, List<string>> messages)
            {
                Message = messages;
            }


            public void SetMessage(IEnumerable<string> messages)
            {
                Message = messages;
            }
        }
    }

