using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autobot.Common
{
    public class Response
    {
        private Response(bool isSuccess, string errorMessage)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
        }

        private Response(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public bool IsSuccess { get; set; }

        public string ErrorMessage { get; set; }

        public static Response Success { get; set; } = new Response(true);

        public static Response Failure(string message)
        {
            return new Response(false, message);
        }
    }
}
