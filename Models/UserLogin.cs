﻿namespace ProjectLibrary_Back.Models
{
    public partial class UserLogin
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime? UserLoginDate { get; set; }
        public virtual User? User { get; set; }

    }
}