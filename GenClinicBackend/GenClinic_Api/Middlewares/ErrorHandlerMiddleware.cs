
using System.Net;
using GenClinic_Common.Exceptions;
using GenClinic_Entities.Response;
using GenClinic_Service.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using static GenClinic_Common.Constants.ErrorMessages;

namespace GenClinic_Api.Middlewares
{
    public class ErrorHandlerMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            //GenerateErrorResponse() will return the response of type ApiResponseDTO statuscode, message, content
            ErrorResponse<object> response = GenerateErrorResponse(context, ex);

            //It is used to convert the response in swagger into camel case we can return response instead of jsonresponse but it will return DTO
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            string jsonResponse = JsonConvert.SerializeObject(response, serializerSettings);
            await context.Response.WriteAsync(jsonResponse);
        }

        private ErrorResponse<object> GenerateErrorResponse(HttpContext context, Exception ex)
        {
            List<string> messages = new();
            int httpStatusCode = (int)HttpStatusCode.InternalServerError;

            void AddStatusCodeAndMessage(int statusCode, List<string> message)
            {
                httpStatusCode = statusCode;
                messages.AddRange(message);
            }

            switch (ex)
            {
                case ModelStateException exception:
                    AddStatusCodeAndMessage(HttpStatusCode.BadRequest.GetValue(), exception.Messages);
                    break;
                case CustomException customException:
                    AddStatusCodeAndMessage(customException.StatusCode, customException.Messages);
                    break;
                default:
                    AddStatusCodeAndMessage(HttpStatusCode.InternalServerError.GetValue(), new List<string>() { ExceptionMessage.INTERNAL_SERVER });
                    break;
            }
            //for showing same statuscode as of apiresponse in swagger
            context.Response.StatusCode = httpStatusCode;

            //In ApiResponseDTO we have constructor which set status code and message but for success we have to set manually
            return new ErrorResponse<object>(httpStatusCode, messages);
        }
    }
}