using System;
using System.Collections.Generic;

namespace BibrusServer.Models;

public partial class Subject
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? EmployeeId { get; set; }

    public virtual ICollection<Attandance> Attandances { get; set; } = new List<Attandance>();

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual Employee? Employee { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
}
