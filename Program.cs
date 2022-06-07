using System;
namespace DTP_Assessment_2022
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            ContinuousQuestion testQ = new ContinuousQuestion("What integer is pi nearest to?", 3);
            Console.WriteLine(testQ.getQuestion());
            string answer = Console.ReadLine();
            Console.WriteLine(testQ.getMultiplier(answer));
        }
    }
}