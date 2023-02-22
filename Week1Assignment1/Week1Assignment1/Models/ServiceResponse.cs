namespace Week1Assignment1.Models
{
    public class ServiceResponse<T> //T is actual type of data that we want to send back
    {
        public T Data { get; set; }

        public bool Success { get; set; } = true;

        public string Message { get; set; } = null;


    }
}
