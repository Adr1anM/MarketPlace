using Newtonsoft.Json;

namespace MarketPlace.WebUI.Middlewares.Models
{
    public class ErrorDetail
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }
        public List<string> Errors { get; } = [];


        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
}
