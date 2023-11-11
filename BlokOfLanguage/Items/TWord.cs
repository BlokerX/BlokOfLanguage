namespace BlokOfLanguage.Items
{
    public class TWord
    {
        public int ID { get; set; }

        public string Word { get; set; }

        public string TranslatedWord { get; set; }

        public string PartOfSpeech { get; set; }

        public string Description { get; set; }

        public string DifficultLevel { get; set; }

        public List<string> ExampleSentences { get; set; }

        public List<string> Synonyms { get; set; }

        public List<string> Antonyms { get; set; }

        public List<string> RelatedWords { get; set; }

        public bool IsDifficultWord { get; set; }

        public bool IsFavourite { get; set; }

        public string DateOfAdded { get; set; }
    }
}
