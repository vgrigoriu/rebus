using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;

namespace RebusLib
{
    public class Wordlist
    {
        private List<Word> wordCollection = new List<Word>();

        public ICollection Words
        {
            get
            {
                return wordCollection;
            }
        }

        public ICollection WordsMatching(Regex regex)
        {
            return (from Word word in wordCollection
                   where regex.IsMatch(word.Value)
                   select word).ToList();
        }

        public void Add(IEnumerable<string> words, int weight)
        {
            foreach (string word in words)
            {
                this.wordCollection.Add(new Word(word, weight));
            }
        }
    }
}
