using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;

namespace TextClassificationWPF.Domain
{

    public class Vectors
    {
        private Dictionary<string, List<List<bool>>> _vectors;

        public Vectors() {
            _vectors = new Dictionary<string, List<List<bool>>>();
        }

        public void AddVector(string categoryName, List<bool> vector) {

            // If category does not exist in dictionary yet create it
            if (!_vectors.ContainsKey(categoryName)) {
                _vectors.Add(categoryName, new List<List<bool>>() { vector });
                return;
            }

            List<List<bool>>? vectorList = _vectors[categoryName];
            vectorList.Add(vector);
            _vectors[categoryName] = vectorList;
        }

        public List<List<bool>> GetVectorListForCategory(string category) {

            if (!_vectors.ContainsKey(category))
                throw new DataException("No valid key present in vector dictionary.");

            return _vectors[category];
        }

        public int GetVectorsCount() {
            int vectorCount = 0;

            // Count vectors
            foreach (KeyValuePair<string, List<List<bool>>> vectorsEntry in _vectors)
                foreach (List<bool> vectorList in vectorsEntry.Value)
                    vectorCount++;

            return vectorCount;
        }

        public Dictionary<string, List<List<bool>>> GetVectors() {
            return _vectors;
        }
    }
}
