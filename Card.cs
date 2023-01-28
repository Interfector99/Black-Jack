using System;
using System.Collections;
using System.Collections.Generic;

namespace card
{
    // TODO
    // ENUM bugos ugyanannál a value-nál
    public enum RANK
    {
        TWO = 2,
        THREE = 3,
        FOUR = 4,
        FIVE = 5,
        SIX = 6,
        SEVEN = 7,
        EIGHT = 8,
        NINE = 9,
        TEN = 10,
        JACK = -1,
        QUEEN = -2,
        KING = -3,
        ACE = 11
    }

    public enum SUIT
    {
        CLUBS,
        DIAMONDS,
        HEARTS,
        SPADES
    }

    public class Card
    {
        private RANK rank;
        private SUIT suit;

        public RANK Rank { get; set; }
        public SUIT Suit { get; set; }

        public Card(string Rank, string Suit)
        {
            this.Rank = (RANK)Enum.Parse(typeof(RANK), Rank);
            this.Suit = (SUIT)Enum.Parse(typeof(SUIT), Suit);
        }

        public override string ToString()
        {
            return "Rank: " + this.Rank + " Suit: " + this.Suit + "\n";
        }
    }
}