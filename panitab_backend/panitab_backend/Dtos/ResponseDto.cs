using System.Text.Json.Serialization;

namespace panitab_backend.Dtos
{
    public class ResponseDto<T> //T es para valores genericos
    {
        public T Data { get; set; }
        public string Message { get; set; }

        [JsonIgnore]
        public int StatusCode { get; set; }
        public bool Status { get; set; }
        //public bool Status { get; set; }

        //[JsonIgnore]
        //public int StatusCode { get; set; }

        //public string Message { get; set; }

        //public T Data { get; set; }
    }
}
