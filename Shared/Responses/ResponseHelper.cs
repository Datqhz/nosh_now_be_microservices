using Microsoft.AspNetCore.Mvc;
using Shared.Models.Dtos;

namespace Shared.Responses;

public static class ResponseHelper
{
    public static ObjectResult ToResponse(int httpStatusCode, string errorMessage = "", string errorMessageCode = "", object data = null)
    {
        return new ObjectResult(new
            {
                status = httpStatusCode,
                statusText = errorMessage,
                errorMessage = errorMessage,
                errorMessageCode = errorMessageCode,
                data = data
            })
            { StatusCode = httpStatusCode };
    }
    
    public static ObjectResult ToPaginationResponse(int httpStatusCode, string errorMessage = "", string errorMessageCode = "", object data = null, PagingDto paging = null)
    {
        return new ObjectResult(new
            {
                status = httpStatusCode,
                statusText = errorMessage,
                errorMessage = errorMessage,
                errorMessageCode = errorMessageCode,
                data = data,
                paging = paging
            })
            { StatusCode = httpStatusCode };
    }
}