namespace ProjectLibrary_Back.DTO
{
    public class AddRentalDto
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime DueDate { get; set; }
    }
}
