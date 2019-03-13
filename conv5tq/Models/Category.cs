using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Conv5tq.Attributes;

namespace Conv5tq.Models
{
    [CSVModel(FileName = "category.txt", Separator = '\t')]
    public class Category
    {
        [CSVColumn(Order = 0)]
        public int Id { get; set; }
        [CSVColumn(Order = 1)]
        public string CategoryName { get; set; }
    }
}
