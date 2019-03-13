using Conv5tq.Logic;
using Conv5tq.Models;

namespace Conv5tq
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var param = new ConvertParameter
            {
                InputFileName = args[0]
            };
            Convert(param);
        }

        private static void Convert(ConvertParameter param)
        {
            var logic = new ConvertLogic();
            logic.Execute(param);
        }
    }
}
