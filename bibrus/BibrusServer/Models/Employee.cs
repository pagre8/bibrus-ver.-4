using System;
using System.Collections.Generic;

namespace BibrusServer.Models;

public partial class Employee
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string? Role { get; set; }

    public virtual ICollection<Attandance> Attandances { get; set; } = new List<Attandance>();

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual ICollection<Subject> Subjects { get; set; } = new List<Subject>();

    public virtual User? User { get; set; }
}
