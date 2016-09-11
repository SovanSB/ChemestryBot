using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ChemestryBot.UtilClasses;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace ChemestryBot.Dialogs
{
    [Serializable]
    public class MotherDialog : IDialog<string>
    {
        private CodeClass mCode = null;
        public MotherDialog()
        {
            
        }


        public async Task StartAsync(IDialogContext context)
        {
            mCode = new CodeClass();
//            await context.PostAsync("Choose the category");
            context.PostAsync(ValuesStrings.HELLO);
            context.Wait(ResumeAfterGreeting);
            
//            context.Call(new InformationDialog(new CodeClass()), ResumeAfterInformation);
            //throw new NotImplementedException();
        }

        private async Task ResumeAfterGreeting(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
                        context.Call(new ChooseCategoryDialog(new CodeClass()), ResumeAfterCategoryChoise);
//            context.Call(new InformationDialog(new CodeClass()), ResumeAfterInformation);
        }

        private async Task ResumeAfterCategoryChoise(IDialogContext context, IAwaitable<int> result)
        {
            int code = await result;
            if (code == -1) // we have no categories to study left
            {
                await context.PostAsync("Not implemented yet (no categories to study)");
                context.Done(0);
                return;
            }
            if (code == -1000)
            {
                await context.PostAsync("Some bad error happened, no category was chosen");
                context.Done(0);
                return;
            }
            else
            {
                mCode.PointAt = code;
                context.Call(new InformationDialog(mCode), ResumeAfterInformation);
            }
        }

        private async Task ResumeAfterInformation(IDialogContext context, IAwaitable<CodeClass> result)
        {
            var temp = await result;
            if (temp == null)
            {
                // something went very bad
                context.Done("");
                return;
            }
//            if (temp.Equals(mCode))
//            {
//                // same code returned, abnormal
//                return;
//            }
            else
            {
                mCode = temp;
                DoNextStep(context);
            }

        }

        private void DoNextStep(IDialogContext context)
        {
            // We 
            if (mCode.PointAt < 0)
            {
                int pointed =  - (mCode.PointAt + 1);
                context.Call(new ChooseCategoryDialog(mCode), ResumeAfterCategoryChoise);
                return;
            }
            context.Call(new InformationDialog(mCode), ResumeAfterInformation);
        }
    }
}