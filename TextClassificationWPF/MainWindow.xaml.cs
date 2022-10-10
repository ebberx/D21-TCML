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
using TextClassificationWPF.Controller;
using TextClassificationWPF.Domain;

namespace TextClassificationWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow() {
            InitializeComponent();

            /////* Taken from Program.cs */
            KnowledgeBuilder nb = new KnowledgeBuilder();

            // initiate the learning process

            nb.Train();

            // getting the (whole) knowledge found in ClassA and in ClassB
            Knowledge k = nb.GetKnowledge();


            // get a part of the knowledge - here just for debugging
            BagOfWords bof = k.GetBagOfWords();

            List<string> entries = bof.GetEntriesInDictionary();


            Console.WriteLine("Showing the list of entries in the BagOfWords (initially with wrong entries) :");
            foreach (string entry in entries) {
                Console.WriteLine(entry);
            }

            Console.Read();
            ////
        }
    }
}
