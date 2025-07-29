using Hotel.Core.Entities.Enum;

namespace HotelSystem.ViewModels
{
    public class ResponseViewModel<T>
    {
        public T Data { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string? TraceId { get; set; } 

        public ErrorCode ErrorCode { get; set; }

       public static ResponseViewModel<T> Error(string message, ErrorCode errorCode, string? traceId = null)
        {
           return new ResponseViewModel<T> 
           { 
               Data = default(T),
               IsSuccess = false,    
               Message = message,  
               ErrorCode = errorCode,
               TraceId = traceId
           };
        }

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
