using System;
namespace DTP_Assessment_2022
{
    internal static class Program
    {
        static Dino enemy;
        static Dino[] playerDinos = new Dino[6];
        static int selectedDino = 0;
        static bool typeWriteOn = true;
        static void Main(string[] args)
        {
            MainMenu();
        }
        static void MainMenu()
        {
            Console.Clear();
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
                    Console.Clear();
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
            while (Console.KeyAvailable) { Console.ReadKey(false); };
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
            if(choice=="y")
            {
                Console.Clear();
                TypeWrite("Ok. Lets begin!");
            }
            else
            {
                TypeWrite("Well that was enexpected. Back to the menu for you!");
                Sleep(2500);
                MainMenu();
            }
            while (true)
            {
                
            }
        }
    }
}