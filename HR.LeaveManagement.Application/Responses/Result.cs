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
        public T? Value { get; }
        public List<string>? ValidationErrors { get; }
        public Result(bool isSuccess, string message, T? value, List<string>? errors)
        {
            IsSuccess = isSuccess;
            Message = message;
            Value = value;
            ValidationErrors = errors;
        }
    }
}
