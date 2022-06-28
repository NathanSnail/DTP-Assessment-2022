using System;
using System.Text.Json;
using System.Text.Json.Nodes;
namespace DTP_Assessment_2022
{
    internal static class Program
    {
        static int befriends = 7;
        static float minMultiplier = 0.5f;
        static float maxMultiplier = 1.5f;
        static Dino[] playerDinos = new Dino[6];
        static int selectedDino = 0;
        static bool typeWriteOn = true;
        static bool debugNoSleep = false;
        static List<Dino> dinos = new List<Dino>();
        static List<Attack> attacks = new List<Attack>();
        static Random random = new Random();
        static List<ContinuousQuestion> questions = new List<ContinuousQuestion>();
        static (float, string) askQuestion(ContinuousQuestion q)
        {
            try
            {
                TypeWrite(q.getQuestion() + "\n");
                string choice = Console.ReadLine();
                return (q.getMultiplier(choice));
            }
            catch
            {
                return askQuestion(q);
            }
        }
        static void Main(string[] args)
        {
            genData();
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
        static void genData()
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
            JsonNode questionJson = data["questions"];
            for (int i = 0; i < questionJson.AsArray().Count; i++)
            {
                string question = questionJson.AsArray()[i]["question"].GetValue<string>();
                float answer = questionJson.AsArray()[i]["answer"].GetValue<float>();
                questions.Add(new ContinuousQuestion(question, answer));
            }
        }
        static void StackOverflowTest(Dino d, int i)
        {
            /*Test to see if stack overflow is an issue due to functions calling themselves in case
            of bad input. Crashes after ~20,000 calls which given the 50 battles would require the user
            to give a bad input 400 times per level which is not a realistic concern. This would take
            the user about 3 hours of nonstop bad inputs. Realisticly only 100 or so will occur overall,
            and even if the user were to try and intentionally break the program they would struggle.*/
            try
            {

                Console.WriteLine(i);
                StackOverflowTest(d, i + 1);
            }
            catch
            {
                Sleep(10000);
            }
        }
        static void MainMenu()
        {
            Console.Clear();
            string title = System.IO.File.ReadAllText("title.txt");
            Console.WriteLine(title);
            Console.WriteLine(
@$"
Press 1 for the main game
press 2 to toggle typewriter mode (Currently {(typeWriteOn ? "On" : "Off")})
press 3 to toggle noSleep mode (Currently {(debugNoSleep ? "On" : "Off")})");
            ConsoleKeyInfo choice = Console.ReadKey();
            switch (choice.KeyChar.ToString())
            {
                case "1":
                    Console.Clear();
                    MainGame();
                    break;
                case "2":
                    typeWriteOn = !typeWriteOn;
                    MainMenu();
                    break;
                case "3":
                    debugNoSleep = !debugNoSleep;
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
            if (!debugNoSleep)
            {
                System.Threading.Thread.Sleep(milis);
                ClearKeyBuffer();
            }
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
                Console.Clear();
                TypeWrite("Ok. Lets begin!");
                Sleep(1000);
                Console.Clear();
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
            Dino curEnemy = dinos[random.Next(0, dinos.Count)].MakeClone(); //FIXME: might return Empty
            Dino PlayerDino = playerDinos[selectedDino];
            TypeWrite($"Battle between your {PlayerDino.name} and enemy {curEnemy.name}\n");
            Sleep(250);
            while (PlayerDino.health > 0 && curEnemy.health > 0)
            {
                DoRound(PlayerDino, curEnemy);
            }
            if (PlayerDino.health <= 0)
            {
                SelectDino();
            }
        }
        static void DoRound(Dino pDino, Dino enemy)
        {
            TypeWrite(@$"
Friendly {pDino.name} health is {Math.Round(pDino.health)}
Enemy {enemy.name} health is {Math.Round(enemy.health)}
");
            TypeWrite(@$"
Select Move
    1: {getAttackString(pDino.attacks[0]).Item1}
    2: {getAttackString(pDino.attacks[1]).Item1}
    3: {getAttackString(pDino.attacks[2]).Item1}
    4: {getAttackString(pDino.attacks[3]).Item1}
    5: Swap Dinos
    6: Befriend Enemy ({befriends} remaining)
");
            string option = Console.ReadKey().KeyChar.ToString();
            try
            {
                int choice = int.Parse(option) - 1;
                if (choice == 4) //offset by 1
                {
                    SelectDino();
                }
                else if (choice == 5) //offset by 1
                {
                    if (befriends > 0)
                    {
                        Console.Clear();
                        for (int i = 0; i < playerDinos.Length; i++)
                        {
                            Dino d = playerDinos[i];
                            if (d.health > 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Gray;
                            }
                            TypeWrite($"{i + 1}: " + d.name + "\n");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                    Select:
                        TypeWrite("What number dino do you want to swap it with??\n");
                        try
                        {
                            int dchoice = int.Parse(Console.ReadKey().KeyChar.ToString());
                            playerDinos[dchoice - 1] = enemy.MakeClone();
                            enemy.health = 0;
                            befriends -= 1;
                        }
                        catch
                        {
                            TypeWrite("Invalid choice");
                            Sleep(500);
                            goto Select; //goto is the best choice here
                        }
                    }
                    else
                    {
                        TypeWrite("No befriends left");
                        Sleep(500);
                        Console.Clear();
                    }
                }
                else if (pDino.attacks[choice].uses > 0)
                {
                    ContinuousQuestion question = questions[random.Next(0, questions.Count)];
                askQuestion:
                    TypeWrite(question.getQuestion() + "\n");
                    string ans = Console.ReadLine();
                    float multiplier;
                    try
                    {
                        (float,string) questionResult = question.getMultiplier(ans);
                        TypeWrite(questionResult.Item2 + "\n" + questionResult.Item1);
                        multiplier = questionResult.Item1;
                        Sleep(500);
                    }
                    catch
                    {
                        goto askQuestion; //best solution to this problem unfortunately
                    }
                    pDino.attacks[choice].useAttack(multiplier, pDino, enemy);
                    List<int> validAttacks = new List<int>();
                    for (int i = 0; i < 4; i++) { if (enemy.attacks[i].uses > 0) { validAttacks.Add(i); } }
                    int enemyAttackID = random.Next(0, validAttacks.Count);
                    enemy.attacks[enemyAttackID].useAttack(random.NextSingle() * (maxMultiplier - minMultiplier) + minMultiplier, enemy, pDino); //for some reason c#'s random doesnt like floats
                }
                else
                {
                    TypeWrite("That attack has no more uses");
                }
            }
            catch
            {
                TypeWrite("Invalid choice");
                Sleep(500);
                DoRound(pDino, enemy);
            }
        }
        static void SelectDino()
        {
            bool lose = false;
            foreach (Dino d in playerDinos)
            {
                if (d.name != "Empty")
                {
                    Console.WriteLine(d.name);
                    lose = lose || d.health <= 0;
                }
            }
            if (lose)
            {
                Environment.Exit(0);
            }
            Console.Clear();
            for (int i = 0; i < playerDinos.Length; i++)
            {
                Dino d = playerDinos[i];
                if (d.health > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                TypeWrite($"{i + 1}: " + d.name + "\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
            TypeWrite("What number dino?\n");
            try
            {
                int choice = int.Parse(Console.ReadKey().KeyChar.ToString());
                if (playerDinos[choice - 1].health > 0)
                {
                    selectedDino = choice - 1;
                    Console.Clear();
                }
                else
                {
                    TypeWrite("Choice is dazed");
                    Sleep(500);
                    SelectDino();
                }
            }
            catch
            {
                TypeWrite("Invalid choice");
                Sleep(500);
                SelectDino();
            }
        }
        static (string, bool) getAttackString(Attack a)
        {
            return (($"{a.name} ({a.uses}/{a.maxUses})", a.uses == 0)); //currently unused data for not able to do uses
        }
    }
}