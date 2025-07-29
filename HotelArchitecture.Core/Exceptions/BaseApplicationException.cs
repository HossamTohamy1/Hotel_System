using Hotel.Core.Entities.Enum;

namespace HotelSystem.Exceptions
{
    public abstract class BaseApplicationException : Exception
    {
        public ErrorCode ErrorCode { get; }
        public int HttpStatusCode { get; }

        protected BaseApplicationException(string message, ErrorCode errorCode, int httpStatusCode) : base(message)
        {
            ErrorCode = errorCode;
            HttpStatusCode = httpStatusCode;
        }

        protected BaseApplicationException(string message, ErrorCode errorCode, int httpStatusCode, Exception innerException)
            : base(message, innerException)
        {
            ErrorCode = errorCode;
            HttpStatusCode = httpStatusCode;
        }
    }
}
