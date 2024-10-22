namespace ProjectLibrary_Back.DTO
{
    public class GetUserLoginHistoryDTO
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public DateTime? UserLoginDate { get; set; }
    }
}
