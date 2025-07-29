using Hotel.Core.Entities.Enum;
using Microsoft.AspNetCore.Http;

namespace HotelSystem.Exceptions
{
    public class BusinessLogicException : BaseApplicationException
    {
        public BusinessLogicException(string message, ErrorCode errorCode)
            : base(message, errorCode, StatusCodes.Status400BadRequest)
        {
        }

        public BusinessLogicException(string message, ErrorCode errorCode, Exception innerException)
            : base(message, errorCode, StatusCodes.Status400BadRequest, innerException)
        {
        }
    }
}
