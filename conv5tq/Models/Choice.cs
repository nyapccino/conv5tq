using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Conv5tq.Attributes;

namespace Conv5tq.Models
{
    [CSVModel(FileName = "choice.txt", Separator = '\t')]
    public class Choice
    {
        [CSVColumn(Order = 0)]
        public int Id { get; set; }
        [CSVColumn(Order = 1)]
        public int QuestionId { get; set; }
        [CSVColumn(Order = 2)]
        public int IsCorrect { get; set; }
        [CSVColumn(Order = 3)]
        public string ChoiceText { get; set; }
    }
}
