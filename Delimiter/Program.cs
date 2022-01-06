using Fclp;
using System;

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
                var replaceNewline = args[0].Replace(@"\n", ",");


                var nums = replaceNewline.Split(',');


                var total = 0;

                foreach (var num in nums)
                {
                    var ignoreMe = 0;

                    var isSuccessful =  int.TryParse(num, out ignoreMe);

                    if (!isSuccessful)
                    {
                        return 0;
                    }

                    total += int.Parse(num);
                }

                return total;
            }

            return 0;
        }
    }
}
