using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TextClassificationWPF.Domain;
using TextClassificationWPF.FileIO;
using TextClassificationWPF.Foundation;

namespace Test
{
    [TestClass]
    public class Testing
    {

        [TestMethod]
        public void TestWordItemGetWord() {
            // arrange
            string expected = "nice";
            WordItem wI = new WordItem("nice", 0);

            // act
            string actual = wI.GetWord();

            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestStringOperationsGetFileName() {
            // deprecated - use 
            // arrange
            string expected = "Suduko";
            string path = "c:\\users\\tha\\documents\\Suduko.txt";

            // act
            string actual = StringOperations.getFileName(path);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestFileGetAllFileNames() {
            // arrange
            string folderA = "ClassA";
            string fileType = "txt";
            List<string> expected = new List<string>();
            expected.Add(StringOperations.getProjectPath() + "\\Resources\\" + folderA + "\\Chiefs baffled by roughing the passer penalty on Chris Jones." + fileType);
            expected.Add(StringOperations.getProjectPath() + "\\Resources\\" + folderA + "\\Josh McDaniels, Raiders 'all-in' on aggressive playcall late." + fileType);
            expected.Add(StringOperations.getProjectPath() + "\\Resources\\" + folderA + "\\Ohio State passes Alabama, Georgia as CFP title betting favorite." + fileType);
            expected.Add(StringOperations.getProjectPath() + "\\Resources\\" + folderA + "\\Ref Chris Jones landed on Derek Carr with full body weight, hence flag." + fileType);
            expected.Add(StringOperations.getProjectPath() + "\\Resources\\" + folderA + "\\Seahawks RB Rashaad Penny sidelined for rest of season." + fileType);

            // act
            CategoryHandler fa = new CategoryHandler(fileType);
            List<string> actual = fa.GetAllFileNames(folderA);

            // assert
            Assert.AreEqual(expected.Count, actual.Count);
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
            Assert.AreEqual(expected[2], actual[2]);
        }
        [TestMethod]
        public void TestGetFilePathA() {
            // arrange
            string folderA = "ClassA";
            string fileType = "txt";
            string fileName = "filnavn";
            string expected = StringOperations.getProjectPath() + "\\Resources\\" + folderA + "\\filnavn." + fileType;

            // act
            CategoryHandler tf = new CategoryHandler(fileType);
            string actual = tf.GetFilePath(folderA, fileName);

            // assert
            Assert.AreEqual(expected, actual);
        }


    }
}
