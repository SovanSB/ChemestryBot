using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ChemestryBot.UtilClasses;
using ChemestryBot.UtilClasses.Tests;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace ChemestryBot.Dialogs
{
    [Serializable]
    public class MotherDialog : IDialog<string>
    {
        private CodeClass mCode = null;
        private int mCorrect = 0;
        public MotherDialog()
        {
            
        }


        public async Task StartAsync(IDialogContext context)
        {
            mCode = new CodeClass();
//            await context.PostAsync("Choose the category");
            await context.PostAsync("Привет! ");
            
            context.Wait(ResumeAfterGreeting);
            
//            context.Call(new InformationDialog(new CodeClass()), ResumeAfterInformation);
            //throw new NotImplementedException();
        }

        private async Task ResumeAfterHello(IDialogContext context, IAwaitable<string> result)
        {
            string temp = await result;
            await context.PostAsync("Привет, " + temp + "! \n" + ValuesStrings.HELLO);
            context.Call(new ChooseCategoryDialog(new CodeClass()), ResumeAfterCategoryChoise);
        }

        private async Task ResumeAfterGreeting(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            //                        context.Call(new ChooseCategoryDialog(new CodeClass()), ResumeAfterCategoryChoise);
            PromptDialog.Text(context, ResumeAfterHello, ValuesStrings.FIRST_HELLO);
            //            context.Call(new InformationDialog(new CodeClass()), ResumeAfterInformation);
        }

        private async Task ResumeAfterCategoryChoise(IDialogContext context, IAwaitable<int> result)
        {
            int code = await result;
            if (code < 0) // we have no categories to study left
            {
                if (mCorrect == 24)
                {
                    await context.PostAsync("Поздравляем! Ты прошёл весь мой курс и правильно ответил на все вопросы!!! До новых встреч в новых приключениях:)");
                }
                else
                {
                    await context.PostAsync("Поздравляем! Ты прошёл весь мой курс и ответил на " + mCorrect +
                                            "/24 вопросов правильно. До новых встреч!:)");
                }
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
                if (MessagesController.Tests[pointed] != null && MessagesController.Tests[pointed].Quiz.Length > 0)
                {
                    context.Call(new TestDialog(MessagesController.Tests[pointed]), ResumeAfterTests);
                    return;
                }
                context.Call(new ChooseCategoryDialog(mCode), ResumeAfterCategoryChoise);
                return;
            }
            context.Call(new InformationDialog(mCode), ResumeAfterInformation);
        }

        private async Task ResumeAfterTests(IDialogContext context, IAwaitable<int> result)
        {
            int temp = await result;
            mCorrect += temp;
            context.Call(new ChooseCategoryDialog(mCode), ResumeAfterCategoryChoise);
        }
    }
}