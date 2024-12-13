using System.Net;

namespace PropertiesMinimalAPI.Models
{
    public class ResponseAPI
    {
        public ResponseAPI() 
        {
            Errors = new List<string>();
        }

        public bool Success { get; set; }
        public Object Result { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public List<string> Errors { get; set; }
    }
}
