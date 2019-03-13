using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Conv5tq.Attributes;

namespace Conv5tq.Models
{
    [CSVModel(FileName = "question.txt", Separator = '\t')]
    public class Question
    {
        [CSVColumn(Order = 0)]
        public int Id { get; set; }
        [CSVColumn(Order = 1)]
        public int GenreId { get; set; }
        [CSVColumn(Order = 2)]
        public string QuestionText { get; set; }
    }
}
