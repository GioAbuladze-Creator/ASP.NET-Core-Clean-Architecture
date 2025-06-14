using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Responses
{
    public class Result<T>
    {
        public bool IsSuccess { get; } = true;
        public string? Message { get; }
        public T? Data { get; }
        public List<string>? ValidationErrors { get; }
        public Result(bool isSuccess, string message, T? data, List<string>? errors)
        {
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
            ValidationErrors = errors;
        }
    }
}
