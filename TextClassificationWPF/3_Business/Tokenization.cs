using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace TextClassificationWPF.Business
{

    public class Tokenization
    {
        private const int SMALLESTWORDLENGTH = 3;

        public static List<string> Tokenize(string originalText) {
            List<string> words = new List<string>();

            foreach (string token in originalText.Split(' ')) {

                // Handle conjoined words
                if (token.Contains("-")) {
                    string[] additonalWords = token.Split('-');

                    foreach (string addToken in additonalWords) {
                        string cleanAddToken = CleanToken(addToken);
                        if(!IsAShortWord(cleanAddToken))
                            words.Add(cleanAddToken);
                    }
                    continue; // Skip adding conjoined-word token below
                }
                
                string cleanToken = CleanToken(token);
                if (!IsAShortWord(cleanToken))
                    words.Add(cleanToken);
            }
            
            return words;
        }

        public static string CleanToken(string token) {
            token = token.Trim();
            token = RemovePunctuation(token);
            token = token.ToLower();
            return token;
        }

        public static bool IsAShortWord(string token) {
            if (token.Length < SMALLESTWORDLENGTH) {
                return true;
            }
            return false;
        }

        public static string RemovePunctuation(string token) {
            token = token.Trim();
            string[] punctuations = { ".", ",", "\"", "?", "\n", "\r", "!", ":", ";", "”", "“"};

            foreach (string s in punctuations) {
                if (token.Contains(s.ToString())) {
                    token = token.Replace(s.ToString(), String.Empty);
                }
            }

            /*
            for (int i = 0; i < punctuations.Length; i++) {
                string ch = punctuations[i].ToString();
                if (token.StartsWith(ch)) {
                    return token.Substring(1);
                }
                else if (token.EndsWith(ch)) {
                    return token.Substring(0, token.Length - 1);
                }
            }
            */
            return token;
        }
    }
}
