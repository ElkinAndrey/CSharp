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

namespace RussianLettersAboveText
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int fontSize = 14;

        private int FontSize 
        { 
            get 
            { 
                return fontSize; 
            } 
            set
            {
                fontSize = value;
                result.FontSize = fontSize;
                downText.FontSize = fontSize;
                upText.FontSize = fontSize;
            } 
        }

        public MainWindow()
        {
            InitializeComponent();
            FontSize = 30;
            result.Text = "a";
            for (int i = 0; i < 300; i++)
                result.Text +=((char)11766).ToString();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            result.Text = WorkWithText.RussianLettersAboveText(downText.Text, upText.Text);
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            result.Text = WorkWithText.RussianLettersAboveText(downText.Text, upText.Text);
        }
    }
}
