using FluentValidation.Results;

namespace WebPOS.Application.Commons.Foundations
{
    public class BaseResponse<T>
    {
        public BaseResponse() 
        {
            Errors = Enumerable.Empty<ValidationFailure>();
            Message = string.Empty;
        }

        public bool IsSuccess { get; set; }

        public T? Data { get; set; }

        public string Message { get; set; }

        public IEnumerable<ValidationFailure> Errors { get; set; }
    }
}
