using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BlackJack.Enums;

namespace BlackJack
{
    class Logic
    {
        const int _quantityCardsInDeck = 52;
        const int _oneTypeCardQuantity = 4;
        Deck _deck;
        User _human;
        User _pc;


        public Logic()
        {
            Communication.Welcome();
            StartTheGame();            
        }

        public void CreatePlayers()
        {
            _human = new User
            {
                IsHuman = true,
                Wins = 0,
                Money = MoneyLogic.MinMoney,
                Points = 0,                
                Hand = new List<Card>()
            };

            _pc = new User
            {
                IsHuman = false,
                Wins = 0,
                Money = 0,
                Points = 0,
                Opponent = _human,
                Hand = new List<Card>()
            };
            _human.Opponent = _pc;

        }

        public void CreateDeck()
        {
            _deck = new Deck();
            _deck.Cards = new List<Card>();

            for (int i = 2; i < _quantityCardsInDeck/ _oneTypeCardQuantity + 2 ; i++)
            {
                for (int j = 0; j < _oneTypeCardQuantity; j++)
                {
                    _deck.Cards.Add(new Card { Name = ((NameOfCards)i).ToString(), Cost = i < 12 ? i : 10 });
                }
            }
        }

        public void ShuffleDeck()
        {
            _deck.Cards.Shuffle();
            _deck.Cards.Shuffle();
        }

        public void CardToHand(User user)
        {
            user.Hand.Add(_deck.Cards[0]);
            _deck.Cards.Add(_deck.Cards[0]);
            _deck.Cards.RemoveAt(0);
            CalculatePoints(user);
        }

        public void ClearHand(User user)
        {
            user.Hand.Clear();
            user.Points = 0;
        }

        public int CalculatePoints(User user)
        {
            return user.Points = user.Hand.Sum(x => x.Cost);
        }

        public bool IsRoundOwered(User user) //true when sb wins
        {
            if (IsExactly(user))
            {
                PointsToWinner(user);
                Communication.ExactlyMassege();
                return true;
            }
            else if (IsExcess(user))
            {
                PointsToWinner(user.Opponent);
                Communication.ExcessMassege();
                return true;
            }
            else
            {
                return false;
            }                
        }

        public bool IsExactly(User user)
        {
            return (user.Points == 21) ? true : false;
        }
        public bool IsExcess(User user)
        {
            return (user.Points > 21) ? true : false;
        }
        public void WinnerByPoints()
        {
            PointsToWinner((_human.Points > _pc.Points) ? _human : _pc);
        }

        public void PointsToWinner (User userWinner)
        {
            userWinner.Wins++;
            MoneyLogic.CalculateMoney(userWinner);
        }

        //_____________________________________________________


        public bool MainPcEvent()       //pc wins or lose - true
        {
            while (_pc.Points < 17)
            {
                CardToHand(_pc);
                Communication.ShowPoints(_pc);
                if (IsRoundOwered(_pc))
                {
                    return true;
                }    
            }
            return false;
        }
        public bool MainHumanEvent()    //human wins or lose - true
        {            
            do
            {                
                CardToHand(_human);
                Communication.ShowPoints(_human);
                if (IsRoundOwered(_human))
                {
                    return true;
                }
                               
            } while (Communication.MoreCard());     //more?        
            return false;
        }
        public void StartTheGame()
        {
            CreatePlayers();
            CreateDeck();
            ShuffleDeck();
            Communication.LineToSeparate();
            Communication.MoneyYouHave(_human);
            MoneyLogic.CheckGoal(_human);

            OneRound();           
        }
        public void FinishGame()
        {
            Communication.Bye();
            Environment.Exit(0);
        }

        public void OneRound()
        {
            MoneyLogic.CheckBet(_human);

            CardToHand(_human);
            if (!MainHumanEvent())
            {
                if (!MainPcEvent())
                {
                    WinnerByPoints();
                }
            }
            GameMoneyOvered(_human);
        }

        public void MoreRound()
        {
            Communication.ShowScore(_human);
            Communication.MoneyYouHave(_human);
            if (Communication.MoreRound())
            {
                Communication.LineToSeparate();
                ClearHand(_human);
                ClearHand(_pc);

                OneRound();
            }
            else
            {
                FinishGame();
            }            
        }
        public void MoreGame()
        {
            Communication.ShowScore(_human);
            if (Communication.MoreGame())
            {
                StartTheGame();
            }
            else
            {
                FinishGame();
            }
        }

        public void GameMoneyOvered(User _human)
        {
            if (!MoneyLogic.IsMoneyOnBorder(_human))
            {
                MoreRound();
            }
            else
            {
                MoreGame();
            }
        }
    }
}
