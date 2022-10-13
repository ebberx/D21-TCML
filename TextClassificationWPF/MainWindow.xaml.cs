using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Numerics;
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
        KnowledgeBuilder? nb;

        public MainWindow() {
            InitializeComponent();
        }

        private void OnClickTrain(object sender, RoutedEventArgs e) {
            nb = new KnowledgeBuilder(new List<string>() { "ClassA", "ClassB" });
            nb.Train();
            MessageBox.Show("Training has completed", "Info message");
        }

        private void OnClickClassify(object sender, RoutedEventArgs e) {
            if(nb is null)
                return;

            // Create vector of words from input text
            List<bool> vector = nb.CreateVector(textbox.Text);

            // KNN
            KNNClassifier classifier = new KNNClassifier(nb.GetKnowledge().GetVectors(), 3);
            string classification = classifier.Classify(vector);

            resultLabel.Content = "Classified as: " + classification;

        }

        private void OnClickBenchmark(object sender, RoutedEventArgs e) {

            // Error handling
            if (nb is null) {
                resultLabel.Content = "Please train model first.";
                return;
            }

            List<string> categoryNames = new List<string>() { "Benchmark/PoolA", "Benchmark/PoolB"};

            // Read thorugh all files in folders
            FileListBuilder flb = new FileListBuilder();
            foreach (string s in categoryNames)
                flb.GenerateFileNames(s);

            // Get a list of files in each category
            Dictionary<string, List<string>> filesInCategories = flb.GetFileLists();

            // convert to vectors
            List<List<bool>> vectors = new List<List<bool>>();
            List<string> fileCategories = new List<string>();

            foreach (string category in categoryNames) {
                List<string>? fileList;

                filesInCategories.TryGetValue(category, out fileList);
                if (fileList is null)
                    throw new ArgumentNullException();

                foreach (string file in fileList) {
                    vectors.Add(nb.CreateVector(File.ReadAllText(file)));
                    fileCategories.Add(category);
                }
            }

            // Benchmark classifier
            KNNClassifier classifier = new KNNClassifier(nb.GetKnowledge().GetVectors(), 3);

            // Classify each vector
            List<string> classificationOfBenchmark = new List<string>();
            foreach (List<bool> vector in vectors)
                classificationOfBenchmark.Add(classifier.Classify(vector));

            // Check results
            int correctClassifications = 0;
            for (int i = 0; i < classificationOfBenchmark.Count; i++) {
                string cat = "";
                if (fileCategories[i] == "Benchmark/PoolA")
                    cat = "ClassA";
                else
                    cat = "ClassB";

                if (classificationOfBenchmark[i] == cat)
                    correctClassifications++;
                Debug.WriteLine("Classification: " + classificationOfBenchmark[i] + " category: " + fileCategories[i]);
            }
    
            // DIsplay results
            resultLabel.Content = "Benchmark accuracy: " + correctClassifications + "/" + classificationOfBenchmark.Count;
        }
    }
}
