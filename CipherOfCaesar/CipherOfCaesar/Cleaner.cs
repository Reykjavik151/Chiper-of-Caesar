namespace CipherOfCaesar
{
    static public class Cleaner
    {

        #region Methods

        /// <summary>
        /// Очищает слово от всех не алфавитных символов (кроме '-' и '/').
        /// </summary>
        static public string CleanWord(string word)
        {
            string result = "";
            foreach(char Ch in word)
            {
                if ((97 <= Ch && Ch <= 122) || Ch == '-' || Ch == '/')
                    result += Ch;
            }
            return result;
        }

        #endregion

    }
}
