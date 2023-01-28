using System;
using System.Collections;
using System.Collections.Generic;

using card;
using playerai;

namespace player
{
    public enum RESULT
    {
        UNKNOWN,
        LOST,
        TIE,
        WON
    }

    public class Player
    {
        private string name;
        private List<Card> cards;
        private List<string> decisions;
        private int money;
        private int moneyIn;
        private int score;
        private bool hasAce;
        private bool isDouble;
        private PlayerAI ai;
        private bool hasStopped;
        private RESULT result;

        public string Name { get; set; }
        public List<Card> Cards { get; set; }
        public List<string> Decisions { get; set; }
        public int Money { get; set; }
        public int MoneyIn { get; set; }
        public int Score { get; set; }
        public bool HasAce { get; set; }
        public bool IsDouble { get; set; }
        public PlayerAI Ai { get; set; }
        public bool HasStopped { get; set; }
        public RESULT Result { get; set; }

        public Player(string Name)
        {
            this.Name = Name;
            Cards = new List<Card>();
            Decisions = new List<string>();
            this.Money = 5000;
            this.MoneyIn = 0;
            this.Score = 0;
            this.HasAce = false;
            this.IsDouble = false;
            this.HasStopped = false;
            this.Result = RESULT.UNKNOWN;
        }

        public void Update(int DealerScore)
        {
            this.Score = 0;
            foreach (Card c in Cards)
            {
                if (c.Rank != RANK.ACE)
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
            }

            foreach (Card c in Cards)
            {
                if (c.Rank == RANK.ACE)
                {
                    this.HasAce = true;
                    this.Score += 1;
                }
            }

            foreach (Card c in Cards)
            {
                if (c.Rank == RANK.ACE)
                {
                    if (this.Score < 12)
                    {
                        this.Score += 10;
                    }
                }
            }

            // TODO
            // máshova
            if (Cards[0].Rank == Cards[1].Rank && Cards.Count < 3)
            {
                this.IsDouble = true;
                this.Decisions.Add("I have a Double \n");
            }
            else
            {
                this.IsDouble = false;
            }
            this.Ai = new PlayerAI(DealerScore, this.Score, this.HasAce, this.IsDouble);
        }

        public void Bet()
        {
            this.Money -= 100;
            this.MoneyIn += 100;
        }

        public void Move()
        {
            this.Ai.Decide();
            switch (this.Ai.Decision)
            {
                case DECISION.STAND:
                    this.Decisions.Add("I stand \n");
                    break;
                case DECISION.HIT:
                    this.Decisions.Add("I hit \n");
                    break;
                case DECISION.SPLIT:
                    this.Decisions.Add("I split \n");
                    break;
                case DECISION.DOUBLE:
                    this.Decisions.Add("I double \n");
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
            output += ("Player " + this.Name + ": ");
            output += "\n";

            foreach (Card c in Cards)
            {
                output += c.ToString();
            }
            output += "\n";

            output += "Money: ";
            output += this.Money.ToString();
            output += "\n";

            output += "Money put in: ";
            output += this.MoneyIn.ToString();
            output += "\n";

            output += "Score: ";
            output += this.Score.ToString();
            output += "\n";

            output += "Decision: \n";
            foreach (string s in Decisions)
            {
                output += s;
            }
            output += "\n";
            output += "Result: ";
            output += this.Result.ToString();
            return output;
        }
    }
}

