using System;
using System.Collections.Generic;

namespace ProjectLibrary_Back.Models;

public partial class Event
{
    public int Id { get; set; }

    public string EventName { get; set; } = null!;

    public DateOnly EventDate { get; set; }

    public string Location { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int? UserId { get; set; }

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual User? User { get; set; }
}
