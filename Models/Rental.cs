using System;
using System.Collections.Generic;

namespace ProjectLibrary_Back.Models;

public partial class Rental
{
    public int Id { get; set; }

    public DateOnly RentalDate { get; set; }

    public DateOnly DueDate { get; set; }

    public int? BookId { get; set; }

    public int? UserId { get; set; }
    public bool status { get; set; }=true;

    public virtual Book? Book { get; set; }

    public virtual User? User { get; set; }
}
