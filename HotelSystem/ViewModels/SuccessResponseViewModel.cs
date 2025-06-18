namespace HotelSystem.ViewModels
{
    public class SuccessResponseViewModel<T> : ResponseViewModel<T>
    {
        public SuccessResponseViewModel(T Data, string message)
        {
            this.Data = Data;
            Message = message;
            IsSuccess = true;
            ErrorCode = Hotel.Core.Entities.Enum.ErrorCode.NoError;
        }
    }
}
