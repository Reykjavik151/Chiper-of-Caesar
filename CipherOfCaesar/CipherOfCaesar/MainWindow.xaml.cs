using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace CipherOfCaesar
{
    public partial class MainWindow : Window
    {

        #region Fields

        Cipher cipher;

        #endregion

        public MainWindow()
        {
            InitializeComponent();
            cipher = new Cipher(26);
        }

        #region Events

        private void EncryptOnClick(object sender, RoutedEventArgs e)
        {
            Bridge(true);
        }
        private void DecryptOnClick(object sender, RoutedEventArgs e)
        {
            Bridge(false);
        }
        private void richTextBoxEnterOnTextChanged(object sender, TextChangedEventArgs e)
        {
            string[] items = (new TextRange(richTextBoxEnter.Document.ContentStart,
                                            richTextBoxEnter.Document.ContentEnd).Text).ToLower().Split(' ');

            for(int i = 0; i < items.Length; i++)
                items[i] = Cleaner.CleanWord(items[i]);

            if (items.Length == 0)
                Guess(0);

            Dictionary<int, int> Mathes = new Dictionary<int, int>();
            Mathes.Add(0, items.Length);
            foreach (string item in items)
            {
                cipher.Enter = item;
                int guess = cipher.Guess();
                if (guess != 0)
                {
                    if (Mathes.ContainsKey(guess))
                        Mathes[guess]++;
                    else
                        Mathes[guess] = 1;

                    Mathes[0]--;
                }
            }

            int value = Mathes.Max(n => n.Value);
            foreach(KeyValuePair<int, int> buff in Mathes)
            {
                if (buff.Value == value)
                {
                    Guess(-buff.Key);
                    return;
                }
            }
            Guess(0);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Переходная функция к вычислению смещенной строки
        /// </summary>
        private void Bridge(bool encrypt)
        {
            int buff;
            try
            {
                buff = Convert.ToInt32(textBoxRotateTo.Text);

                if (!encrypt)
                    buff *= -1;

                Work(buff);
            }
            catch (FormatException)
            {
                MessageBox.Show("Please, enter only integer number in the field \"Rotate to\".");
            }
            catch (OverflowException)
            {
                MessageBox.Show("You have set too many cycles.");
            }
            
        }

        /// <summary>
        /// Функция смещения строки
        /// </summary>
        private void Work(int ROTN)
        {
            cipher.Enter = (new TextRange(richTextBoxEnter.Document.ContentStart,
                                          richTextBoxEnter.Document.ContentEnd).Text).ToLower();

            FlowDocument document = new FlowDocument();
            document.Blocks.Add(new Paragraph(new Bold(new Run(cipher.Rotate(ROTN)))));
            richTextBoxAnswer.Document = document;
        }

        /// <summary>
        /// Функция смены текстового блока в зависимости от угадывания смещения
        /// </summary>
        private void Guess(int ROTN)
        {
            if (ROTN == 0)
            {
                textBlockGuess.Text = "I think, \nthat the word\nis not \nencrypted";
                return;
            }

            textBlockGuess.Text = "Possible \ndecryption \nwith a rotate\nof " + (26 + ROTN);
        }

        #endregion

        
    }
}
