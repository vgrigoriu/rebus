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
    }
}
