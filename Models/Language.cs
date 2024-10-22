using System;
using System.Collections.Generic;

namespace ProjectLibrary_Back.Models;

public partial class Language
{
    public int Id { get; set; }

    public string BookLanguage { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
