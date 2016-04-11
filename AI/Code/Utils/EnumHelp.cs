using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AI.Code.Utils
{
    static public class EnumHelp
    {
        public static string GetDescription(string message)
        {
            if (message == "Ms")
                return "Ms.";
            if (message == "Mr")
                return "Mr.";
            if (message == "Mrs")
                return "Mrs.";
            if (message == "Dr")
                return "Dr.";
            if (message == "AssociateProfessor")
                return "Associate Professor";
            if (message == "TeachingAssistant")
                return "Teaching Assistant";
            if (message == "PhdStudent")
                return "PhD Student";
            if (message == "Lecturer")
                return "Lecturer";
            if (message == "Postdoc")
                return "Postdoc";
            if (message == "Professor")
                return "Professor";

            return null;
        }
    }
}