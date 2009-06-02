using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RebusLib
{
    class WordList: List<Word>
    {
        public void Add(IEnumerable<string> words, int weight)
        {
            foreach (string word in words)
            {
                this.Add(new Word(word, weight));
            }
        }
    }
}
