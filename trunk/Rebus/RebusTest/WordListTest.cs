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


            Assert.That(list.Count, Is.EqualTo(3));
        }
    }
}
