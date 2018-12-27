namespace ScalableWeb.Diff.Model.Models
{
    public class DiffEntry
    {
        public DiffEntry(int offset, int length)
        {
            Offset = offset;
            Length = length;
        }

        public int Offset { get; set; }
        public int Length { get; set; }
    }
}