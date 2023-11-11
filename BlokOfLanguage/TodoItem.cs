namespace BlokOfLanguage
{
    public class TodoItem
    {
        public bool Done { get; internal set; }
        
        public int ID { get; internal set; }
        
        public string Word { get; internal set; }
        
        public string TranslatedWord { get; internal set; }
        
        public DateTime DateAdded { get; internal set; }

        public bool IsFavourite { get; internal set; }

        public bool IsDifficultWord { get; internal set; }

    }
}