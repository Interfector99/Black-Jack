using System;
using System.Collections;
using System.Collections.Generic;

using card;
using dealerai;
using playerai;

namespace dealer
{
    public class Dealer
    {
        private string name;
        private List<Card> cards;
        private List<string> decisions;
        private int money;
        private int score;
        private DealerAI dealerai;
        private bool hasStopped;
        private bool hasLost;

        public string Name { get; set; }
        public List<Card> Cards { get; set; }
        public List<string> Decisions { get; set; }
        public int Money { get; set; }
        public int Score { get; set; }
        public DealerAI Dealerai { get; set; }
        public bool HasStopped { get; set; }
        public bool HasLost { get; set; }

        public Dealer(string Name)
        {
            this.Name = Name;
            Cards = new List<Card>();
            Decisions = new List<string>();
            this.Money = 50000;
            this.Score = 0;
            this.HasStopped = false;
            this.HasLost = false;
        }

        public void Update()
        {
            this.Score = 0;
            foreach (Card c in Cards)
            {
                if ((int)c.Rank > 0)
                {
                    this.Score += (int)c.Rank;
                }
                else
                {
                    this.Score += 10;
                }
            }
            this.Dealerai = new DealerAI(this.Score);
        }

        public void Move()
        {
            this.Dealerai.Decide();
            switch (this.Dealerai.Decision)
            {
                case DECISION.STAND:
                    this.Decisions.Add("I stand \n");
                    break;
                case DECISION.HIT:
                    this.Decisions.Add("I hit \n");
                    break;
                default:
                    break;
            }
        }

        public override string ToString()
        {
            string output = "";
            output += "\n";
            output += "--------------------------------";
            output += "\n";
            output += ("Dealer " + this.Name + ": ");
            output += "\n";

            foreach (Card c in Cards)
            {
                output += c.ToString();
            }
            output += "\n";

            output += "Money: ";
            output += this.Money.ToString();
            output += "\n";

            output += "Score: ";
            output += this.Score.ToString();
            output += "\n";

            output += "Decision: ";
            foreach (string s in Decisions)
            {
                output += s;
            }

            return output;
        }
    }
}