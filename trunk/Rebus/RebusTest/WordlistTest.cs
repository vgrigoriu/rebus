using System.IO;
using System.Linq;

using NUnit.Framework;

using RebusLib;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace RebusTest
{
    [TestFixture]
    public class WordlistTest : AssertionHelper
    {
        private string[] loPriStrings = new string[] { "ana", "are", "mere" };
        private string[] hiPriStrings = new string[] { "hopai", "tupai", "diridiridam", "are" };
        private int hiWeight = 14;
        private int loWeight = 1;

        private Wordlist wordlist;

        [TestFixtureSetUp]
        public void Setup()
        {
            wordlist = new Wordlist();
            wordlist.Add(loPriStrings, loWeight);
        }

        [Test]
        public void AddWords()
        {
            Word[] words = new Word[wordlist.Words.Count];
            wordlist.Words.CopyTo(words, 0);
            Expect(words.Length, Is.EqualTo(3));
            foreach (string s in loPriStrings)
            {
                Expect(List.Map(words).Property("Value"), Has.Some.EqualTo(s));
            }
            Expect(List.Map(words).Property("Weight"), Has.All.EqualTo(loWeight));
        }

        [Test]
        public void AddWordsFromFile()
        {
            string[] words = File.ReadAllLines(@"..\..\..\data\words.ro-ro.txt");
            int weight = 7;
            Wordlist list = new Wordlist();
            list.Add(words, weight);

            Expect(list.Words.Count, Is.EqualTo(134255));
        }

        [Test]
        public void WordsMatchingTest()
        {
            Regex secondLetterR = new Regex("^.r");
            ICollection<Word> wordsMatching = wordlist.WordsMatching(secondLetterR);
            Word[] words = new Word[wordsMatching.Count];
            wordsMatching.CopyTo(words, 0);

            Expect(words.Length, EqualTo(1));
            Expect(List.Map(words).Property("Value"), Has.All.EqualTo("are"));
        }

        [Test]
        public void LowThenHighTest()
        {
            Wordlist loHiList = new Wordlist();
            loHiList.Add(loPriStrings, loWeight);
            loHiList.Add(hiPriStrings, hiWeight);

            Word are = loHiList.GetWord("are");
            Expect(are.Weight, Is.EqualTo(hiWeight));
        }

        [Test]
        public void HighThenLowTest()
        {
            Wordlist hiLoList = new Wordlist();
            hiLoList.Add(hiPriStrings, hiWeight);
            hiLoList.Add(loPriStrings, loWeight);

            Word are = hiLoList.GetWord("are");
            Expect(are.Weight, Is.EqualTo(hiWeight));
        }

        [Test]
        public void InexistentWordTest()
        {
            Word doesNotExist = this.wordlist.GetWord("dezoxiribonucleic");
            Expect(doesNotExist, Is.Null);
        }
    }
}
