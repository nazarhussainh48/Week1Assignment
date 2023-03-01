namespace Week1Assignment1.Models
{
    public class ServiceResponseModel
    {
        public enum Eresponse { Success = 1, Error = 0 }

        public Eresponse Status { get; set; }

        public object? Data { get; set; }

        public string? Message { get; set; } 

    }
}
