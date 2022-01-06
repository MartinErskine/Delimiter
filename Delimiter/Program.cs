using Fclp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Fclp.Internals.Extensions;

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
                return ParseDelimiter(args);
            }

            return 0;
        }

        private static int ParseDelimiter(string[] args)
        {
            var delimiter = ",";
            var delimiters = new List<string>();

            if (args[0].StartsWith("//"))
            {
                var delimiterProvided = args[0].Split(@"\n");

                if (delimiterProvided[0].Contains("[") && delimiterProvided[0].Contains("]"))
                {
                    var pattern = @"\[(.*?)\]";

                    delimiter = Regex.Matches(delimiterProvided[0], pattern).Cast<Match>()
                        .Select(m => m.Groups[1].Value)
                        .FirstOrDefault();

                    delimiters = Regex.Matches(delimiterProvided[0], pattern).Cast<Match>()
                        .Select(m => m.Groups[1].Value)
                        .ToList();
                }
                else
                {
                    delimiter = delimiterProvided[0].Substring(2);
                }
                
                var body = delimiterProvided[1];

                return Parse(body, delimiters);
            }

            return Parse(args[0], delimiter);
        }

        private static int Parse(string str, List<string> delimiterList)
        {
            if (str.Contains(@"\n"))
            {
                str = str.Replace(@"\n", delimiterList.FirstOrDefault());
            }

            var errorMessage = "Negative numbers were provided:\n";
            var errors = false;
            var delimiters = delimiterList.ToArray();
            var total = 0;
            var nums = str.Split(delimiters, StringSplitOptions.None);

            foreach (var num in nums)
            {
                var ignoreMe = 0;
                var isSuccessful = int.TryParse(num, out ignoreMe);
                var numInt = int.Parse(num);

                if (numInt < 0)
                {
                    errors = true;
                    errorMessage += $"{numInt}\n";
                }

                if (numInt > 1000)
                {
                    numInt = 0;
                }

                if (!isSuccessful)
                {
                    return 0;
                }

                total += numInt;
            }

            if (!errors)
            {
                return total;
            }

            Console.WriteLine(errorMessage);
            total = 0;

            return total;
        }


        private static int Parse(string str, string delimiter)
        {
            if (str.Contains(@"\n"))
            {
                str = str.Replace(@"\n", delimiter);
            }

            var errorMessage = "Negative numbers were provided:\n";
            var errors = false;
            var nums = str.Split(delimiter);
            var total = 0;

            foreach (var num in nums)
            {
                var ignoreMe = 0;
                var isSuccessful = int.TryParse(num, out ignoreMe);
                var numInt = int.Parse(num);

                if (numInt < 0)
                {
                    errors = true;
                    errorMessage += $"{numInt}\n";
                }

                if (numInt > 1000)
                {
                    numInt = 0;
                }

                if (!isSuccessful)
                {
                    return 0;
                }

                total += numInt;
            }

            if (!errors)
            {
                return total;
            }

            Console.WriteLine(errorMessage);
            total = 0;

            return total;
        }
    }
}
