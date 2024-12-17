using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GenClinic_Entities.Response;
using Microsoft.AspNetCore.Mvc;

namespace GenClinic_Api.Helpers
{
    public class ResponseHelper
    {
        public static IActionResult CreatedResponse<T>(T? data, string message)
        {
            ApiResponse<T> result = new()
            {
                StatusCode = (int)HttpStatusCode.Created,
                Message = message,
                Data = data,
                Success = true,
            };
            return new ObjectResult(result) { StatusCode = (int)HttpStatusCode.Created };
        }

        public static IActionResult SuccessResponse<T>(T? data, string message = "Success")
        {
            ApiResponse<T> result = new()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = message,
                Data = data,
                Success = true,
            };
            return new ObjectResult(result) { StatusCode = (int)HttpStatusCode.OK };

        }

        // public static IActionResult CreatePageResponse<T>(IEnumerable<T> data, int pageNumber, int pageSize, int totalPage, long totalRecords = 0)
        // {
        //     PageResponseDto<T> result = new(data, pageNumber, pageSize, totalPage, totalRecords);
        //     ApiResponse<PageResponseDto<T>> response = new()
        //     {
        //         Success = true,
        //         Data = result,
        //         StatusCode = (int)HttpStatusCode.OK,
        //     };
        //     return new ObjectResult(response) { StatusCode = (int)HttpStatusCode.OK };
        // }

        internal static IActionResult CreatedResponse(object token, object login_SUCCESS)
        {
            throw new NotImplementedException();
        }

        internal static IActionResult CreatedResponse(long userId, object accountCreated)
        {
            throw new NotImplementedException();
        }
    }
}