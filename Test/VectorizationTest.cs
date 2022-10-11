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

            KnowledgeBuilder nb = new KnowledgeBuilder(new List<string>() { "VectorizationTest" });
            nb.Train();

            Knowledge k = nb.GetKnowledge();
            BagOfWords bof = k.GetBagOfWords();

            List<string> entries = bof.GetEntriesInDictionary();

            List<List<bool>> vectors = k.GetVectors().GetVectorListForCategory("VectorizationTest");
            Debug.WriteLine(entries.Count);
            // Check vector length is correct
            Assert.AreEqual(entries.Count, vectors[0].Count);

            // Check vector is correct
            foreach (bool b in vectors[0])
                Assert.AreEqual(b, true);

        }

        [TestMethod]
        public void Vectorization02() {

            // Loremipsum: 64 unique tokens
            // VectorizationTest: 14 unique tokens
            KnowledgeBuilder nb = new KnowledgeBuilder(new List<string>() { "LoremIpsum", "VectorizationTest"});
            nb.Train();
            Knowledge k = nb.GetKnowledge();
            List<string> entries = k.GetBagOfWords().GetEntriesInDictionary();


            List<List<bool>> vectors = k.GetVectors().GetVectorListForCategory("VectorizationTest");

            Debug.WriteLine("BagOfWords count: " + entries.Count);

            // Check vector length is correct
            Assert.AreEqual(entries.Count, vectors[0].Count);

            bool[] expectedVector = {
                false, false, false, true, false, true, false, true, false, false,
                true, false, false, false, false, false, true, false, false, false,
                false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, true, false, false, false,
                false, false, false, false, false, false, false, false, false, true,
                false, false, false, false, false, false, false, false, false, true,
                false, true, false, false, false, true, true, true, false, false,
                false, false, false, false, false, false, true, true };

            // Check vector is correct
            for (int i = 0; i < entries.Count; i++)
                Assert.AreEqual(vectors[0][i], expectedVector[i]);

        }
    }
}
