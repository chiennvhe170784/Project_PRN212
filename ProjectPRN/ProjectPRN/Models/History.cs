using System;
using System.Collections.Generic;

namespace ProjectPRN.Models;

public partial class History
{
    public string Code { get; set; } = null!;

    public string Mark { get; set; } = null!;

    public DateTime Date { get; set; }

    public int Uid { get; set; }

    public int HistoryId { get; set; }

    public virtual User UidNavigation { get; set; } = null!;
}
