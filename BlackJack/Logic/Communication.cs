using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    static class Communication
    {
        public static void Welcome()
        {
            Console.WriteLine("Welcome to the game!");
        }
        public static void Bye()
        {
            Console.WriteLine("Ok. See you");
            Console.ReadKey();
        }

        public static void LineToSeparate()
        {
            Console.WriteLine("______________________________________________________________");
        }

        public static bool MoreCard()
        {
            Console.WriteLine("1 more card? Y/N");
            return Console.ReadLine().ToLower() == "y" ? true : false;
        }
        public static bool MoreRound()
        {
            Console.WriteLine("Would u like 1 more round? Y/N");
            return Console.ReadLine().ToLower() == "y" ? true : false;
        }
        public static bool MoreGame()
        {
            Console.WriteLine("Would you like to play from the begining?");
            return Console.ReadLine().ToLower() == "y" ? true : false;
        }

        private static string PrintNames(User user)
        {
            StringBuilder s = new StringBuilder();
            foreach (var item in user.Hand)
            {
                s.Append(item.Name + ", ");
            }
            return s.Remove(s.Length - 2, 2).ToString();
        }

        public static void ShowPoints(User user)
        {
            Console.WriteLine($"{(user.IsHuman ? "You" : "Computer")} have {(PrintNames(user))} cards."
                            + $"It is {user.Points} points");
        }
        public static void ShowScore(User user)
        {
            Console.WriteLine($"\n\tYou have {user.Wins} wins and {user.Opponent.Wins} loses \n");
        }

        public static void ExcessMassege()
        {
            Console.WriteLine("\nIt is too match points :( ");
        }
        public static void ExactlyMassege()
        {
            Console.WriteLine("\nIt is exactly 21 points! :D ");
        }
        //___________________________________________________________

        public static void MoneyYouHave(User user)
        {
            Console.WriteLine($"Now you have {user.Money} money");
        }

        public static int SetBet()
        {
            int result;
            Console.WriteLine("How much money do you whant to bet? It should be not more than you have!");
            int.TryParse(Console.ReadLine(), out result);
            return result;
        }

        public static int SetGoal()
        {
            int result;
            Console.WriteLine("How much money do you whant as your goal?");
            int.TryParse(Console.ReadLine(), out result);
            return result;
        }
        public static void GoalIs(User user)
        {
            Console.WriteLine($"Your goal is {user.Goal}");
        }

        public static void Congretulations(User user)
        {
            Console.WriteLine($"You have achieve your goal! You have >= {user.Goal} money");
        }
        public static void Sorre()
        {
            Console.WriteLine("You have no money :=O");
        }        
    }
}