using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace ChemestryBot.Dialogs
{
    [Serializable]
    public class MotherDialog : IDialog
    {
        public MotherDialog(Activity activity)
        {
            
        }


        public Task StartAsync(IDialogContext context)
        {
            throw new NotImplementedException();
        }
    }
}