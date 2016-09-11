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
    public class TestDialog : IDialog<int>
    {
        private TestSeries mSeries;
        private int mCurrent = 0;
        private int mCorrect = 0;
        public TestDialog(TestSeries series)
        {
            mSeries = series;
        }

        public async Task StartAsync(IDialogContext context)
        {
            if (mSeries == null || mSeries.Quiz == null || mSeries.Quiz.Length <= mCurrent)
            {
                context.Done(0);
            }
            else
            {
                await context.PostAsync(ValuesStrings.TEST_START_PHRASE);
                mCurrent = 0;
                PromptDialog.Choice(context, ResumeAfterChoise, mSeries.Quiz[mCurrent].Answers, mSeries.Quiz[mCurrent].Question,
                    ValuesStrings.NOT_UNDERSTANDING);
            }
        }

        private async Task ResumeAfterChoise(IDialogContext context, IAwaitable<string> result)
        {
            string temp = await result;
            if (temp.Equals(mSeries.Quiz[mCurrent].Correct))
            {
                mCorrect++;
                mCurrent++;
                await context.PostAsync(ValuesStrings.TEST_CORRECT);
                

            }
            else
            {
                await context.PostAsync(ValuesStrings.TEST_WRONG);
                mCurrent++;
                //context.Done(false);
            }
            if (mSeries.Quiz.Length > mCurrent)
            {
                PromptDialog.Choice(context, ResumeAfterChoise, mSeries.Quiz[mCurrent].Answers,
                    mSeries.Quiz[mCurrent].Question,
                    ValuesStrings.NOT_UNDERSTANDING);
            }
            else
            {
                context.Done(mCorrect);
            }
        }
    }
}