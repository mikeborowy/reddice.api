using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedDice.API.Helpers
{
    public class Tools
    {

        /**
         *  string text = "This is an example string and my data is here";
         *  string data = getBetween(text, "my", "is"); 
         */
        public string getStringBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }
        }
    }
}