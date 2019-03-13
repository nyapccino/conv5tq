using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conv5tq.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CSVModelAttribute : Attribute
    {
        /// <summary>ファイル名</summary>
        public string FileName { get; set; }
        /// <summary>区切り句</summary>
        public char Separator { get; set; }
    }
}
