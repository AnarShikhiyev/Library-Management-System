using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ProjectLibrary_Back.Models;

public partial class User
{
    public int Id { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int? RoleId { get; set; }
 public bool Active { get; set; }=true;

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Remender> Remenders { get; set; } = new List<Remender>();

    public virtual ICollection<Rental> Rentals { get; set; } = new List<Rental>();

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    public virtual ICollection<UserLogin> UserLogins { get; set; } = new List<UserLogin>();

    public virtual Role? Role { get; set; }
   
    
}
