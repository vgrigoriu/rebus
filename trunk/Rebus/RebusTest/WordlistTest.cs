namespace RebusTest
{
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    using NUnit.Framework;

    using RebusLib;

    /// <summary>
    /// Tests for Wordlist
    /// </summary>
    [TestFixture]
    public class WordlistTest : AssertionHelper
    {
        private string[] lowPriorityStrings = new string[] { "ana", "are", "mere" };
        private string[] highPriorityStrings = new string[] { "hopai", "tupai", "diridiridam", "are" };
        private int highWeight = 14;
        private int lowWeight = 1;

        private Wordlist wordlist;

        [TestFixtureSetUp]
        public void Setup()
        {
            this.wordlist = new Wordlist();
            this.wordlist.Add(this.lowPriorityStrings, this.lowWeight);
        }

        [Test]
        public void AddWords()
        {
            Word[] words = new Word[this.wordlist.Words.Count];
            this.wordlist.Words.CopyTo(words, 0);
            Expect(words.Length, Is.EqualTo(3));
            foreach (string s in this.lowPriorityStrings)
            {
                Expect(List.Map(words).Property("Value"), Has.Some.EqualTo(s));
            }

            Expect(List.Map(words).Property("Weight"), Has.All.EqualTo(this.lowWeight));
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
            ICollection<Word> wordsMatching = this.wordlist.WordsMatching(secondLetterR);
            Word[] words = new Word[wordsMatching.Count];
            wordsMatching.CopyTo(words, 0);

            Expect(words.Length, EqualTo(1));
            Expect(List.Map(words).Property("Value"), Has.All.EqualTo("are"));
        }

        [Test]
        public void LowThenHighTest()
        {
            Wordlist lowHighList = new Wordlist();
            lowHighList.Add(this.lowPriorityStrings, this.lowWeight);
            lowHighList.Add(this.highPriorityStrings, this.highWeight);

            Word are = lowHighList.GetWord("are");
            Expect(are.Weight, Is.EqualTo(this.highWeight));
        }

        [Test]
        public void HighThenLowTest()
        {
            Wordlist highLowList = new Wordlist();
            highLowList.Add(this.highPriorityStrings, this.highWeight);
            highLowList.Add(this.lowPriorityStrings, this.lowWeight);

            Word are = highLowList.GetWord("are");
            Expect(are.Weight, Is.EqualTo(this.highWeight));
        }

        [Test]
        /**
         * 
         */
        public void InexistentWordTest()
        {
            Word doesNotExist = this.wordlist.GetWord("dezoxiribonucleic");
            Expect(doesNotExist, Is.Null);
        }
    }
}
