using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChemestryBot.UtilClasses
{
    [Serializable]
    public class CodeClass
    {
        public int[] IndexArray = new int[MessagesController.BotCategories.Length];
        private int mPointAt;


        public int PointAt
        {
            get { return mPointAt; }
            set { mPointAt = value; }
        }

        
    }

}