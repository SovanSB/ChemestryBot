using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChemestryBot.UtilClasses
{
    [Serializable]
    public class TestQuestion
    {
        public string Question;

        public string[] Answers;

        public string Correct;

        public TestQuestion(string question, string[] answers, string correct)
        {
            this.Question = question;
            this.Answers = answers;
            this.Correct = correct;
        }

    }
}