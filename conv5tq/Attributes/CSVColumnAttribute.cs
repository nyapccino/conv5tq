using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conv5tq.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CSVColumnAttribute : Attribute
    {
        /// <summary>順序</summary>
        public int Order { get; set; }
        /// <summary>フォーマット</summary>
        public string Format { get; set; } = "{0}";
    }
}
