using System;
using System.Collections.Generic;

namespace BibrusServer.Models;

public partial class School
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? AddressId { get; set; }

    public virtual Address? Address { get; set; }
}
