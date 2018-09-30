using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imap.Toolkit.Core
{
    public abstract class OutputRowBase
    {
        internal abstract void Format(List<OutputColumn> columns);
        internal abstract void Pad(List<OutputColumn> columns);

        internal List<string> Formatted { get; set; }

        internal void WriteTo(TextWriter writer)
        {
            writer.WriteLine(string.Join(" ", Formatted.ToArray()));
        }
    }
}
