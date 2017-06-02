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
            chart.DataContext = cipher.Frequency;
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
            cipher.NullFrequency();

            string[] items = (new TextRange(richTextBoxEnter.Document.ContentStart,
                                            richTextBoxEnter.Document.ContentEnd).Text).ToLower().Split(' ');

            for (int i = 0; i < items.Length; i++)
            {
                items[i] = Cleaner.CleanWord(items[i]);
                if (items.Length > 1)
                    ;
            }

            if (items.Length == 0)
                Guess(0);

            Dictionary<int, int> Mathes = new Dictionary<int, int>();
            Mathes.Add(0, items.Length);
            foreach (string item in items)
            {
                cipher.Enter = item;
                cipher.CalculateFrequency();
                List<int> guess = cipher.Guess();
                if (guess.Count != 0)
                {
                    foreach (int buff in guess)
                    {
                        if (Mathes.ContainsKey(buff))
                            Mathes[buff]++;
                        else
                            Mathes[buff] = 1;
                    }

                    Mathes[0]--;
                }
            }
            ChartChange();

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
        private void textBlockGuessOnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
             textBoxRotateTo.Text = textBlockGuess.Text.Substring(37);
        }
        private void MenuItemRemoveOnClick(object sender, RoutedEventArgs e)
        {
            FlowDocument document = new FlowDocument();
            document.Blocks.Add(new Paragraph(new Bold(new Run(""))));
            richTextBoxEnter.Document = document;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Переходная функция к вычислению смещенной строки.
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
        /// Функция смещения строки.
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
        /// Функция смены текстового блока в зависимости от "угадывания смещения".
        /// </summary>
        private void Guess(int ROTN)
        {
            if (ROTN == 0)
            {
                textBlockGuess.Text = "I think, that the word is not encrypted";
                textBlockGuess.Cursor = Cursors.Arrow;
                textBlockGuess.IsEnabled = false;
                return;
            }

            textBlockGuess.Text = "Possible decryption with a rotate of " + (26 + ROTN);
            textBlockGuess.Cursor = Cursors.Hand;
            textBlockGuess.IsEnabled = true;
        }

        /// <summary>
        /// Функция перерисовки гистограммы.
        /// </summary>
        private void ChartChange()
        {
            columnSeries.ItemsSource = cipher.Frequency;
        }

        #endregion

    }
}
