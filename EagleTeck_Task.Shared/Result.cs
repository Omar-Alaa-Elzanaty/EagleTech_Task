using EagleTeck_Task.Shared.Extensions;
using FluentValidation.Results;
using System.Net;

namespace EagleTeck_Task.Shared
{
    public class Result<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
        public Dictionary<string, List<string>> Errors { get; set; }

        public Result(HttpStatusCode statusCode = HttpStatusCode.OK) => StatusCode = statusCode;

        public static Result<T>Success(T data,string message)
        {
            return new Result<T>()
            {
                IsSuccess = true,
                Data = data,
                Message = message
            };
        }
        
        public static Result<T>Success(T data)
        {
            return new Result<T>()
            {
                IsSuccess = true,
                Data = data
            };
        }
        
        public static Result<T>Success()
        {
            return new Result<T>()
            {
                IsSuccess = true
            };
        }

        public static Result<T> Failure()
        {
            return new Result<T>
            {
                StatusCode = HttpStatusCode.BadRequest
            };
        }
        
        public static Result<T> Failure(T data,string message)
        {
            return new Result<T>
            {
                StatusCode = HttpStatusCode.BadRequest,
                Data = data,
                Message = message
            };
        }
        
        public static Result<T> Failure(string message)
        {
            return new Result<T>
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = message
            };
        }

        public static Result<T>ValidationFailure(List<ValidationFailure> validationFailures,HttpStatusCode httpStatusCode)
        {
            var errors = validationFailures.GetErrorDictionary();

            return new Result<T>()
            {
                Errors = errors,
                StatusCode = httpStatusCode
            };
        }
    }
}
