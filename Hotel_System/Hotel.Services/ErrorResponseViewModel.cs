using Hotel.Core.Entities.Enum;
using HotelSystem.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Hotel.Services
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
