﻿namespace ProjectLibrary_Back.DTO
{
    public class GetAthorBooksbyIdDto
    {
        public int Id { get; set; }
        public string? BookTitle { get; set; }
        public double BookPrice { get; set; }
        public string? BookImg { get; set; }
        public int BookPage { get; set; }
        public DateTime? BookPublicationYear { get; set; }
        public int BookInventoryCount { get; set; }
    }
}