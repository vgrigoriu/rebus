using System.IO;

using NUnit.Framework;

using RebusLib;

namespace RebusTest
{
    [TestFixture]
    public class WordListTest : AssertionHelper
    {
        [Test]
        public void AddWords()
        {
            string[] strings = new string[] { "ana", "are", "mere" };
            int weight = 3;

            WordList list = new WordList();
            list.Add(strings, weight);

            Expect(list.Count, Is.EqualTo(3));
            Expect(list, Is.All.InstanceOf(typeof(Word)));
            foreach (string s in strings)
            {
                Expect(List.Map(list).Property("Value"), Has.Some.EqualTo(s));
            }
            Expect(List.Map(list).Property("Weight"), Has.All.EqualTo(weight));
        }

        [Test]
        public void AddWordsFromFile()
        {
            string[] words = File.ReadAllLines(@"..\..\..\data\words.ro-ro.txt");
            int weight = 7;
            WordList list = new WordList();
            list.Add(words, weight);

            Expect(list.Count, Is.EqualTo(134259));
            Expect(List.Map(list).Property("Weight"), Has.All.EqualTo(weight));
        }
    }
}
