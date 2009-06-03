using System.IO;
using System.Linq;

using NUnit.Framework;

using RebusLib;
using System.Collections;
using System.Text.RegularExpressions;

namespace RebusTest
{
    [TestFixture]
    public class WordlistTest : AssertionHelper
    {
        [Test]
        public void AddWords()
        {
            string[] strings = new string[] { "ana", "are", "mere" };
            int weight = 3;

            Wordlist list = new Wordlist();
            list.Add(strings, weight);

            Expect(list.Words.Count, Is.EqualTo(3));
            Expect(list.Words, Is.All.InstanceOf(typeof(Word)));
            foreach (string s in strings)
            {
                Expect(List.Map(list.Words).Property("Value"), Has.Some.EqualTo(s));
            }
            Expect(List.Map(list.Words).Property("Weight"), Has.All.EqualTo(weight));
        }

        [Test]
        public void AddWordsFromFile()
        {
            string[] words = File.ReadAllLines(@"..\..\..\data\words.ro-ro.txt");
            int weight = 7;
            Wordlist list = new Wordlist();
            list.Add(words, weight);

            Expect(list.Words.Count, Is.EqualTo(134259));
            Expect(List.Map(list.Words).Property("Weight"), Has.All.EqualTo(weight));
        }

        [Test]
        public void WordsMatchingTest()
        {
            string[] strings = new string[] { "ana", "are", "mere" };
            int weight = 3;

            Wordlist list = new Wordlist();
            list.Add(strings, weight);

            Regex secondLetterR = new Regex("^.r");
            ICollection wordsMatching = list.WordsMatching(secondLetterR);

            Expect(wordsMatching.Count, EqualTo(1));
            Expect(List.Map(wordsMatching).Property("Value"), Has.All.EqualTo("are"));
        }
    }
}
