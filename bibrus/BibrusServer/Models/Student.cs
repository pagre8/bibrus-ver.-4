using System;
using System.Collections.Generic;

namespace BibrusServer.Models;

public partial class Student
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? ClassId { get; set; }

    public int? ParentUserId { get; set; }

    public virtual ICollection<Attandance> Attandances { get; set; } = new List<Attandance>();

    public virtual Class? Class { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

    public virtual User? ParentUser { get; set; }

    public virtual User? User { get; set; }
}
