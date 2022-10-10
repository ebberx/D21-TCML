using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextClassificationWPF.Foundation
{
    public class StringOperations
    {
        const int EXTENSIONLENGTH = 4;
        public static string getFileName(string path)
        {
            // a) skipping the extension .txt (4 chars)
            string name = path.Substring(0,path.Length-EXTENSIONLENGTH);

            // b) skipping the front part
            int startPos = name.LastIndexOf('\\')+1;
            name = name.Substring(startPos,name.Length-startPos);

            return name;
        }

        public static string getProjectPath() {

            string path = Environment.CurrentDirectory;
            bool foundProjectFolder = false;
            int subFoldersSearched = 0;

            while (true) {
                if (Directory.GetParent(path).Name == "D21-TCML") {
                    path = Directory.GetParent(path).FullName;
                    foundProjectFolder = true;
                    break;
                }
                if (subFoldersSearched > 5)
                    break;

                path = Directory.GetParent(path).FullName;
                subFoldersSearched++;
            }

            // Return project folder path if found, else null
            if (foundProjectFolder)
                return path;
            else
                return null;
        }
    }
}
