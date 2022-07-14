namespace ColorBlindTestGeneratorBlazor.Models
{
    public class TextDataModel
    {
        public TextDataModel()
        {
            Text = "New Test";
            Color = "Red";
        }

        public TextDataModel(string text, string color)
        {
            Text = text;
            Color = color;
        }

        public string Text { get; set; }
        public string Color { get; set; }
    }
}
