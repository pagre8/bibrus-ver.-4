using System;
using System.Collections.Generic;

namespace BibrusServer.Models;

public partial class Credential
{
    public int Id { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
