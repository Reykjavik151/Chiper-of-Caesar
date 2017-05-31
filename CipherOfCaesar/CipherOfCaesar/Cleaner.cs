using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherOfCaesar
{
    static class Cleaner
    {
        static public string CleanWord(string word)
        {
            string result = "";
            foreach(char Ch in word)
            {
                if ((96 <= Ch && Ch <= 122) || Ch == '-' || Ch == '/')
                    result += Ch;
            }
            return result;
        }
    }
}
