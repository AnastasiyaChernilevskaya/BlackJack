using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class GameLogic
    {
        
        User _human;
        User _pc;
        Preparation _preparations = new Preparation();
        
        public GameLogic()
        {
            Communication.Welcome();
            StartTheGame();
        }
        public void CreatePlayers() 
        {
            _human = new User();
            _pc = new User();

            _human.IsHuman = true;
            _pc.IsHuman = false;

            _human.Opponent = _pc;
            _pc.Opponent = _human;
        }

        public void RefreshUsers() 
        {
            Calculations.ClearHand(_human);
            Calculations.ClearHand(_pc);
        }

        public void StartTheGame() 
        {
            CreatePlayers();
            _preparations.StartCreates();
            Communication.LineToSeparate();
            Communication.MoneyYouHave(_human);
            MoneyLogic.CheckGoal(_human);

            OneRound();
        }


        public void OneRound() 
        {
            Communication.LineToSeparate();
            MoneyLogic.CheckBet(_human);

            _preparations.CardToHand(_human);
            //Communication.ShowPoints(_human);
            if (!_preparations.MainEvent(_human))
            {
                if (!_preparations.MainEvent(_pc)) //
                {
                    WinnerByPoints();
                }
            }
            MoneyOvered();
        }

        public void MoreRound()
        {
            Communication.ShowScore(_human);
            Communication.MoneyYouHave(_human);
            if (Communication.MoreRound())
            {                
                RefreshUsers();

                OneRound();
                return;
            }
            Calculations.FinishGame();
        }

        public void MoreGame() 
        {
            Communication.ShowScore(_human);
            if (Communication.MoreGame())
            {
                StartTheGame();
                return;
            }
            Calculations.FinishGame();
        }
        public void MoneyOvered()
        {
            if (Calculations.IsGameMoneyOvered(_human))
            {
                MoreRound();
                return;
            }
            MoreGame();
        }

        public void WinnerByPoints()
        {
            User userWinner = (_human.Points > _pc.Points) ? _human : _pc;
            Calculations.PointsToWinner(userWinner);
        }

    }
}
