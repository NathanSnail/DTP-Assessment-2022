using System;
namespace DTP_Assessment_2022
{
    class Creature : Dino
    {
        public Creature() : base(5, 5, 5, new Attack[4] { new Attack(0, 0, 0, 0, 1, 0, 10, "test"), new Attack(0, 0, 0, 0, 1, 0, 10, "test"), new Attack(0, 0, 0, 0, 1, 0, 10, "test"), new Attack(0, 0, 0, 0, 1, 0, 10, "test") }, "creature")
        { }
    }
    internal static class Program
    {
        static void Main(string[] args)
        {
            ContinuousQuestion testQ = new ContinuousQuestion("What integer is pi nearest to?", 3);
            Console.WriteLine(testQ.getQuestion());
            string answer = Console.ReadLine();
            Console.WriteLine(testQ.getMultiplier(answer));
            Attack test = new Attack(0, 0, 0, 0, 1, 0, 10, "test");
            Creature enemy = new Creature();
            Console.WriteLine(enemy.getDamage(5));
        }
    }
}