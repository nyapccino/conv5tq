using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Conv5tq.Models;
using Conv5tq.Models.Raw;
using Conv5tq.Util;

namespace Conv5tq.Logic
{
    public class ConvertLogic
    {
        public void Execute(ConvertParameter param)
        {
            var rawData = this.ByteParser(param);
            var convertData = this.ObjectConverter(param, rawData);

            if (string.IsNullOrEmpty(param.OutputPath))
            {
                param.OutputPath = Path.GetDirectoryName(param.InputFileName);
            }

            CSVFile.WriteAllLines<Category>(param.OutputPath, null, convertData.Category, false);
            CSVFile.WriteAllLines<Genre>(param.OutputPath, null, convertData.Genre, false);
            CSVFile.WriteAllLines<Question>(param.OutputPath, null, convertData.Question, false);
            CSVFile.WriteAllLines<Choice>(param.OutputPath, null, convertData.Choice, false);
        }

        private RawData ByteParser(ConvertParameter param)
        {
            var logic = new ByteParser();
            var rawData = logic.Execute(param.InputFileName);
            return rawData;
        }

        private ConvertData ObjectConverter(ConvertParameter param, RawData rawData)
        {
            var logic = new ObjectConverter();
            var convertData = logic.Execute(param, rawData);
            return convertData;
        }
    }
}
