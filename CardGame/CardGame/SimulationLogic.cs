using System;
using System.Collections.Generic;

namespace CardGame
{
    /// <summary>
    /// Class to handle main logic for Game simulation.
    /// </summary>
    public class SimulationLogic
    {
        private string[] FaceValues ={"Q","K","J","A","2", "3", "4", "5", "6", "7", "8", "9", "10"};
        private Random random = new Random();

        /// <summary>
        /// Method to create pile of cards, based on provided number of packs.
        /// </summary>
        /// <param name="packsNumber">Number of Packs provided by user.</param>
        /// <returns></returns>
        public List<Card> CreateCardsPile(int packsNumber)
        {
            var cardsPile = new List<Card>();
            for (int i = 0; i < packsNumber; i++)
            {
                foreach (var face in FaceValues)
                {
                    foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                    {
                        cardsPile.Add(new Card { FaceValue = face, Suit = suit });
                    }
                }
            }
            return cardsPile;
        }

        /// <summary>
        /// Method to shuffle cards.
        /// </summary>
        /// <param name="cardPile">Collection of Cards created by number of Packs.</param>
        public void ShufflingCards(List<Card> cardPile)
        {
            for (int i = cardPile.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                (cardPile[i], cardPile[j]) = (cardPile[j], cardPile[i]);// swaping through tuple.
            }
        }

        /// <summary>
        /// Method to match the cards according to selected matching and provided
        /// Packs numbers by user.
        /// </summary>
        /// <param name="piledCards">Shuffled pile of cards.</param>
        /// <param name="matchCondition">Match condition selected by user.</param>
        /// <returns></returns>
        public (int scoreForFirstPlayer, int scoreForSecondPlayer, List<string> snapMessages) ExecutionOfGame(List<Card> piledCards, MatchingConditions matchCondition)
        {
            Card previousCard = null;
            List<Card> pile = new List<Card>();
            List<string> snapMessages = new List<string>();

            int scoreForFirstPlayer = 0;
            int scoreForSecondPlayer = 0;

            for (int i = 0; i < piledCards.Count; i++)
            {
                var currentCard = piledCards[i];

                pile.Add(currentCard);

                if (previousCard != null && this.IsConditionMatched(previousCard, currentCard, matchCondition))
                {
                    int winnerPlayer = random.Next(1, 3); // 1 or 2
                    snapMessages.Add($"Snap! Player {winnerPlayer} wins this round with {pile.Count} cards.");

                    if (winnerPlayer == 1)
                        scoreForFirstPlayer += pile.Count;
                    else
                        scoreForSecondPlayer += pile.Count;

                    pile.Clear();
                }

                previousCard = currentCard;
            }

            return (scoreForFirstPlayer, scoreForSecondPlayer, snapMessages);
        }

        /// <summary>
        /// Method to match cards with matching condition.
        /// </summary>
        /// <param name="previousCard">previous card</param>
        /// <param name="currentCard">corrent card</param>
        /// <param name="matchingConditon">selected matching condition.</param>
        /// <returns></returns>
        private bool IsConditionMatched(Card previousCard, Card currentCard, MatchingConditions matchingConditon)
        {
            switch (matchingConditon)
            {
                case MatchingConditions.FaceValue:
                    return previousCard.FaceValue == currentCard.FaceValue;
                case MatchingConditions.Suit:
                    return previousCard.Suit == currentCard.Suit;
                case MatchingConditions.Both:
                    return previousCard.FaceValue == currentCard.FaceValue && previousCard.Suit == currentCard.Suit;

                default:
                    return false;
            }
        }

    }
}
