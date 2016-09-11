using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChemestryBot.UtilClasses
{
    [Serializable]
    public class Category
    {
        public string Name;
        public Unit[] Units;

        public Category(string name, Unit[] units)
        {
            this.Name = name;
            this.Units = units;
        }

        public Category(string name)
        {
            this.Name = name;
        }

        public int GetCount()
        {
            if (Units == null)
            {
                return 0;
            }
            return Units.Length;
        }
        
    }
}