using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    static class Calculations
    {        


        public static void ClearHand(User user)
        {
            user.Hand.Clear();
            user.Points = 0;
            user.Bet = 0;
        }

        public static int CalculatePoints(User user)
        {
            return user.Points = user.Hand.Sum(x => x.Cost);
        }

        public static bool IsGameMoneyOvered(User user)
        {
            if (!MoneyLogic.IsMoneyOnBorder(user))
            {
                return true;
            }
            return false;
        }

        public static bool IsRoundOwered(User user) //true when sb wins
        {
            if (IsExactly(user))
            {
                PointsToWinner(user);
                Communication.ExactlyMassege();
                return true;
            }
            if (IsExcess(user))
            {
                PointsToWinner(user.Opponent);
                Communication.ExcessMassege();
                return true;
            }
            return false;
        }

        public static bool IsExactly(User user)
        {
            return (user.Points == 21) ? true : false;
        }
        public static bool IsExcess(User user)
        {
            return (user.Points > 21) ? true : false;
        }

        public static void PointsToWinner(User userWinner)
        {
            userWinner.Wins++;
            MoneyLogic.CalculateMoney(userWinner);
        }

        public static bool CheckPcPonits(User user)
        {
            while (user.Points < 17)
            {
                return true;
            }
            return false;
        }
        public static void FinishGame()
        {
            Communication.Bye();
            Environment.Exit(0);
        }

    }
}
