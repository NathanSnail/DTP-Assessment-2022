using System;
namespace DTP_Assessment_2022
{
    internal static class Program
    {

        static void Main(string[] args)
        {
            // ContinuousQuestion testQ = new ContinuousQuestion("What integer is pi nearest to?", 3);
            // Console.WriteLine(testQ.getQuestion());
            // string answer = Console.ReadLine();
            // Console.WriteLine(testQ.getMultiplier(answer));
            Attack test = new Attack(0, 0, 0, 0, 1, 0, 10, "test");
            Dino creature = new Dino(5, 5, 5, new Attack[4] {test,test,test,test}, "creature");
            Dino enemy = creature.MakeClone();
            // Console.WriteLine(enemy.getDamage(-5));
            enemy.takeDamage(50);
            enemy.attacks[0] = new Attack(1,1,1,1,1,712,111,"faker");
            Console.WriteLine(enemy.health);
            Console.WriteLine(creature.health);
            Console.WriteLine(enemy.attacks[0].maxUses);
            Console.WriteLine(enemy.attacks[1].maxUses);
            Console.WriteLine(creature.attacks[0].maxUses);
            Console.WriteLine(creature.attacks[1].maxUses);
        }
    }
}