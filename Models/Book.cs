using System;
using System.Collections.Generic;

namespace ProjectLibrary_Back.Models;

public partial class Book
{
    public int Id { get; set; }

    public string BookTitle { get; set; } = null!;

    public double BookPrice { get; set; }

    public string BookImg { get; set; } = null!;

    public int BookPage { get; set; }

    public DateOnly? BookPublicationYear { get; set; }

    public int BookInventoryCount { get; set; }

    public int? AuthorId { get; set; }

    public int? CategoryId { get; set; }

    public int? LanguageId { get; set; }

    public virtual Author? Author { get; set; }

    public virtual Category? Category { get; set; }

    public virtual Language? Language { get; set; }

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Remender> Remenders { get; set; } = new List<Remender>();

    public virtual ICollection<Rental> Rentals { get; set; } = new List<Rental>();

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
