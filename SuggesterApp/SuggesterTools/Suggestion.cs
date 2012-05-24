
namespace SuggesterTools
{
    public class Suggestion
    {
        public Suggestion() { }
        public Suggestion(int id, string text)
        {
            Id = id;
            Text = text;
        }
        public int Id { get; set; }
        public string Text { get; set; }
    }
}
