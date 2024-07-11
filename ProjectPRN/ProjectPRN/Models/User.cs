using System;
using System.Collections.Generic;

namespace ProjectPRN.Models;

public partial class User
{
    public int Uid { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int Role { get; set; }

    public virtual ICollection<History> Histories { get; set; } = new List<History>();
}
