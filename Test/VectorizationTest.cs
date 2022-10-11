using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TextClassificationWPF.Controller;
using TextClassificationWPF.Domain;

namespace Test
{
    [TestClass]
    public class VectorizationTest
    {
        [TestMethod]
        public void Vectorization01() {

            KnowledgeBuilder nb = new KnowledgeBuilder(new List<string>() { "ClassA", "ClassB" });
            nb.Train();

            Knowledge k = nb.GetKnowledge();
            BagOfWords bof = k.GetBagOfWords();

            List<string> entries = bof.GetEntriesInDictionary();

            Vectors vectors = k.GetVectors();

            foreach (string entry in entries) {
                Debug.WriteLine(entry);
            }

        }
    }
}
