using Newtonsoft.Json;

namespace Gallery.WebAPI.Models
{
    public class ErrorDetailsViewModel
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
