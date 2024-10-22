namespace ProjectLibrary_Back.DTO
{
    public class GetBookRentalHistoryDTO
    {
        public int? UserId { get; set; }
        public string? RentalDate { get; set; }
        public string? DueDate { get; set; }

    }
}
