namespace ProjectLibrary_Back.DTO
{
    public class AddReservationDto
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
        public DateTime? ReservationDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}
