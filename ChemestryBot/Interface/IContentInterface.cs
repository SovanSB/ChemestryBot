using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChemestryBot.UtilClasses;
using Microsoft.Bot.Connector;

namespace ChemestryBot.Interface
{
    interface IContentInterface
    {
        Activity GetContent(CodeClass code);

        CodeClass GetNextCode(CodeClass code);
    }
}
