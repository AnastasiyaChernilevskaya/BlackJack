using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    static class MoneyLogic
    {
        const int _maxMoney = 20000;
        const int _minMoney = 100;

        public static int MinMoney
        {
            get
            {
                return _minMoney;
            }
        }

        public static int MaxMoney
        {
            get
            {
                return _maxMoney;
            }
        }

        public static void CheckBet(User user)
        {
            int bet;
            bool flag = true;
            do
            {
                bet = Communication.SetBet();
                if (bet > user.Money || bet <= 0)           //not correct bet
                {
                    flag = true;
                }
                else                                          //correct bet
                {
                    user.Bet = bet;
                    flag = false;
                }

            } while (flag);
        }

        public static void CheckGoal(User user)
        {
            var goal = Communication.SetGoal();
            if (goal <= MinMoney)
            {
                user.Goal = MaxMoney;
            }
            else
            {
                user.Goal = goal;
            }
            Communication.GoalIs(user);
        }

        public static void CalculateMoney(User userWinner) //adds money for human-user
        {
            if (userWinner.IsHuman)
            {
                userWinner.Money += userWinner.Bet;
                return;
            }
            userWinner.Opponent.Money -= userWinner.Opponent.Bet;
        }

        public static bool IsMoneyOnBorder(User user)
        {
            if (user.Money == 0)
            {
                Communication.Sorre();
                return true;
            }
            else if (user.Money >= user.Goal)
            {
                Communication.Congretulations(user);
                return true;
            }
            return false;
        }
    }
}
