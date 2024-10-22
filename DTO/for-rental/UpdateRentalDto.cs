namespace ProjectLibrary_Back.DTO
{
    public class UpdateRentalDto
    {
        public int RentalId { get; set; }
        public DateTime NewDueDate { get; set; }
        public bool IsReturned { get; set; }
    }
}
