namespace Imap.Toolkit.Core
{
    public class OutputColumn
    {
        public string Caption { get; set; } = "";
        public string Format { get; set; } = "";
        public OutputAlignment Alignment { get; set; } = OutputAlignment.Left;

        internal int Width { get; set; }
    }

    public enum OutputAlignment
    {
        Left,
        Center,
        Right
    }
}
