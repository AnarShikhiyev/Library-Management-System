namespace ProjectLibrary_Back.DTO
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public int? RoleId { get; set; }
        public bool Active { get; set; } 
    }
}
