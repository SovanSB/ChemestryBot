using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ChemestryBot.Interface;
using ChemestryBot.UtilClasses;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace ChemestryBot.Dialogs
{
    [Serializable]
    public class InformationDialog : IDialog<CodeClass>
    {
        private CodeClass mCode;
        private IContentInterface mContent = new CategoryOperation();

        public InformationDialog(CodeClass code)
        {
            this.mCode = code;
        }

        public async Task StartAsync(IDialogContext context)
        {
            Activity toShow = mContent.GetContent(mCode);
            if (toShow != null)
            {
                IMessageActivity toSend = context.MakeMessage();
                toSend.Attachments = toShow.Attachments;
                toSend.Text = toShow.Text;
                ShowActivity(toSend, context);
                return;
            }
            else // something went wrong
            {
                mCode.PointAt = -1;
                context.Done(mCode);
                return;
            }
        }

        private async void ShowActivity(IMessageActivity activity, IDialogContext context)
        {
            if (activity == null)
            {
                return; // something went wrong
            }
            if (activity.GetActivityType() != ActivityTypes.Message)
            {
                return;
            }
            if (activity.Attachments != null && activity.Attachments.Count > 0)
            {
                string text = activity.Text;
                activity.Text = null;
                context.PostAsync(activity);
                
                                PromptDialog.Choice<string>(
                                    context,
                                    ResumeAfterChoise,
                                    new[]
                                    {
                                        ValuesStrings.NEXT//, ValuesStrings.STATISTICS, ValuesStrings.CHANGE_NICK
                //                        ValuesStrings.ENTER_QUEUE
                                    },
                                    text,
                                    retry: "Sorry, I didn't understand you:( Please be more accurate in your wishes!");
                return; // handling would be added later
            }
            PromptDialog.Choice<string>(
                    context,
                    ResumeAfterChoise,
                    new[]
                    {
                        ValuesStrings.NEXT//, ValuesStrings.STATISTICS, ValuesStrings.CHANGE_NICK
//                        ValuesStrings.ENTER_QUEUE
                    },
                    activity.Text,
                    retry: "Sorry, I didn't understand you:( Please be more accurate in your wishes!");
        }

        private async Task ResumeAfterChoise(IDialogContext context, IAwaitable<string> result)
        {
            string temp = await result;
            switch (temp)
            {
                case ValuesStrings.NEXT:
                    mCode = mContent.GetNextCode(mCode);
                    context.Done(mCode);
                    return;
                    break;

                // other cases and buttons would be added later
            }
        }
    }
}