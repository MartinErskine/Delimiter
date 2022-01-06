using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;
using System.Linq;

namespace Delimiter
{
    public class DelimiterService : IDelimiterService
    {
        public int Add(string[] args)
        {
            if (args.Length < 1)
            {
                return 0;
            }

            return args.Length > 0 ? ParseDelimiter(args) : 0;
        }

        private int ParseDelimiter(string[] args)
        {
            var delimiter = ",";
            var delimiters = new List<string>();

            if (args[0].StartsWith("//"))
            {
                var delimiterProvided = args[0].Split(@"\n");

                if (delimiterProvided[0].Contains("[") && delimiterProvided[0].Contains("]"))
                {
                    const string pattern = @"\[(.*?)\]";

                    delimiters = Regex.Matches(delimiterProvided[0], pattern)
                        .Select(m => m.Groups[1].Value)
                        .ToList();
                }
                else
                {
                    delimiters.Add(delimiterProvided[0].Substring(2));
                }

                var body = delimiterProvided[1];

                return Parse(body, delimiters);
            }

            delimiters.Add(delimiter);

            return Parse(args[0], delimiters);
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
            var numbers = str.Split(delimiters, StringSplitOptions.None);

            foreach (var number in numbers)
            {
                var ignoreMe = 0;
                var isSuccessful = int.TryParse(number, out ignoreMe);
                var numInt = int.Parse(number);

                switch (numInt)
                {
                    case < 0:
                        errors = true;
                        errorMessage += $"{numInt}\n";
                        break;
                    case > 1000:
                        numInt = 0;
                        break;
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
