using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ChemestryBot.UtilClasses;
using Microsoft.Bot.Builder.Dialogs;

namespace ChemestryBot.Dialogs
{
    [Serializable]
    public class ChooseCategoryDialog : IDialog<int>
    {
        private CodeClass mCode;

        public ChooseCategoryDialog(CodeClass code)
        {
            this.mCode = code;
        }

        public async Task StartAsync(IDialogContext context)
        {
            List<Category> buttonList = new List<Category>();
            List < string > nameList = new List<string>();
            for (int i = 0; i < mCode.IndexArray.Length; i++)
            {
                if (mCode.IndexArray[i] < MessagesController.BotCategories[i].GetCount())
                {
                    buttonList.Add(MessagesController.BotCategories[i]);
                    nameList.Add(MessagesController.BotCategories[i].Name);
                }
            }
            if (nameList.Count > 0)
            {

                PromptDialog.Choice(context, ResumeAfterCategoryChoise, nameList.ToArray(), "Выберите категорию");
                return;
            }
            else
            {
                context.Done(-1); // we have no categories to study
            }
        }

        private async Task ResumeAfterCategoryChoise(IDialogContext context, IAwaitable<string> result)
        {
            string selected = await result;
            if (selected == null)
            {
                context.Done(-1000); //error happened
                return;
            }
            for (int i = 0; i < MessagesController.BotCategories.Length; i++)
            {
                if (selected.Equals(MessagesController.BotCategories[i].Name))
                {
                    context.Done(i);
                    return;
                }
            }
            context.Done(-1000); //error happened
                                 
        }
    }
}