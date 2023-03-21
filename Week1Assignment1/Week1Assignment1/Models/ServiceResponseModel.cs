﻿namespace Week1Assignment1.Models
{
    public class ServiceResponseModel
    {
        public enum Response { Success = 0, Error = 1 }

        public Response Status { get; set; }

        public object? Data { get; set; }

        public string Message { get; set; }

        public object? Errors { get; set; }

    }
}
