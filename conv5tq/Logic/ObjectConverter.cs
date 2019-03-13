using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Conv5tq.Models;
using Conv5tq.Models.Raw;
using Conv5tq.Util;

namespace Conv5tq.Logic
{
    public class ObjectConverter
    {
        public ConvertData Execute(ConvertParameter param, RawData rawData)
        {
            var convertData = new ConvertData();
            convertData.Category = this.ConvertCategory(param.StartCategoryId, rawData);
            convertData.Genre = this.ConvertGenre(param.StartGenreId, rawData, convertData.Category);
            (convertData.Question, convertData.Choice) = this.ConvertQuestion(param.StartQuestionId, rawData, convertData.Genre);
            return convertData;
        }

        private Category ConvertCategory(int startId, RawData rawData)
        {
            var head = rawData.Head.First();
            var category = new Category
            {
                Id = startId,
                CategoryName = StringUtil.GetString(head.file, false)
            };
            return category;
        }

        private List<Genre> ConvertGenre(int startId, RawData rawData, Category category)
        {
            var genreList = new List<Genre>();
            foreach (var head in rawData.Head.Select((v, i) => new { value = v, id = i + startId }))
            {
                var genre = new Genre
                {
                    Id = head.id,
                    CategoryId = category.Id,
                    GenreName = StringUtil.GetString(head.value.genreName, false),
                    StartId = head.value.skip - 7,
                    EndId = head.value.skip - 8 + head.value.size
                };
                genreList.Add(genre);
            }
            return genreList;
        }

        private (List<Question>, List<Choice>) ConvertQuestion(int startId, RawData rawData, List<Genre> genre)
        {
            var questionList = new List<Question>();
            var choiceList = new List<Choice>();
            foreach (var data in rawData.Data.Select((v, i) => new { value = v, id = i + startId }))
            {
                var question = new Question
                {
                    Id = data.id,
                    GenreId = genre.Where(x => x.StartId <= data.id && x.EndId >= data.id).First().Id,
                    QuestionText = StringUtil.GetString(data.value.message, true)
                };
                questionList.Add(question);

                var values = new string[]
                {
                    StringUtil.GetString(data.value.choices1, true),
                    StringUtil.GetString(data.value.choices2, true),
                    StringUtil.GetString(data.value.choices3, true),
                    StringUtil.GetString(data.value.choices4, true),
                    StringUtil.GetString(data.value.choices5, true)
                };

                var choice = values.Select((v, i) => new Choice
                {
                    Id = (data.id - 1) * 5 + i + 1,
                    QuestionId = data.id,
                    IsCorrect = i == 0 ? 1 : 0,
                    ChoiceText = v
                });
                choiceList.AddRange(choice);
            }

            return (questionList, choiceList);
        }
    }
}
