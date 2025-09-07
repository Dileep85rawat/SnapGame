namespace CardGame
{
    #region Global Enums
    public enum Suit { Hearts, Diamonds, Clubs, Spades }
    public enum MatchingConditions { FaceValue = 1, Suit, Both }
    #endregion

    public class Card
    {
        public string FaceValue { get; set; }
        public Suit Suit { get; set; }
    }
}
