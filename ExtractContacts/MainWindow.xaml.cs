using System;
using System.Collections.Generic;
using System.IO;
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
using Domains;
using ExtractContactData;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace ExtractContacts
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var r = new Parser();
            string myText = new TextRange(html.Document.ContentStart, html.Document.ContentEnd).Text;
          

            resultText.Document.Blocks.Clear();
            resultText.Document.Blocks.Add(new Paragraph(new Run(r.ParseAndReturnAsText(myText))));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var r = new Parser();
            string myText = new TextRange(html.Document.ContentStart, html.Document.ContentEnd).Text;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, r.ParseAndReturnAsText(myText));
                
            }
        }
    }
}
