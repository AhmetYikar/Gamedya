﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gamedya.Helper
{
    public static class Subs
    {
        public static string Sub30(string text)
        {
            int finish = 30;
            if (text.Length < finish)
            {
                return text;
            }
            else
            {
                return text.Substring(0, 30);
            }
        }
    }
}