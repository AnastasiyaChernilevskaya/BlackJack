using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public class Preparation
    {
        const int _quantityCardsInDeck = 52;
        const int _oneTypeCardQuantity = 4;
        const int _allTypesCards = 13;

        Deck _deck;


        public void CreateDeck()
        {
            _deck = new Deck();
            _deck.Cards = new List<Card>();

            for (int i = 2; i < _allTypesCards + 2; i++)
            {
                for (int j = 0; j < _oneTypeCardQuantity; j++)
                {
                    Card card = new Card();
                    card.Name = ((NameOfCards)i).ToString();
                    card.Cost = i < 12 ? i : 10;

                    _deck.Cards.Add(card);
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
            Card firstCard = _deck.Cards[0];

            user.Hand.Add(firstCard);
            _deck.Cards.Add(firstCard);
            _deck.Cards.RemoveAt(0);
            Calculations.CalculatePoints(user);
        }



        public void StartCreates()
        {
            CreateDeck();
            ShuffleDeck();
        }
        
        public bool TypeOfGettings(User user)
        {
            if (user.IsHuman)
            {
                return Communication.MoreCard();
            }
            return Calculations.CheckPcPonits(user);
        }

        public bool MainEvent(User user)            //user wins or lose - true
        {
            do
            {
                CardToHand(user);
                Communication.ShowPoints(user);
                if (Calculations.IsRoundOwered(user))
                {
                    return true;
                }

            } while (TypeOfGettings(user));     //more?        
            return false;
        }


    }

}
