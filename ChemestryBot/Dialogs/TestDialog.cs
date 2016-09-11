using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ChemestryBot.UtilClasses;
using ChemestryBot.UtilClasses.Tests;
using Microsoft.Bot.Builder.Dialogs;

namespace ChemestryBot.Dialogs
{
    [Serializable]
    public class TestDialog : IDialog
    {
        private TestSeries mQuestion;
        public TestDialog(TestSeries series)
        {
            mQuestion = series;
        }

        public async Task StartAsync(IDialogContext context)
        {
            if (mQuestion == null || mQuestion.Quiz == null || mQuestion.Quiz.Length < 1)
            {
                context.Done(true);
            }
            else
            {
                PromptDialog.Choice(context, ResumeAfterChoise, mQuestion.Answers, mQuestion.Question,
                    ValuesStrings.NOT_UNDERSTANDING);
            }
        }

        private async Task ResumeAfterChoise(IDialogContext context, IAwaitable<string> result)
        {
            string temp = await result;
            if (temp.Equals(mQuestion.Correct))
            {
                context.Done(true);
            }
            else
            {
                context.Done(false);
            }
        }
    }
}