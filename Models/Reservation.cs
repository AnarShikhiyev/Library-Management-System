using System;
using System.Collections.Generic;

namespace ProjectLibrary_Back.Models;

public partial class Reservation
{
    public int Id { get; set; }

    public DateOnly? ReservationDate { get; set; }

    public DateOnly? ExpirationDate { get; set; }

    public int? BookId { get; set; }

    public int? UserId { get; set; }
    public bool Status { get; set; }

    public virtual Book? Book { get; set; }

    public virtual User? User { get; set; }
}
