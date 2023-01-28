using System;
using System.Collections;
using System.Collections.Generic;

using card;

namespace deck
{
    public class Deck
    {
        private List<Card> cards;

        public List<Card> Cards { get; set; }

        public Deck(int DeckCount)
        {
            Cards = new List<Card>();
            for (int i = 0; i < DeckCount; i++)
            {
                foreach (string rank in Enum.GetNames(typeof(RANK)))
                {
                    foreach (string suit in Enum.GetNames(typeof(SUIT)))
                    {
                        Cards.Add(new Card(rank, suit));
                    }
                }
            }
            Shuffle(DeckCount);
        }

        public void Shuffle(int DeckCount)
        {
            System.Random rnd = new System.Random();
            for (int i = 0; i < DeckCount * 200; i++)
            {
                int x = rnd.Next(0, DeckCount * 52);
                int y = rnd.Next(0, DeckCount * 52);

                Card c = Cards[x];
                Cards[x] = Cards[y];
                Cards[y] = c;
            }
        }
        public override string ToString()
        {
            string output = "";
            foreach (Card c in Cards)
            {
                output += c.ToString();
            }
            return output;
        }
    }
}


