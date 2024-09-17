using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Reflection;
using Week1Assignment1.Models;

namespace Week1Assignment1.Helper
{
    public class BaseController: ControllerBase
    {
        /// <summary>
        ///  Ok Response for object
        /// </summary>
        /// <param name="value"></param>
        /// <returns>success Ok message</returns>
        public override OkObjectResult Ok(object? value)
        {
            return base.Ok(new ServiceResponseModel()
            {
                Status = ServiceResponseModel.Eresponse.Success,
                Data = value
            }); 
        }

        /// <summary>
        ///  Ok Response for string
        /// </summary>
        /// <param name="message"></param>
        /// <returns>success Ok message</returns>
        protected OkObjectResult Ok(string message)
        {
            return base.Ok(new ServiceResponseModel()
            {
                Status = ServiceResponseModel.Eresponse.Success,
                Message = message
            });
        }

        /// <summary>
        /// ok response for object&string
        /// </summary>
        /// <param name="value"></param>
        /// <param name="message"></param>
        /// <returns>success ok message</returns>
        protected OkObjectResult Ok(object value, string message)
        {
            return base.Ok(new ServiceResponseModel() 
            {
                Status= ServiceResponseModel.Eresponse.Success,
                Message= message,
                Data = value
            });
        }

        /// <summary>
        /// BadRequet for object
        /// </summary>
        /// <param name="message"></param>
        /// <returns>return bad response for object</returns>
        public override BadRequestObjectResult BadRequest(object? message)
        {
            return base.BadRequest(new ServiceResponseModel()
            {
                Status = ServiceResponseModel.Eresponse.Error,
                Data = message
            });
        }

        /// <summary>
        /// BadRequest for string
        /// </summary>
        /// <param name="message"></param>
        /// <returns>return bad response for string</returns>
        protected BadRequestObjectResult BadRequest(string message)
        {
            return base.BadRequest(new ServiceResponseModel()
            {
                Status = ServiceResponseModel.Eresponse.Error,
                Message= message
            });
        }
        
        /// <summary>
        /// badrequest for object&string
        /// </summary>
        /// <param name="value"></param>
        /// <param name="message"></param>
        /// <returns>return bad respons for both object&string</returns>
        protected BadRequestObjectResult BadRequest(object value, string message)
        {
            return base.BadRequest(new ServiceResponseModel()
            {
                Status = ServiceResponseModel.Eresponse.Error,
                Message = message,
                Data= value
            });
        }
    }
}
