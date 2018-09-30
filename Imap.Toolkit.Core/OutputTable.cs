using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace Imap.Toolkit.Core
{
    public class OutputTable
    {
        public OutputTable()
        {
        }

        public OutputTable(params string[] captions)
            : this(captions?.Select(c => new OutputColumn { Caption = c }).ToArray())
        {
        }

        public OutputTable(params OutputColumn[] columns)
        {
            if (columns != null)
                Columns.AddRange(columns);
        }

        public OutputTable AddColumn(string caption, string format = "", OutputAlignment alignment = OutputAlignment.Left)
        {
            Columns.Add(new OutputColumn { Caption = caption, Format = format, Alignment = alignment });
            return this;
        }

        public OutputTable AddColumns(params string[] captions)
        {
            if (captions != null)
                Columns.AddRange(captions?.Select(c => new OutputColumn { Caption = c }).ToArray());
            return this;
        }

        public List<OutputColumn> Columns { get; } = new List<OutputColumn>();

        public List<OutputRowBase> Rows { get; } = new List<OutputRowBase>();
        
        public OutputRow AddRow(params object[] values)
        {
            var row = new OutputRow(values);
            Rows.Add(row);
            return row;
        }

        public OutputRowSeparator AddSeparator()
        {
            var row = new OutputRowSeparator();
            Rows.Add(row);
            return row;
        }

        public void WriteTo(TextWriter writer)
        {
            List<OutputRowBase> lines = new List<OutputRowBase>
            {
                new OutputRowHeader(),
                new OutputRowSeparator()
            };            

            lines.AddRange(Rows);

            foreach (var line in lines)
            {
                line.Formatted = new List<string>();
                line.Format(Columns);
            }
                        
            foreach (var line in lines)
            {
                line.Pad(Columns);
                line.WriteTo(writer);               
            }
        }
    }
}
