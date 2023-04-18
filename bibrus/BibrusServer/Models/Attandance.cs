using System;
using System.Collections.Generic;

namespace BibrusServer.Models;

public partial class Attandance
{
    public int Id { get; set; }

    public int? SubjectId { get; set; }

    public int? StudentId { get; set; }

    public string? Value { get; set; }

    public DateTime Date { get; set; }

    public int Hour { get; set; }

    public int? EmployeeId { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Student? Student { get; set; }

    public virtual Subject? Subject { get; set; }
}
