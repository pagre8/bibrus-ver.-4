using System;
using System.Collections.Generic;

namespace BibrusServer.Models;

public partial class User
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? MiddleName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public int? AddressId { get; set; }

    public string? Type { get; set; }

    public int? CredentialsId { get; set; }

    public virtual Address? Address { get; set; }

    public virtual Credential? Credentials { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<Student> StudentParentUsers { get; set; } = new List<Student>();

    public virtual ICollection<Student> StudentUsers { get; set; } = new List<Student>();
}
