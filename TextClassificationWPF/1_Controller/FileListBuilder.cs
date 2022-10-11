using System.Collections.Generic;
using TextClassificationWPF.FileIO;

namespace TextClassificationWPF.Controller
{
    public class FileListBuilder
    {
        
        private Dictionary<string, List<string>> _fileLists;
        private CategoryHandler _fileAdapter;

        public FileListBuilder() {
            _fileLists = new Dictionary<string, List<string>>();
            _fileAdapter = new CategoryHandler("txt");
        }

        public Dictionary<string, List<string>> GetFileLists() {
            return _fileLists;
        }

        public void GenerateFileNames(string folder) {
            List<string> fileNames = _fileAdapter.GetAllFileNames(folder);
            _fileLists.Add(folder, fileNames);
        }
    }
}
