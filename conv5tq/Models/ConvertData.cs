using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conv5tq.Models
{
    public class ConvertData
    {
        public Category Category { get; set; }
        public List<Genre> Genre { get; set; }
        public List<Question> Question { get; set; }
        public List<Choice> Choice { get; set; }
    }
}
