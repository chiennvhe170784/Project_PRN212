using System;
using System.Collections.Generic;

namespace ProjectPRN.Models;

public partial class TestQuestion
{
    public string Code { get; set; } = null!;

    public int Qid { get; set; }

    public int TestQuestionId { get; set; }

    public virtual Test CodeNavigation { get; set; } = null!;

    public virtual Question QidNavigation { get; set; } = null!;
}
