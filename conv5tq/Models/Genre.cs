using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Conv5tq.Attributes;

namespace Conv5tq.Models
{
    [CSVModel(FileName = "genre.txt", Separator = '\t')]
    public class Genre
    {
        [CSVColumn(Order = 0)]
        public int Id { get; set; }
        [CSVColumn(Order = 1)]
        public int CategoryId { get; set; }
        [CSVColumn(Order = 2)]
        public string GenreName { get; set; }
        public int StartId { get; set; }
        public int EndId { get; set; }
    }
}
