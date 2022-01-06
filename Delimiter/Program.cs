using System;

namespace Delimiter
{
    class Program
    {
        private static readonly DelimiterService _delimiterService = new DelimiterService();

        static void Main(string[] args)
        {
            Console.WriteLine(_delimiterService.Add(args));
            Console.ReadLine();
        }
    }
}
