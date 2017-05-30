using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherOfCaesar
{
    class BinaryTreeOfWords
    {

        #region Fields

        public string Value { get; set; }
        BinaryTreeOfWords Left;
        BinaryTreeOfWords Right;

        #endregion

        #region Contructors

        public BinaryTreeOfWords(string Value)
        {
            this.Value = Value;
            Left = null;
            Right = null;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Функция поиска в бинарном дереве.
        /// </summary>
        public bool Search(string item)
        {
            // Слово на вершине у данного дерева (поддерева) = найдено
            if (item == Value) return true;

            // Сравниваем с текущим словом на вершине
            // Если [item] < [Value] - рекурсивно ищем в левой подветви
            // Если [item] > [Value] - рекурсивно ищем в правой подветви
            int cmpr = String.Compare(item, Value);
            if (cmpr < 0 && Left != null)
                return Left.Search(item);
            else if (cmpr > 0 && Right != null)
                return Right.Search(item);

            // Поиск не дал результатов (На вершине нет совпадения и обе подветви == null)
            return false;
        }

        /// <summary>
        /// Функция добавления в бинарное дерево.
        /// </summary>
        public void Add(string item)
        {
            // Слово уже добавлено
            if (item == Value) return;

            // Сравниваем строки
            // Если [item] < [Value] - добавляем рекурсивно в левую подветвь
            // Если [item] > [Value] - добавляем рекурсивно в правую подветвь
            int cmpr = String.Compare(item, Value);
            if (cmpr < 0)
            {
                if (Left == null)
                    Left = new BinaryTreeOfWords(item);
                else
                    Left.Add(item);
            }
            else
            {
                if (Right == null)
                    Right = new BinaryTreeOfWords(item);
                else
                    Right.Add(item);
            }
        }

        #endregion

    }
}