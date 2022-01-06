using Fclp;
using System;
using System.Linq;

namespace Delimiter
{
    class Program
    {
        static void Main(string[] args)
        {
            //var p = new FluentCommandLineParser<ApplicationArguments>();

            Console.WriteLine(Add(args));

            Console.ReadLine();
        }

        public static int Add(string[] args)
        {
            if (args.Length < 1)
            {
                return 0;
            }

            if (args.Length > 0)
            {
                var delimiter = ",";

                if (args[0].StartsWith("//"))
                {
                    var delimiterProvided = args[0].Split(@"\n");

                    delimiter = delimiterProvided[0].Substring(2);
                    var body = delimiterProvided[1];

                    return Parse(body, delimiter);
                }

                return Parse(args[0], delimiter);

            }

            return 0;
        }

        private static int Parse(string str, string delimiter)
        {
            if (str.Contains("\n"))
            {
                str = str.Replace(@"\n", delimiter);
            }


            var nums = str.Split(delimiter);


            var total = 0;

            foreach (var num in nums)
            {
                var ignoreMe = 0;

                var isSuccessful = int.TryParse(num, out ignoreMe);

                if (!isSuccessful)
                {
                    return 0;
                }

                total += int.Parse(num);
            }

            return total;



            return 0;
        }
    }
}
