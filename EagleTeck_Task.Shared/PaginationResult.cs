using EagleTeck_Task.Shared.Extensions;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EagleTeck_Task.Shared
{
    public class PaginationResult<T>:Result<T>
    {
        public PaginationResult() { }
        public PaginationResult(bool succeeded, List<T> data = default,
                               string message = null,
                               List<ValidationFailure> validationFailures = null, int count = 0,
                               HttpStatusCode httpStatusCode = HttpStatusCode.OK,
                               int pageNumber = 1, int pageSize = 10)
        {
            Data = data;
            CurrentPage = pageNumber;
            StatusCode = httpStatusCode;
            IsSuccess = succeeded;
            Message = message;
            Errors = validationFailures?.GetErrorDictionary();
            PageSize = pageSize;
            TotalPages = count / pageSize + (count % pageSize > 0 ? 1 : 0);
            TotalCount = count;
        }

        public new List<T> Data { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }

        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;

        public static PaginationResult<T> Create(List<T> data, int count, int pageNumber, int pageSize)
        {
            return new PaginationResult<T>(true, data, null, null, count, HttpStatusCode.OK, pageNumber, pageSize);
        }

        public static async Task<PaginationResult<T>> FailureAsync(string message, HttpStatusCode statusCode)
        {
            return new PaginationResult<T>()
            {
                Message = message,
                StatusCode = statusCode,
                IsSuccess = false
            };
        }
    }
}
