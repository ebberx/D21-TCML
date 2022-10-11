// deprecated (THA)
namespace TextClassificationWPF.Domain
{
    public class WordItem
    {
        string _word;
        int _occurency;

        public WordItem(string word, int occurency) {
            _word = word;
            _occurency = occurency;
        }

        public string GetWord() {
            return _word;
        }

    }
}
