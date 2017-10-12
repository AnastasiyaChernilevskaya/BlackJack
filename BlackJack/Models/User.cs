using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public class User
    {
        public bool IsHuman { get; set; }
        public List<Card> Hand { get; set; }
        public int Points { get; set; }
        public int Wins { get; set; }
        public User Opponent { get; set; }

        public int Money { get; set; }
        public int Bet { get; set; }
        public int Goal { get; set; }

        public User()
        {
            Hand = new List<Card>();
            Points = 0;
            Wins = 0;

            Money = MoneyLogic.MinMoney;
            Bet = 0;
            Goal = MoneyLogic.MaxMoney;
        }       

    }
}
