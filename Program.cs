using System;
using System.Text.Json;
using System.Text.Json.Nodes;
namespace DTP_Assessment_2022
{
    internal static class Program
    {
        static Dino[] playerDinos = new Dino[6];
        static int selectedDino = 0;
        static bool typeWriteOn = true;
        static List<Dino> dinos = new List<Dino>();
        static List<Attack> attacks = new List<Attack>();
        static Random random = new Random();
        static void Main(string[] args)
        {
            genDinos();
            Console.BackgroundColor = ConsoleColor.Green;
            Console.WriteLine(dinos[0].name + " " + dinos[0].attacks[1].name);
            Console.BackgroundColor = ConsoleColor.Black;
            Dino empty = getDino("Empty");
            playerDinos[0] = getDino("Basic");
            for (int i = 1; i < 6; i++)
            {
                playerDinos[i] = empty.MakeClone();
            }
            MainMenu();
        }
        static Dino getDino(string name)
        {
            for (int i = 0; i < dinos.Count; i++)
            {
                if (dinos[i].name == name)
                {
                    Dino d = dinos[i].MakeClone();
                    int attackID = random.Next(0, attacks.Count);
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine(name + " " + attackID);
                    Console.BackgroundColor = ConsoleColor.Black;
                    d.attacks[3] = attacks[attackID];
                    return (d);
                }
            }
            return null;
        }
        static Attack GetAttack(string name)
        {
            for (int i = 0; i < attacks.Count; i++)
            {
                if (attacks[i].name == name)
                {
                    return (attacks[i]);
                }
            }
            return null;
        }
        static void genDinos()
        {
            JsonNode data = JsonNode.Parse(System.IO.File.ReadAllText("data.json"));
            JsonNode attacksJson = data["attacks"];
            for (int i = 0; i < attacksJson.AsArray().Count; i++)
            {
                string name = attacksJson.AsArray()[i]["name"].GetValue<string>();
                int selfDefense = attacksJson.AsArray()[i]["selfDefense"].GetValue<int>();
                int selfAttack = attacksJson.AsArray()[i]["selfAttack"].GetValue<int>();
                int enemyDefense = attacksJson.AsArray()[i]["enemyDefense"].GetValue<int>();
                int enemyAttack = attacksJson.AsArray()[i]["enemyAttack"].GetValue<int>();
                int maxUses = attacksJson.AsArray()[i]["maxUses"].GetValue<int>();
                attacks.Add(new Attack(selfDefense, selfAttack, enemyDefense, enemyAttack, maxUses, name));
            }
            JsonNode dinosJson = data["dinos"];
            for (int i = 0; i < dinosJson.AsArray().Count; i++)
            {
                string name = dinosJson.AsArray()[i]["name"].GetValue<string>();
                int hp = dinosJson.AsArray()[i]["hp"].GetValue<int>();
                int attack = dinosJson.AsArray()[i]["attack"].GetValue<int>();
                int defense = dinosJson.AsArray()[i]["defense"].GetValue<int>();
                List<Attack> dinoAttacks = new List<Attack>();
                JsonArray attacksArray = dinosJson.AsArray()[i]["attacks"].AsArray();
                for (int j = 0; j < 3; j++)
                {
                    dinoAttacks.Add(GetAttack(attacksArray[j].ToString()));
                }
                dinoAttacks.Add(GetAttack("Empty"));
                dinos.Add(new Dino(hp, attack, defense, dinoAttacks.ToArray(), name).MakeClone());
            }
        }
        static void MainMenu()
        {
            //Console.Clear();
            string title = System.IO.File.ReadAllText("title.txt");
            Console.WriteLine(title);
            Console.WriteLine(
@$"
Press 1 for the main game
press 2 to toggle typewriter mode (Currently {(typeWriteOn ? "On" : "Off")})");
            ConsoleKeyInfo choice = Console.ReadKey();
            switch (choice.KeyChar.ToString())
            {
                case "1":
                    //Console.Clear();
                    MainGame();
                    break;
                case "2":
                    typeWriteOn = !typeWriteOn;
                    MainMenu();
                    break;
                default:
                    MainMenu();
                    break;
            }
        }
        static void ClearKeyBuffer()
        {
            while (Console.KeyAvailable) { Console.ReadKey(true); };
        }


        static void Sleep(int milis)
        {
            System.Threading.Thread.Sleep(milis);
            ClearKeyBuffer();
        }
        static void TypeWrite(string message, int timePerKey = 10)
        {
            if (typeWriteOn)
            {
                foreach (char c in message)
                {
                    Console.Write(c);
                    Sleep(timePerKey);
                }
            }
            else
            {
                Console.Write(message);
            }
        }
        static void MainGame()
        {
            TypeWrite(
@"Welcome to the world of enviro dinos, you can have six dinos in your collection at once, and one currently active.
The main combat of enviro dinos involves selecting a move, and then performing it, each move has unique effects.
The effects of a move will scale up with how accurately you answer the question. You will also be given some information on if your answer was correct or not.");
            Sleep(2500);
            TypeWrite("\nWould you like to begin? Y / N");
            string choice = Console.ReadKey().KeyChar.ToString().ToLower();
            if (choice == "y")
            {
                //Console.Clear();
                TypeWrite("Ok. Lets begin!");
                Sleep(1000);
                //Console.Clear();
            }
            else
            {
                TypeWrite("Well that was enexpected. Back to the menu for you!");
                Sleep(2500);
                MainMenu();
            }
            while (true)
            {
                Battle();
            }
        }
        static void Battle()
        {

            foreach (Attack a in attacks)
            {
                Console.WriteLine(a.name);
            }
            Dino curEnemy = dinos[random.Next(0, dinos.Count)].MakeClone(); //FIXME: might return Empty
            Dino PlayerDino = playerDinos[selectedDino];
            TypeWrite($"Battle between your {PlayerDino.name} and enemy {curEnemy.name}");
            Sleep(250);
            while (PlayerDino.health > 0 && curEnemy.health > 0)
            {
                TypeWrite($"Friendly {PlayerDino.name} health is {PlayerDino.health}\nEnemy {curEnemy.name} health is {curEnemy.health}\n");
                TypeWrite(
@$"Select attack
    1: {PlayerDino.attacks[0].name}
    2: {PlayerDino.attacks[1].name}
    3: {PlayerDino.attacks[2].name}
    4: {PlayerDino.attacks[3].name}
");
                string option = Console.ReadKey().KeyChar.ToString();
                switch (option)
                {
                    case "1":
                        break;
                }
            }
        }
    }
}