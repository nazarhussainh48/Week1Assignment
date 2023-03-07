namespace Week1Assignment1.Models
{
    public class ServiceResponseModel
    {
        public enum Response { Success = 1, Error = 0 }

        public Response Status { get; set; }

        public object? Data { get; set; }

        public string Message { get; set; } 

    }
}
