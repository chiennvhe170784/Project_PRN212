using System;
using System.Collections.Generic;

namespace ProjectPRN.Models;

public partial class Question
{
    public int Qid { get; set; }

    public string Question1 { get; set; } = null!;

    public string Answer { get; set; } = null!;

    public string CorrectAnswer { get; set; } = null!;

    public virtual ICollection<TestQuestion> TestQuestions { get; set; } = new List<TestQuestion>();
}
