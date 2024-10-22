﻿namespace ProjectLibrary_Back.DTO
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public string AuthorName { get; set; } = null!;
        public string AuthorSurname { get; set; } = null!;
        public DateTime? AuthorBirthDate { get; set; }
        public string AuthorImg { get; set; } = null!;
    }
}