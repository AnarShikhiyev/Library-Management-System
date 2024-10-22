namespace ProjectLibrary_Back.DTO
{
    public class RegisterUserDto
    {
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int? RoleId { get; set; }
    }
}
