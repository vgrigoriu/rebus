using System.IO;

using NUnit.Framework;

using RebusLib;

namespace RebusTest
{
    [TestFixture]
    public class WordListTest
    {
        [Test]
        public void AddWords()
        {
            WordList list = new WordList();
            list.Add(new string[] {"ana", "are", "mere"}, 3);

            string[] strings = new string[] { "ana", "are", "mere" };
            int[] weights = new int[] { 3, 3, 3 };


            Assert.That(list.Count, Is.EqualTo(3));
            Assert.That(list, Is.All.InstanceOf(typeof(Word)));
            Assert.That(List.Map(list).Property("Value"), Is.EqualTo(strings));
            Assert.That(List.Map(list).Property("Weight"), Is.EqualTo(weights));
        }

        [Test]
        public void AddWordsFromFile()
        {
            string[] words = File.ReadAllLines(@"..\..\..\data\words.ro-ro.txt");
            WordList list = new WordList();
            list.Add(words, 7);
            Assert.That(list.Count, Is.EqualTo(134259));
            Assert.That(List.Map(list).Property("Weight"), Has.All.EqualTo(7));
        }
    }
}
