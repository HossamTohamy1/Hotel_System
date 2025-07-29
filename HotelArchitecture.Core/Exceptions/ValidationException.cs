using Hotel.Core.Entities.Enum;
using Microsoft.AspNetCore.Http;

namespace HotelSystem.Exceptions
{
    public class ValidationException : BaseApplicationException
    {
        public ValidationException(string message)
            : base(message, ErrorCode.ValidationError, StatusCodes.Status400BadRequest)
        {
        }

        public ValidationException(string message, ErrorCode errorCode)
            : base(message, errorCode, StatusCodes.Status400BadRequest)
        {
        }

        public ValidationException(string message, Exception innerException)
            : base(message, ErrorCode.ValidationError, StatusCodes.Status400BadRequest, innerException)
        {
        }

        public ValidationException(string message, ErrorCode errorCode, Exception innerException)
            : base(message, errorCode, StatusCodes.Status400BadRequest, innerException)
        {
        }
    }
}
