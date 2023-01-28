using System;
using System.Collections;
using System.Collections.Generic;

using playerai;

namespace dealerai
{
    public class DealerAI
    {
        private int score;
        private DECISION decision;

        public int Score { get; set; }
        public DECISION Decision { get; set; }

        public DealerAI(int Score)
        {
            this.Score = Score;
        }

        public void Decide()
        {
            if (this.Score >= 17)
            {
                this.Decision = DECISION.STAND;
            }
            else
            {
                this.Decision = DECISION.HIT;
            }
        }
    }
}

