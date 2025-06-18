using Hotel.Core.Entities.Enum;

namespace HotelSystem.ViewModels
{
    public class ResponseViewModel<T>
    {
        public T Data { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public ErrorCode ErrorCode { get; set; }
        public static ResponseViewModel<T> Success(T data)
        {
            return new ResponseViewModel<T>
            {
                Data = data,
                IsSuccess = true,
                Message = string.Empty,
                ErrorCode = ErrorCode.NoError,
            };
        }
    }
}
