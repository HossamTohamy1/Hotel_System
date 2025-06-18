using Hotel.Core.Entities.Enum;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HotelSystem.ViewModels
{
    public class ErrorResponseViewModel<T> : ResponseViewModel<T>
    {
        public ErrorResponseViewModel(string message, ErrorCode errorCode = ErrorCode.UnknownError)
        {
            Data = default;
            IsSuccess = false;
            Message = message;
            ErrorCode = errorCode;
        }
    }
}
