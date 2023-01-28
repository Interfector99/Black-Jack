using System;
using System.Collections;
using System.Collections.Generic;

namespace playerai
{
    public enum DECISION
    {
        HIT,
        STAND,
        DOUBLE,
        SPLIT
    }

    public class PlayerAI
    {
        private int dealerScore;
        private int score;
        private bool hasAce;
        private bool isDouble;
        private DECISION decision;

        public int DealerScore { get; set; }
        public int Score { get; set; }
        public bool HasAce { get; set; }
        public bool IsDouble { get; set; }
        public DECISION Decision { get; set; }

        public PlayerAI(int DealerScore, int Score, bool HasAce, bool IsDouble)
        {
            this.DealerScore = DealerScore;
            this.Score = Score;
            this.HasAce = HasAce;
            this.IsDouble = IsDouble;
        }

        public void Decide()
        {
            // nincs dupla lap
            if (!this.IsDouble)
            {
                // nincs ász
                // legfelsõ táblázat
                if (!this.HasAce)
                {
                    if (this.Score >= 17)
                    {
                        this.Decision = DECISION.STAND;
                    }
                    else if (this.Score >= 13 && this.DealerScore <= 6)
                    {
                        this.Decision = DECISION.STAND;
                    }
                    else if (this.Score == 12 && this.DealerScore >= 4 && this.DealerScore <= 6)
                    {
                        this.Decision = DECISION.STAND;
                    }
                    else if (this.Score == 11)
                    {
                        this.Decision = DECISION.DOUBLE;
                    }
                    else if (this.Score == 10 && this.DealerScore <= 9)
                    {
                        this.Decision = DECISION.DOUBLE;
                    }
                    else if (this.Score == 9 && this.DealerScore >= 3 && this.DealerScore <= 6)
                    {
                        this.Decision = DECISION.DOUBLE;
                    }
                    else
                    {
                        this.Decision = DECISION.HIT;
                    }
                }
                else
                {
                    // van ász
                    // középsõ táblázat
                    if (this.Score >= 20)
                    {
                        // 21 beleszámít?
                        this.Decision = DECISION.STAND;
                    }
                    else if (this.Score == 19 && this.DealerScore != 6)
                    {
                        this.Decision = DECISION.STAND;
                    }
                    else if (this.Score == 19)
                    {
                        this.Decision = DECISION.DOUBLE;
                    }
                    else if (this.Score == 18 && this.DealerScore <= 6)
                    {
                        this.Decision = DECISION.DOUBLE;
                    }
                    else if (this.Score == 18 && this.DealerScore <= 8)
                    {
                        this.Decision = DECISION.STAND;
                    }
                    else if (this.Score == 18)
                    {
                        this.Decision = DECISION.HIT;
                    }
                    else if (this.Score == 17 && this.DealerScore >= 3 && this.DealerScore <= 6)
                    {
                        this.Decision = DECISION.DOUBLE;
                    }
                    else if (this.Score == 17)
                    {
                        this.Decision = DECISION.HIT;
                    }
                    else if (this.Score == 16 && this.DealerScore >= 4 && this.DealerScore <= 6)
                    {
                        this.Decision = DECISION.DOUBLE;
                    }
                    else if (this.Score == 16)
                    {
                        this.Decision = DECISION.HIT;
                    }
                    else if (this.Score == 15 && this.DealerScore >= 4 && this.DealerScore <= 6)
                    {
                        this.Decision = DECISION.DOUBLE;
                    }
                    else if (this.Score == 15)
                    {
                        this.Decision = DECISION.HIT;
                    }
                    else if (this.Score == 14 && this.DealerScore >= 5 && this.DealerScore <= 6)
                    {
                        this.Decision = DECISION.DOUBLE;
                    }
                    else if (this.Score == 14)
                    {
                        this.Decision = DECISION.HIT;
                    }
                    else if (this.Score == 13 && this.DealerScore >= 5 && this.DealerScore <= 6)
                    {
                        this.Decision = DECISION.DOUBLE;
                    }
                    else if (this.Score == 13)
                    {
                        this.Decision = DECISION.HIT;
                    }
                }
            }
            else
            {
                // van dupla
                // alsó táblázat
                if (this.Score == 22)
                {
                    this.Decision = DECISION.SPLIT;
                }
                else if (this.Score == 18 && this.DealerScore != 7 && this.DealerScore != 10 && this.DealerScore != 11)
                {
                    this.Decision = DECISION.SPLIT;
                }
                else if (this.Score == 16)
                {
                    this.Decision = DECISION.SPLIT;
                }
                else if (this.Score == 14 && this.DealerScore <= 7)
                {
                    this.Decision = DECISION.SPLIT;
                }
                else if (this.Score == 12 && this.DealerScore <= 6)
                {
                    this.Decision = DECISION.SPLIT;
                }
                else if (this.Score == 8 && this.DealerScore >= 5 && this.DealerScore <= 6)
                {
                    this.Decision = DECISION.SPLIT;
                }
                else if (this.Score == 6 && this.DealerScore <= 7)
                {
                    this.Decision = DECISION.SPLIT;
                }
                else if (this.Score == 4 && this.DealerScore <= 7)
                {
                    this.Decision = DECISION.SPLIT;
                }
                else
                {
                    this.IsDouble = false;
                    this.Decide();
                }
            }
        }
    }
}

