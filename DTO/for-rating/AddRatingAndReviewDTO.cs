﻿namespace ProjectLibrary_Back.DTO
{
    public class AddRatingAndReviewDTO
    {
        public int? BookId { get; set; }
        public int? UserId { get; set; }
        public byte? Rating { get; set; }
        public string? Review { get; set; }
    }
}