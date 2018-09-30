using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imap.Toolkit.Core
{
    public class OutputRowSeparator : OutputRowBase
    {
        public OutputRowSeparator(char separator = '-')
        {
            Separator = separator;
        }

        public char Separator { get; private set; }

        internal override void Format(List<OutputColumn> columns)
        {
            Formatted = new List<string>();
        }

        internal override void Pad(List<OutputColumn> columns)
        {
            foreach (var column in columns)
            {
                Formatted.Add("".PadRight(column.Width, Separator));
            }
        }
    }
}
