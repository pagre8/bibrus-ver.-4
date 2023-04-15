using System;
using System.Collections.Generic;

namespace BibrusServer.Models;

public partial class Class
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Year { get; set; }

    public int? SubjectId { get; set; }

    public int? EmployeeId { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual Subject? Subject { get; set; }
}
