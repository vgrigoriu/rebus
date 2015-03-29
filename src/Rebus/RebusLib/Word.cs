namespace RebusLib
{
    /// <summary>
    /// A weighted word.
    /// </summary>
    public class Word
    {
        /// <summary>
        /// non-interesting constructor
        /// </summary>
        /// <param name="word">the actual string</param>
        /// <param name="weight">it's weight</param>
        public Word(string word, int weight)
        {
            this.Value = word;
            this.Weight = weight;
        }

        public string Value { get; set; }

        public int Weight { get; set; }
    }
}
