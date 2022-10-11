using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using TextClassificationWPF.Business;
using TextClassificationWPF.Controller;
using TextClassificationWPF.Domain;

namespace Test
{
    [TestClass]
    public class TokenizationTest {

        [TestMethod]
        public void Tokenization01() {
            List<string> tokens = Tokenization.Tokenize("\"MAjEstY!\" hE sAId, \"thErE's A bUg!\"");

            List<string> expected = new List<string>();
            expected.Add("majesty");
            expected.Add("said");
            expected.Add("there");
            expected.Add("bug");

            int i = 0;
            foreach (string s in tokens) {
                Assert.AreEqual(expected[i++], s);
            }
        }

        [TestMethod]
        public void Tokenization02() {
            List<string> tokens = Tokenization.Tokenize("CRiKey, SiR! THe Tea'S GeTTiNG CoLD...");

            List<string> expected = new List<string>();
            expected.Add("crikey");
            expected.Add("sir");
            expected.Add("the");
            expected.Add("tea");
            expected.Add("getting");
            expected.Add("cold");

            int i = 0;
            foreach (string s in tokens) {
                Assert.AreEqual(expected[i++], s);
            }
        }

        [TestMethod]
        public void Tokenization03() {
            List<string> tokens = Tokenization.Tokenize("The Cold-Brew-Machine is functional!");

            List<string> expected = new List<string>();
            expected.Add("the");
            expected.Add("cold");
            expected.Add("brew");
            expected.Add("machine");
            expected.Add("functional");

            int i = 0;
            foreach (string s in tokens) {
                Assert.AreEqual(expected[i++], s);
            }
        }

    }
}
