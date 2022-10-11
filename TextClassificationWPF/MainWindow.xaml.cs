using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
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

            KnowledgeBuilder nb = new KnowledgeBuilder(new List<string>() { "ClassA", "ClassB" });

            // initiate the learning process
            nb.Train();

            // getting the (whole) knowledge found in ClassA and in ClassB
            Knowledge k = nb.GetKnowledge();

            // get a part of the knowledge - here just for debugging
            BagOfWords bof = k.GetBagOfWords();

            List<string> entries = bof.GetEntriesInDictionary();

            Debug.WriteLine("Showing the list of entries in the BagOfWords (initially with wrong entries) :");
            foreach (string entry in entries) {
                Debug.WriteLine(entry);
            }
        }
    }
}
