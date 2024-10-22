namespace ProjectLibrary_Back.DTO
{
    public class ReservationDto
    {
        public int Id { get; set; }
        public DateOnly? ReservationDate { get; set; }
        public DateOnly? ExpirationDate { get; set; }
        public int? BookId { get; set; }
        public int? UserId { get; set; }

    }
}
