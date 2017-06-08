namespace CipherOfCaesar
{
    static public class Cleaner
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
