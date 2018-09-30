using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imap.Toolkit.Core
{
    public class OutputRow : OutputRowBase
    {
        public OutputRow(params object[] values)
        {
            Values.AddRange(values ?? new object[0]);
        }

        public List<object> Values { get; } = new List<object>();

        internal override void Format(List<OutputColumn> columns)
        {
            for (int i = 0; i < columns.Count; i++)
            {
                var column = columns[i];
                var text = Values.Count > i ? string.Format("{0:" + column.Format + "}", Values[i]) : "";
                Formatted.Add(text);
                column.Width = Math.Max(column.Width, text.Length);
            }
        }

        internal override void Pad(List<OutputColumn> columns)
        {
            for (int i = 0; i < columns.Count; i++)
            {
                var column = columns[i];
                switch (column.Alignment)
                {
                    case OutputAlignment.Left:
                        Formatted[i] = Formatted[i].PadRight(column.Width);
                        break;
                    case OutputAlignment.Right:
                        Formatted[i] = Formatted[i].PadLeft(column.Width);
                        break;
                    case OutputAlignment.Center:
                        Formatted[i] = Formatted[i].PadLeft(column.Width / 2);
                        Formatted[i] = Formatted[i].PadRight(column.Width);
                        break;
                }
            }
        }
    }
}
