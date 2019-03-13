using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conv5tq.Models
{
    public class ConvertParameter
    {
        public string InputFileName { get; set; }
        public string OutputPath { get; set; }
        public int StartCategoryId { get; set; } = 1;
        public int StartGenreId { get; set; } = 1;
        public int StartQuestionId { get; set; } = 1;
    }
}
