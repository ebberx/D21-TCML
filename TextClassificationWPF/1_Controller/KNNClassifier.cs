using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Windows.Automation;
using System.Windows.Media.Converters;
using TextClassificationWPF.Domain;

namespace TextClassificationWPF.Controller
{
    public class KNNClassifier : IClassifier
    {
        Vectors _vectors;
        int _k = 0;

        public KNNClassifier(Vectors vectors, int k) {
            _vectors = vectors;
            _k = k;
        }

        public string Classify(List<bool> vectorToClassify) {

            // Error handling
            if (_k > _vectors.GetVectorsCount())
                throw new ArgumentException("KNN Search size is bigger than data pool!");

            // Convert supplied vector to normalised form
            List<double> vectorNorm = NormalizeVector(BoolVectorToDoubleVector(vectorToClassify));

            // Go thorugh all vectors and calculate distance between all vectors and vector-to-classify
            Dictionary<string, List<double>> _distances = new Dictionary<string, List<double>>();
            foreach (KeyValuePair<string, List<List<bool>>> kv in _vectors.GetVectors()) {
                string categoryValue = kv.Key;
                
                List<double> distancesInCategory = new List<double>();
                // Iterate list of vectors
                for (int i = 0; i < kv.Value.Count; i++) {
                    // Convert iterated vector to normalised form
                    List<double> vectorFromKnowledge = NormalizeVector(BoolVectorToDoubleVector(kv.Value[i]));
                    
                    // Calcualte distance
                    double distance = CalculateDistance(vectorFromKnowledge, vectorNorm);
                    distancesInCategory.Add(distance);
                    Debug.WriteLine("Added distance: " + distance + " for category: " + categoryValue);
                }

                _distances.Add(categoryValue, distancesInCategory);
            }

            // Generate list of shortest distances of length _k
            List<KeyValuePair<string, double>> shortestDistances = new List<KeyValuePair<string, double>>();
            foreach (KeyValuePair<string, List<double>> kv in _distances) {
                foreach (double val in kv.Value) {

                    if (shortestDistances.Count < _k) {
                        // Fill list until we have _k distances
                        shortestDistances.Add(new KeyValuePair<string, double>(kv.Key, val));
                        Debug.WriteLine("Filling list before count = _k: " + kv.Key + " - " + val);
                    }
                    else {
                        // Find highest value distance
                        KeyValuePair<string, double> highestDistance = shortestDistances.OrderByDescending(x => x.Value).First();

                        // If highest distance in our list of shortest distances is greater than the val we look at, replace it
                        if (highestDistance.Value > val) {
                            shortestDistances.Remove(highestDistance);
                            shortestDistances.Add(new KeyValuePair<string, double>(kv.Key, val));
                            Debug.WriteLine("New shortest: " + kv.Key + " - " + val);
                        }
                    }
                }
            }

            // Find most recurring category in shortestDistances list
            Dictionary<string, int> categoryFrequency = new Dictionary<string, int>();
            foreach (KeyValuePair<string, double> kv in shortestDistances) {
                if (!categoryFrequency.ContainsKey(kv.Key)) {
                    categoryFrequency.Add(kv.Key, 1);
                }
                else {
                    categoryFrequency[kv.Key]++;
                }
                Debug.WriteLine(kv.Key + " | " + kv.Value);
            }

            // Return the most recurring key
            return categoryFrequency.OrderByDescending(x => x.Value).First().Key;
        }

        private double CalculateDistance(List<double> vec1, List<double> vec2) {
            
            if (vec1.Count != vec2.Count)
                throw new ArgumentException("Supplied vectors are not of equal length.");
            
            double SumOfComponents = 0;
            for (int i = 0; i < vec1.Count; i++) {
                SumOfComponents += Math.Pow(vec1[i] - vec2[i], 2);
            }
            return Math.Sqrt(SumOfComponents);
        }

        private List<double> NormalizeVector(List<double> vector) {
            double SumOfComponents = 0;
            foreach (double d in vector) {
                SumOfComponents += d * d;
            }
            double factor = Math.Sqrt(SumOfComponents);

            for (int i = 0; i < vector.Count; i++)
                vector[i] /= factor;

            return vector;
        }

        private List<double> BoolVectorToDoubleVector(List<bool> vector) {
            List<double> retList = new List<double>();
            foreach (bool b in vector) { 
                if(b)
                    retList.Add(1);
                else
                    retList.Add(0);
            }
            return retList;
        }
    }
}
