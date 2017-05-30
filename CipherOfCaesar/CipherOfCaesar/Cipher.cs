﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CipherOfCaesar
{
    class Cipher
    {

        #region Fields

        // Входная строка
        public string Enter { get; set; }

        // Количество символов в алфавите
        public int CharacterCounter { get; private set; }

        // Частота каждого символа в тексте
        public SortedDictionary<char, int> Frequency { get; private set; }

        // Дерево, хранящее в себе множество английских слов
        BinaryTreeOfWords TreeOfWords;

        #endregion

        #region Constructors

        public Cipher(int CharacterCounter)
        {
            Enter = null;
            this.CharacterCounter = CharacterCounter;
            Frequency = new SortedDictionary<char, int>();
            TreeOfWords = new BinaryTreeOfWords("Start");
            FillTree();
        }
        public Cipher(int CharacterCounter, string Enter)
        {
            this.Enter = Enter.ToLower();
            this.CharacterCounter = CharacterCounter;
            Frequency = new SortedDictionary<char, int>();
            CalculateFrequency();
            TreeOfWords = new BinaryTreeOfWords("Start");
            FillTree();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Сдвиг на [ROTN] входной строки.
        /// </summary>
        public string Rotate(int ROTN)
        {
            // Проверка на инициализированность входной строки
            if (Enter == null)
                return null;

            // Избавление от циклов путем остатка от деления
            ROTN = ROTN % CharacterCounter;
            if (ROTN == 0)
                return Enter;

            // Преобразование строки посимвольно
            // Если это символ английского алфавита      ->  сдвигаем его на [ROTN]
            // Если это не символ английского алфавита   ->  оставляем его без изменений
            // Повторяем действие для каждого символа входной строки
            string result = "";
            for (int i = 0; i < Enter.Length; i++)
            {
                if (97 <= Enter[i] && Enter[i] <= 122)
                    result += RotateChar(Enter[i], ROTN);
                else
                    result += Enter[i];
            }
            return result;
        }

        /// <summary>
        /// Сдвиг символа [Char] на [Rotn].
        /// </summary>
        private char RotateChar(char Char, int Rotn)
        {
            // Проверка на выход из диапазона алфавита
            int RotnChar = Char + Rotn;

            // Если символ "ушел вниз" :   a -> [ROTN == -1] -> z
            // Если символ "ушел вверх":   z -> [ROTN ==  1] -> a
            if (97 > RotnChar)
                RotnChar = 123 - (97 - RotnChar);
            else if (RotnChar > 122)
                RotnChar = 96 + (RotnChar - 122);

            return (char)RotnChar;
        }

        /// <summary>
        /// Поиск слова в дереве.
        /// </summary>
        public bool SearchWord(string item)
        {
            return TreeOfWords.Search(item);
        }

        /// <summary>
        /// Подсчет частоты каждого символа входной строки.
        /// </summary>
        public void CalculateFrequency()
        {
            // Если символ уже есть в словаре - инкрементируем частоту этого символа
            // В противном случае добавляем его в словарь со значением [1]
            for (int i = 0; i < Enter.Length; i++)
            {
                if (Frequency.ContainsKey(Enter[i]))
                    Frequency[Enter[i]]++;
                else
                    Frequency.Add(Enter[i], 1);
            }
        }

        /// <summary>
        /// Заполнение дерева множеством английских слов.
        /// </summary>
        private void FillTree()
        {
            // Считываем слова из файла, в котором хранятся слова
            string[] strs = File.ReadAllLines("glossary.txt");

            // Добавляем в дерево все из них
            foreach (string item in strs)
            {
                TreeOfWords.Add(item);
            }
        }

        /// <summary>
        /// Функция, которая определяет, является ли данный текст зашифрованным или нет.
        /// </summary>
        public int Guess()
        {
            // Если введенное слово существует в словаре - значит шифра нет
            if (SearchWord(Enter))
                return 0;

            // Пробуем все смещения, чтобы попробовать привести слово к нормальному виду
            // Каждый раз после преобразования слова мы проверяем его на существование в словаре
            // Если слово найдено - значит мы верно расшифровали его
            for (int i = 1; i < 27; i++)
            {
                if (SearchWord(Rotate(i)))
                    return i;
            }

            // Ни одно из смещенний не подошло
            // Итог - это слово введено явно не для расшифровки, а просто ошибочным образом
            return 0;
        }

        #endregion

    }
}