namespace RebusLib
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Keeps a list of weighted words, can retrieve those matching a regex
    /// </summary>
    public class Wordlist
    {
        private List<Word> wordCollection = new List<Word>();

        public ICollection<Word> Words
        {
            get
            {
                return this.wordCollection;
            }
        }

        public ICollection<Word> WordsMatching(Regex regex)
        {
            return (from Word word in this.wordCollection
                   where regex.IsMatch(word.Value)
                   select word).ToList();
        }

        public void Add(IEnumerable<string> words, int weight)
        {
            foreach (string word in words)
            {
                this.Add(word, weight);
            }
        }

        public void Add(string value, int weight)
        {
            Word word = this.GetWord(value);
            if (word == null)
            {
                word = new Word(value, weight);
                this.wordCollection.Add(word);
            }
            else
            {
                Console.WriteLine("Already added word {0}", value);
                word.Weight = System.Math.Max(weight, word.Weight);
            }
        }

        public Word GetWord(string value)
        {
            Word word = null;
            try
            {
                word = this.wordCollection.First(w => w.Value == value);
            }
            catch (InvalidOperationException) 
            { 
                /*ignore*/ 
            }

            return word;
        }
    }
}
