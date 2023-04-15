using System;
using System.Collections.Generic;

namespace BibrusServer.Models;

public partial class Grade
{
    public int Id { get; set; }

    public int? StudentId { get; set; }

    public int? Subjectid { get; set; }

    public int? Value { get; set; }

    public int? Weight { get; set; }

    public DateTime? Date { get; set; }

    public string? Comment { get; set; }

    public string? Category { get; set; }

    public bool? CountToAvg { get; set; }

    public int? Semester { get; set; }

    public virtual Student? Student { get; set; }

    public virtual Subject? Subject { get; set; }
}
