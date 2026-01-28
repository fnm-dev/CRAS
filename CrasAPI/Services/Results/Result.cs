using CrasAPI.Services.Errors;

namespace CrasAPI.Services.Results
{
    public class Result<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public ErrorCode? Error { get; set; }

        private Result(bool success, T? data, ErrorCode? error)
        {
            Success = success;
            Data = data;
            Error = error;
        }

        public static Result<T> Ok(T data) => new(true, data, null);

        public static Result<T> Fail(ErrorCode error) => new(false, default, error);
    }
}
