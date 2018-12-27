namespace ScalableWeb.Diff.Model.Models
{
    public class Comparer
    {
        public Comparer()
        {
            Left = string.Empty;
            Right = string.Empty;
        }

        public int Id { get; set; }
        public string Left { get; set; }
        public string Right { get; set; }
    }
}