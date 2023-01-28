using System;
using System.Collections;
using System.Collections.Generic;

using dealer;
using player;
using deck;
using card;
using dealerai;
using playerai;

public class Game
{
    private int PlayerCount = 50;
    private int DeckCount = 6;
    private int moneySum;
    private List<Player> players;
    private Dealer dealer;
    private Deck deck;
    //private bool[PlayerCount + 1] BlackJack;

    public List<Player> Players { get; set; }
    public Deck Deck { get; set; }
    public Dealer Dealer { get; set; }
    public int MoneySum { get; set; }

    void Start()
    {
        deck = new Deck(DeckCount);
        players = new List<Player>();
        this.MoneySum = 0;
        for (int i = 0; i < PlayerCount; i++)
        {
            players.Add(new Player(i.ToString()));
        }
        dealer = new Dealer("Dealer");

        // ez egy kör, ezek ismétlõdnek, amíg ki nem fogynak pénzbõl
        {
            for (int i = 0; i < players.Count; i++)
            {
                players[i].MoneyIn = 0;
                players[i].Bet();
                this.MoneySum += 100;
            }

            Deal();


            // minden játékos dönt, amíg meg nem állnak vagy túllépik
            Play();

            // dealer lapokat húz
            DealerPlay();

            // kiderül ki nyert
            CheckWin();

            Console.WriteLine(this.ToString());
        }
    }

    public void Deal()
    {
        Card c = deck.Cards[0];

        deck.Cards.RemoveAt(0);
        dealer.Cards.Add(c);
        dealer.Update();
        for (int i = 0; i < players.Count; i++)
        {
            // BlackJack[i] = false;

            for (int j = 0; j < 2; j++)
            {
                c = deck.Cards[0];

                deck.Cards.RemoveAt(0);
                players[i].Cards.Add(c);

            }
            players[i].Update(dealer.Score);
            // 21 a játékosnak? akkor egybõl nyert -> 1.5cx kifizetés, és kimarad
            // dealer bj ?
            /*if (players[i].Score == 21)
            {
                BlackJack[i] = true;
            }*/
        }
    }

    public void Play()
    {
        try
        {
            Card c;
            for (int i = 0; i < players.Count; i++)
            {
                // TODO
                // while kívûlre, hogy mindenki egyesével dönthessen
                while (!players[i].HasStopped)
                {
                    players[i].Move();
                    switch (players[i].Ai.Decision)
                    {
                        case DECISION.STAND:
                            players[i].HasStopped = true;
                            break;
                        case DECISION.HIT:
                            c = deck.Cards[0];
                            deck.Cards.RemoveAt(0);
                            players[i].Cards.Add(c);
                            players[i].Update(dealer.Score);
                            break;
                        case DECISION.SPLIT:
                            Player p = players[i];
                            p.Cards.RemoveAt(1);
                            p.Bet();
                            p.MoneyIn = 100;
                            players[i].Cards.RemoveAt(0);
                            players.Add(p);

                            // új player, az eredeti kapja/veszti a pénzt
                            break;
                        case DECISION.DOUBLE:
                            c = deck.Cards[0];
                            deck.Cards.RemoveAt(0);
                            players[i].Cards.Add(c);
                            players[i].Update(dealer.Score);
                            players[i].HasStopped = true;
                            players[i].Bet();
                            this.MoneySum += 100;
                            break;
                        default: break;
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
        }
    }

    public void DealerPlay()
    {
        Card c;
        while (!dealer.HasStopped)
        {
            dealer.Move();
            switch (dealer.Dealerai.Decision)
            {
                case DECISION.STAND:
                    dealer.HasStopped = true;
                    break;
                case DECISION.HIT:
                    if (deck.Cards.Count > 0)
                    {
                        c = deck.Cards[0];
                        deck.Cards.RemoveAt(0);
                        dealer.Cards.Add(c);
                        dealer.Update();
                    }
                    else
                    {
                        Console.WriteLine("Elfogyott a lap");
                        Console.WriteLine(this.ToString());
                        Dealer.HasStopped = true;
                    }
                    break;
                default: break;
            }
        }
    }

    public void CheckWin()
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].Score > 21)
            {
                players[i].Result = RESULT.LOST;
            }
            else if (dealer.Score > 21)
            {
                players[i].Result = RESULT.WON;
            }
            else if (players[i].Score == dealer.Score)
            {
                players[i].Result = RESULT.TIE;
                players[i].Money += players[i].MoneyIn;
            }
            else
            {
                players[i].Result = RESULT.WON;
                players[i].Money += 2 * (players[i].MoneyIn);
            }
        }
    }

    public override string ToString()
    {
        string output = "";

        output += dealer.ToString();
        for (int i = 0; i < players.Count; i++)
        {
            output += players[i].ToString();
        }

        output += "\n";
        output += "--------------------------------";
        output += "\n";

        output += deck.ToString();

        output += "\n";
        output += "--------------------------------";
        output += "\n";

        output += "Cards in the deck: ";
        output += deck.Cards.Count.ToString();
        output += "\n";

        output += "Players in the game: ";
        output += this.PlayerCount;
        output += "\n";

        output += "Money in the game: ";
        output += this.MoneySum;
        output += "\n";

        return output;
    }
}
