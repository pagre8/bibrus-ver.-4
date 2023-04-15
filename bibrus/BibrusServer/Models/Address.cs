using System;
using System.Collections.Generic;

namespace BibrusServer.Models;

public partial class Address
{
    public int Id { get; set; }

    public string? City { get; set; }

    public string? Road { get; set; }

    public string? HomeNumber { get; set; }

    public string? Voivoidship { get; set; }

    public string? PostalCode { get; set; }

    public string? PhoneNumber { get; set; }

    public virtual ICollection<School> Schools { get; set; } = new List<School>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
