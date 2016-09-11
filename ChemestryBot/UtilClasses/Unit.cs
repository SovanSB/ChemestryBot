using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChemestryBot.UtilClasses
{
    [Serializable]
    public class Unit
    {
        public string name;
        public string imageUrl;
        public string wikiUrl;
        public string description;
        public bool flag;

        public Unit(string nm, string ds, string wikiUrl, string iu)
        {
            this.name = nm;
            this.description = ds;
            this.imageUrl = iu;
            this.wikiUrl = wikiUrl;
            flag = false;
        }

        public Unit(string nm, string ds)
        {
            name = nm;
            description = ds;
            imageUrl = "";
            flag = false;
        }

        public Unit(string nm, string ds, string iu)
        {
            name = nm;
            description = ds;
            this.imageUrl = iu;
            flag = false;
        }

        public static bool ResultUnit(Unit unit)
        {
            return (unit.flag == false);
        }



    }
}