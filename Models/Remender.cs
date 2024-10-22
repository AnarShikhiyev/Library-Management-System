using System;
using System.Collections.Generic;

namespace ProjectLibrary_Back.Models;

public partial class Remender
{
    public int Id { get; set; }

    public int? BookId { get; set; }

    public int? UserId { get; set; }

    public DateOnly ReminderDate { get; set; }

    public string ReminderMessage { get; set; } = null!;

    public virtual Book? Book { get; set; }

    public virtual User? User { get; set; }
}
