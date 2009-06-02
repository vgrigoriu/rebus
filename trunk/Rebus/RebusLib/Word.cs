namespace RebusLib
{
    public class Word
    {
        public string Value { get; set; }
        public int Weight { get; set; }

        public Word(string word, int weight)
        {
            this.Value = word;
            this.Weight = weight;
        }
    }
}
