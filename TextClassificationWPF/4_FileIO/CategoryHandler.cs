using System.Collections.Generic;
using System.IO;
using TextClassificationWPF.Foundation;

namespace TextClassificationWPF.FileIO
{
    public class CategoryHandler
    {
        string PROJECTPATH = StringOperations.getProjectPath();

        string _fileType = "txt";

        public CategoryHandler(string fileType) {
            _fileType = fileType;
        }

        public List<string> GetAllFileNames(string folderName) {
            List<string> fileNames = new List<string>();
            string[] paths = Directory.GetFiles(PROJECTPATH + "\\Resources\\" + folderName, "*." + _fileType);

            foreach (string path in paths) {
                fileNames.Add(path);
            }
            return fileNames;
        }

        public string GetFilePath(string category, string fileName) {
            return @PROJECTPATH + "\\Resources\\" + category + "\\" + fileName + "." + _fileType;
        }
    }
}
