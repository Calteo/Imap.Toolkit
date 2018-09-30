using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imap.Toolkit.Core
{
    internal class OutputRowHeader : OutputRowBase
    {
        internal override void Format(List<OutputColumn> columns)
        {
            foreach (var column in columns)
            {
                Formatted.Add(column.Caption);
                column.Width = column.Caption.Length;
            }
        }

        internal override void Pad(List<OutputColumn> columns)
        {
            for (int i = 0; i < columns.Count; i++)
            {
                var column = columns[i];
                Formatted[i] = Formatted[i].PadRight(column.Width);                
            }
        }
    }
}
