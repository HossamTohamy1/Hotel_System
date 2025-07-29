namespace Hotel.Services
{
    public class CustomerToStaffPromotionDto
    {
        public int CustomerId { get; set; }
        public string StaffNumber { get; set; }
        public string Position { get; set; }
        public DateTime HireDate { get; set; }
        public bool IsManager { get; set; }
        public int RoleId { get; set; }
    }
}