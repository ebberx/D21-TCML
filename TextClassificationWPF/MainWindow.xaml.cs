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
        KnowledgeBuilder nb;

        public MainWindow() {
            InitializeComponent();
        }

        private void OnClickTrain(object sender, RoutedEventArgs e) {
            nb = new KnowledgeBuilder(new List<string>() { "ClassA", "ClassB" });
            nb.Train();
            MessageBox.Show("Training has completed", "Info message");
        }

        private void OnClickClassify(object sender, RoutedEventArgs e) {
            // getting the (whole) knowledge found in ClassA and in ClassB
            Knowledge k = nb.GetKnowledge();

            List<string> entries = k.GetBagOfWords().GetEntriesInDictionary();

            Debug.WriteLine("Showing the list of entries in the BagOfWords (initially with wrong entries) :");
            foreach (string entry in entries) {
                Debug.WriteLine(entry);
            }
        }
    }
}
