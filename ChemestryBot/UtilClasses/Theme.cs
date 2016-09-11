using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChemestryBot.UtilClasses
{
    [Serializable]
    public class Theme
    {
        public string name;
        public bool flag;
        public Unit[] Units;
        public int done;

        public Theme(string nm, Unit[] units)
        {
            name = nm;
            Units = units;
            flag = false;
            if (units != null)
            {
                done = units.Count();
            }
        }

        public static Theme GetNextTheme(Theme[] Themes)
        {
            //Theme NewTheme = new Theme("NewName");
            Theme NewTheme = Array.Find(Themes, ResultTheme);
            return NewTheme;
        }

        private static bool ResultTheme(Theme theme)
        {
            return (theme.flag == false);
        }

    }
}