using Hotel.Core.Entities.Enum;
using Microsoft.AspNetCore.Http;

namespace HotelSystem.Exceptions
{
    public class NotFoundException : BaseApplicationException
    {
        public NotFoundException(string message, ErrorCode errorCode)
            : base(message, errorCode, StatusCodes.Status404NotFound)
        {
        }

        public NotFoundException(string message, ErrorCode errorCode, Exception innerException)
            : base(message, errorCode, StatusCodes.Status404NotFound, innerException)
        {
        }
    }
}
