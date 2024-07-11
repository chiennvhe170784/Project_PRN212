using System;
using System.Collections.Generic;

namespace ProjectPRN.Models;

public partial class Test
{
    public string Code { get; set; } = null!;

    public string TestName { get; set; } = null!;

    public int Size { get; set; }

    public virtual ICollection<TestQuestion> TestQuestions { get; set; } = new List<TestQuestion>();
}
