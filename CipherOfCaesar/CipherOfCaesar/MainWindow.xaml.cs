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
                MessageBox.Show("Please, enter only integer number in the field \"Rotate to\"");
            }
            catch(OverflowException)
            {
                MessageBox.Show("");
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

        #endregion
    }
}
