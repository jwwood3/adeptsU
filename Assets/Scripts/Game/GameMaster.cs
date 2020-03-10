using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using static Spell;
using static CardView;

public class GameMaster : MonoBehaviour
{
    public static GameMaster MASTER;
    public static int[] FIRSTGAME    = new int[20] {1,1,2,2,1,2,1,1,2,2,1,1,1,2,2,1,2,2,2,1};
    public static int[] IVORYTOWER   = new int[20] {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1};
    public static int[] BATTLEGROUND = new int[20] {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2};
    public static int[] PLAYER2      = new int[20] {2,1,1,1,2,2,2,1,2,1,1,2,1,1,2,2,2,2,1,1};
    public static int[] PLAYER5      = new int[20] {1,1,2,2,2,1,1,1,1,2,2,1,2,2,1,2,1,1,2,2};
    public static int[] CUSTOM;
    public GameObject EndGameSlide;
    public Camera boardCam;
    public Camera queueCam;
    public Camera cardCam;
    public Camera discardCam;
    public GameObject boardCanvas;
    public GameObject queueCanvas;
    public GameObject cardCanvas;
    public GameObject discardCanvas;
    public Material p1P1, p1P2, p1P3, p1P4;
    public Material p2P1, p2P2, p2P3, p2P4;
    public Material p3P1, p3P2, p3P3, p3P4;
    public Material p4P1, p4P2, p4P3, p4P4;
    public Material p5P1, p5P2, p5P3, p5P4;
    public Material clearThing;
    public static int[] transfiguration = new int[2] { -1, -1 };
    public static int mode = 1;
    public static string activeSpell = "";
    public static bool subsidence = false;
    public static int telepathyFrom = -1;
    public static int[] imperfection = new int[8] {0, 0, 0, 0, 0, 0, 0, 0};
    public static int stasis = -1;
    public static int[] siphonBat;
    public static int[] realignBat;
    public static bool siphoning = false;
    public static int[] plundered;
    public static int[] transfusion = new int[2] { -1, -1 };
    public static int altTurn = -1;
    public static int victim = -1;
    public static int PLAYERS = 5;
    public static Card toCast = null;
    public static PlayerObj[] players;
    public static int round = 1;
    public static int phase = 1;
    public static int subPhase = 3;
    public static int first = 0;
    public static int turn = 0;
    public static Mine[] mines = new Mine[8];
    public static Deck drawDeck;
    public static Deck discard;
    public static int gemBoosted = -1;
    public static bool checking = false;
    public static int toAdd = 2;
    public static GameObject[] manaMarkers;
    public static Vector3[] pColors = new Vector3[6] {new Vector3(255f, 0f, 0f),
                                                      new Vector3(0f, 96f, 255f),
                                                      new Vector3(0f, 255f, 0f),
                                                      new Vector3(155f, 0f, 115f),
                                                      new Vector3(255f, 155f, 0f),
                                                      new Vector3(0f,0f,0f)};
    public static int[] adeptBonuses = new int[8] { -1, -1, -1, -1, -1, -1, -1, -1 };

    public static void nextTurn()
    {
        updateSpells();
        turn++;
        if (turn == PLAYERS)
        {
            turn = 0;
        }
        if(subPhase==1 && phase != 4)
        {
            if (turn == first)
            {
                subPhase = 2;
                if (phase == 1)
                {
                    turn = adeptBonuses[0];
                    if (adeptBonuses[0] == -1)
                    {
                        turn = first;
                        subPhase = 3;
                        GameObject[] acts = GameObject.FindGameObjectsWithTag("ActiveCard");
                        for (int i = 0; i < acts.Length; i++) {
                            GameObject.Destroy(acts[i]);
                        }
                        if (players[turn].activeSpell != null)
                        {
                            activeSpellCard(players[turn].activeSpell.cardId);
                        }
                        if (players[turn].miners < 2)
                        {
                            nextTurn();
                        }
                    }
                }
                else if (phase == 2)
                {
                    turn = adeptBonuses[3];
                    if (adeptBonuses[3] == -1)
                    {
                        turn = first;
                        subPhase = 3;
                        phase2();
                    }
                }
                else if (phase == 3)
                {
                    //Layout top three spells from deck
                    subPhase = 2;
                    if (!players[turn].concentration)
                    {
                        if (drawDeck.deck.Count < 3)
                        {
                            int g = 0;
                            for (int i = 0; i < drawDeck.deck.Count; i++)
                            {
                                spellCard(i + 1, drawDeck.draw().cardId);
                                g++;
                            }

                            for (int j = 0; j < discard.deck.Count; j++)
                            {
                                drawDeck.add(discard.deck[j]);
                            }
                            discard = new Deck();
                            CardView.updateDiscard();
                            drawDeck.shuffle();
                            for (int k = g; k < 3; k++)
                            {
                                spellCard(k + 1, drawDeck.draw().cardId);
                            }
                        }
                        else
                        {
                            spellCard(1, drawDeck.draw().cardId);
                            spellCard(2, drawDeck.draw().cardId);
                            spellCard(3, drawDeck.draw().cardId);
                        }
                    }
                    else
                    {
                        players[turn].concentration = false;
                        nextTurn();
                    }
                }
                else
                {

                    if (adeptBonuses[6] != -1)
                    {
                        turn = adeptBonuses[6];
                        onyxAdeptBonus();
                    }
                    subPhase = 3;
                    turn = adeptBonuses[7];
                    if (adeptBonuses[7] == -1)
                    {
                        turn = first;
                        subPhase = 4;
                        siphonBat = new int[8] { turn, turn, turn, turn, turn, turn, turn, turn };
                        realignBat = new int[8] { 0, 1, 2, 3, 4, 5, 6, 7 };
                        if (siphoning)
                        {
                            altTurn = turn;
                            activeSpell = "Siphon";
                            for (int i = first; i < PLAYERS; i++)
                            {
                                if (i != altTurn && players[i].siphoned)
                                {
                                    turn = i;
                                    break;
                                }
                            }
                            if (turn == altTurn)
                            {
                                for (int i = 0; i < first; i++)
                                {
                                    if (i!=altTurn && players[i].siphoned)
                                    {
                                        turn = i;
                                        break;
                                    }
                                }
                            }
                            if (turn == altTurn)
                            {
                                advBat();
                            }
                        }
                        else if (players[turn].realignment)
                        {
                            realignBat = new int[8] { -1, -1, -1, -1, -1, -1, -1, -1 };
                            players[turn].realigning = 0;
                            activeSpell = "Realignment";
                        }
                        else
                        {
                            advBat();
                        }
                    }
                }
            }
            else
            {
                if (players[turn].hand.deck.Count != 0)
                {
                    for (int i = 1; i <= players[turn].hand.deck.Count; i++)
                    {
                        spellCard(i, players[turn].hand.deck[i - 1].cardId);
                    }
                }
                else
                {
                    nextTurn();
                }
            }
        }
        else if(phase == 1 && subPhase == 3)
        {
            GameObject[] acts = GameObject.FindGameObjectsWithTag("ActiveCard");
            for (int i = 0; i < acts.Length; i++)
            {
                GameObject.Destroy(acts[i]);
            }
            if (players[turn].activeSpell != null)
            {
                activeSpellCard(players[turn].activeSpell.cardId);
            }
            bool moreMiners = false;
            string minersNum = "";
            for (int i = 0; i < PLAYERS; i++)
            {
                minersNum += " "+(i+1)+": "+players[i].miners;
                if (players[i].miners >= 2)
                {
                    moreMiners = true;
                }
            }
            print("Miners - "+minersNum);
            print("minersLeft: "+moreMiners);
            if (!moreMiners)
            {
                turn = first;
                phase = 2;
                subPhase = 1;
                for(int i=0;i<PLAYERS;i++)
                {
                    players[i].miners = 0;
                }
                if (players[turn].hand.deck.Count != 0)
                {
                    for (int i = 1; i <= players[turn].hand.deck.Count; i++)
                    {
                        spellCard(i, players[turn].hand.deck[i - 1].cardId);
                    }
                }
                else
                {
                    nextTurn();
                }
            }
            else if (players[turn].miners < 2 || players[turn].suspended>0)
            {
                players[turn].suspended--;
                if (players[turn].suspended < 0)
                {
                    players[turn].suspended = 0;
                }
                nextTurn();
            }
        }
        else if(subPhase==2 && phase == 3)
        {
            if (turn == first)
            {
                phase = 4;
                subPhase = 1;
                phase4();
            }
            else
            {
                //Layout top three spells from deck
                if (drawDeck.deck.Count < 3)
                {
                    int g = 0;
                    for (int i = 0; i < drawDeck.deck.Count; i++)
                    {
                        spellCard(i+1, drawDeck.draw().cardId);
                        g++;
                    }

                    for (int j = 0; j < discard.deck.Count; j++)
                    {
                        drawDeck.add(discard.deck[j]);
                        
                    }
                    CardView.updateDiscard();
                    drawDeck.shuffle();
                    for (int k = g; k < 3; k++)
                    {
                        spellCard(k+1, drawDeck.draw().cardId);
                    }
                }
                else
                {
                    spellCard(1, drawDeck.draw().cardId);
                    spellCard(2, drawDeck.draw().cardId);
                    spellCard(3, drawDeck.draw().cardId);
                }
            }
        }
        else if(phase==5 && subPhase == 4)
        {
            if (turn == first)
            {
                subPhase = 5;
                phase5();
            }
            else
            {
                realignBat = new int[8] { 0, 1, 2, 3, 4, 5, 6, 7 };
                siphonBat = new int[8] { turn, turn, turn, turn, turn, turn, turn, turn };
                if (siphoning)
                {
                    altTurn = turn;
                    activeSpell = "Siphon";
                    for (int i = first; i < PLAYERS; i++)
                    {
                        if (i!=altTurn && players[i].siphoned)
                        {
                            turn = i;
                            break;
                        }
                    }
                    if (turn == altTurn)
                    {
                        for (int i = 0; i < first; i++)
                        {
                            if (i!= altTurn && players[i].siphoned)
                            {
                                turn = i;
                                break;
                            }
                        }
                    }
                }
                else if (players[turn].realignment)
                {
                    realignBat = new int[8] { -1, -1, -1, -1, -1, -1, -1, -1 };
                    players[turn].realigning = 0;
                    activeSpell = "Realignment";
                }
                else
                {
                    advBat();
                }
            }
        }
    }

    public static void setupDeck(int[] deckMask)
    {
        discard = new Deck();
        CardView.updateDiscard();
        drawDeck = new Deck();
        if (deckMask[0] == 1)
        {
            for (int i = 0; i < 4; i++)
            {
                drawDeck.add(new Card("1A")); /////
            }
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                drawDeck.add(new Card("1B"));
            }
        }
        if (deckMask[1] == 1)
        {
            for (int i = 0; i < 4; i++)
            {
                drawDeck.add(new Card("2A"));
            }
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                drawDeck.add(new Card("2B")); /////
            }
        }
        if (deckMask[2] == 1)
        {
            for (int i = 0; i < 3; i++)
            {
                drawDeck.add(new Card("3A")); /////
            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                drawDeck.add(new Card("3B"));
            }
        }
        if (deckMask[3] == 1)
        {
            for (int i = 0; i < 3; i++)
            {
                drawDeck.add(new Card("4A"));
            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                drawDeck.add(new Card("4B"));
            }
        }
        if (deckMask[4] == 1)
        {
            for (int i = 0; i < 2; i++)
            {
                drawDeck.add(new Card("5A"));
            }
        }
        else
        {
            for (int i = 0; i < 2; i++)
            {
                drawDeck.add(new Card("5B"));
            }
        }
        if (deckMask[5] == 1)
        {
            for (int i = 0; i < 4; i++)
            {
                drawDeck.add(new Card("6A"));
            }
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                drawDeck.add(new Card("6B"));
            }
        }
        if (deckMask[6] == 1)
        {
            for (int i = 0; i < 4; i++)
            {
                drawDeck.add(new Card("7A"));
            }
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                drawDeck.add(new Card("7B"));
            }
        }
        if (deckMask[7] == 1)
        {
            for (int i = 0; i < 3; i++)
            {
                drawDeck.add(new Card("8A"));
            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                drawDeck.add(new Card("8B"));
            }
        }
        if (deckMask[8] == 1)
        {
            for (int i = 0; i < 3; i++)
            {
                drawDeck.add(new Card("9A"));
            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                drawDeck.add(new Card("9B"));
            }
        }
        if (deckMask[9] == 1)
        {
            for (int i = 0; i < 2; i++)
            {
                drawDeck.add(new Card("10A"));
            }
        }
        else
        {
            for (int i = 0; i < 2; i++)
            {
                drawDeck.add(new Card("10B"));
            }
        }
        if (deckMask[10] == 1)
        {
            for (int i = 0; i < 4; i++)
            {
                drawDeck.add(new Card("11A"));
            }
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                drawDeck.add(new Card("11B"));
            }
        }
        if (deckMask[11] == 1)
        {
            for (int i = 0; i < 4; i++)
            {
                drawDeck.add(new Card("12A"));
            }
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                drawDeck.add(new Card("12B"));
            }
        }
        if (deckMask[12] == 1)
        {
            for (int i = 0; i < 3; i++)
            {
                drawDeck.add(new Card("13A"));
            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                drawDeck.add(new Card("13B"));
            }
        }
        if (deckMask[13] == 1)
        {
            for (int i = 0; i < 3; i++)
            {
                drawDeck.add(new Card("14A"));
            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                drawDeck.add(new Card("14B"));
            }
        }
        if (deckMask[14] == 1)
        {
            for (int i = 0; i < 2; i++)
            {
                drawDeck.add(new Card("15A"));
            }
        }
        else
        {
            for (int i = 0; i < 2; i++)
            {
                drawDeck.add(new Card("15B"));
            }
        }
        if (deckMask[15] == 1)
        {
            for (int i = 0; i < 4; i++)
            {
                drawDeck.add(new Card("16A"));
            }
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                drawDeck.add(new Card("16B"));
            }
        }
        if (deckMask[16] == 1)
        {
            for (int i = 0; i < 4; i++)
            {
                drawDeck.add(new Card("17A"));
            }
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                drawDeck.add(new Card("17B"));
            }
        }
        if (deckMask[17] == 1)
        {
            for (int i = 0; i < 3; i++)
            {
                drawDeck.add(new Card("18A"));
            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                drawDeck.add(new Card("18B"));
            }
        }
        if (deckMask[18] == 1)
        {
            for (int i = 0; i < 3; i++)
            {
                drawDeck.add(new Card("19A"));
            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                drawDeck.add(new Card("19B"));
            }
        }
        if (deckMask[19] == 1)
        {
            for (int i = 0; i < 2; i++)
            {
                drawDeck.add(new Card("20A"));
            }
        }
        else
        {
            for (int i = 0; i < 2; i++)
            {
                drawDeck.add(new Card("20B"));
            }
        }
        drawDeck.shuffle();
    }

    public static void setupMines()
    {
        print("Setting up mines");
        mines = new Mine[8];
        int sub = 10;
        int maj = 8;
        if (PLAYERS == 2)
        {
            sub = 10;
            maj = 8;
        }
        else if (PLAYERS == 3)
        {
            sub = 15;
            maj = 12;
        }
        else if (PLAYERS == 4)
        {
            sub = 20;
            maj = 16;
        }
        else if (PLAYERS == 5)
        {
            sub = 25;
            maj = 20;
        }
        mines[0] = new Mine(sub);
        mines[1] = new Mine(maj);
        mines[2] = new Mine(sub);
        mines[3] = new Mine(maj);
        mines[4] = new Mine(sub);
        mines[5] = new Mine(maj);
        mines[6] = new Mine(sub);
        mines[7] = new Mine(maj);
    }

    public static void setupPlayers()
    {
        players = new PlayerObj[PLAYERS];
        for(int i = 0; i < PLAYERS; i++)
        {
            players[i] = new PlayerObj();
            players[i].hand = new Deck();
            players[i].color = pColors[i];
            players[i].queue = new int[3][];
            players[i].queue[0] = new int[8];
            players[i].queue[1] = new int[8];
            players[i].queue[2] = new int[8];
            players[i].battery = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
        }

    }

    public static void phase2()
    {
        for(int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 4; j++)
            {
                if (mines[i].sites[j][1] != -1)
                {
                    for (int k = 0; k < mines[i].sites[j][0]; k++)
                    {
                        if (mines[i].pool > 0)
                        {
                            mines[i].pool -= 1;
                            if (players[mines[i].sites[j][1]].advancement[0] == i)
                            {
                                players[mines[i].sites[j][1]].queue[players[mines[i].sites[j][1]].advancement[1]][i] += 1;
                            }
                            else
                            {
                                players[mines[i].sites[j][1]].queue[0][i] += 1;
                            }
                        }
                    }
                    if (mines[i].pool > 0 && j != 0 && mines[i].sites[j][1] == adeptBonuses[2])
                    {
                        mines[i].pool--;
                        if (players[mines[i].sites[j][1]].advancement[0] == i)
                        {
                            players[mines[i].sites[j][1]].queue[players[mines[i].sites[j][1]].advancement[1]][i] += 1;
                        }
                        else
                        {
                            players[mines[i].sites[j][1]].queue[0][i] += 1;
                        }
                    }
                    if (mines[i].pool > 0 && j != 0 && mines[i].isBoosted)
                    {
                        mines[i].pool--;
                        if (players[mines[i].sites[j][1]].advancement[0] == i)
                        {
                            players[mines[i].sites[j][1]].queue[players[mines[i].sites[j][1]].advancement[1]][i] += 1;
                        }
                        else
                        {
                            players[mines[i].sites[j][1]].queue[0][i] += 1;
                        }
                    }
                    if (mines[i].pool > 0 && j != 0 && mines[i].isBoosted)
                    {
                        mines[i].pool--;
                        if (players[mines[i].sites[j][1]].advancement[0] == i)
                        {
                            players[mines[i].sites[j][1]].queue[players[mines[i].sites[j][1]].advancement[1]][i] += 1;
                        }
                        else
                        {
                            players[mines[i].sites[j][1]].queue[0][i] += 1;
                        }
                    }
                    mines[i].sites[j] = new int[2] { 0, -1 };
                }
            }
        }
        for(int i = 0; i < 8; i++)
        {
            mines[i].isBoosted = false;
            mines[i].closed = false;
        }
        for(int i = 0; i < PLAYERS; i++)
        {
            players[i].tunneling = false;
            players[i].miners = 10;
            if (i == adeptBonuses[1])
            {
                players[i].miners += 2;
            }
            players[i].advancement = new int[2] { -1, -1 };
        }
        subsidence = false;
        phase = 3;
        subPhase = 1;
        if (players[turn].hand.deck.Count != 0)
        {
            for (int i = 1; i <= players[turn].hand.deck.Count; i++)
            {
                spellCard(i, players[turn].hand.deck[i - 1].cardId);
            }
        }
        else
        {
            nextTurn();
        }
    }

    public static void phase4()
    {
        if (adeptBonuses[1] != -1)
        {
            players[adeptBonuses[1]].miners -= 2;
        }
        for (int bonusNum = 0; bonusNum < 8; bonusNum++)
        {
            int max = 0;
            int max3 = 0;
            int max2 = 0;
            int max1 = 0;
            int maxIndex = 0;
            for (int p = 0; p < PLAYERS; p++)
            {
                int playerCount1 = players[p].queue[0][bonusNum];
                int playerCount2 = players[p].queue[1][bonusNum];
                int playerCount3 = players[p].queue[2][bonusNum];
                int total = playerCount1 + playerCount2 + playerCount3;
                if (total > max)
                {
                    max = total;
                    max3 = playerCount3;
                    max2 = playerCount2;
                    max1 = playerCount1;
                    maxIndex = p;
                }
                else if (total == max)
                {
                    if (playerCount3 > max3)
                    {
                        max = total;
                        max3 = playerCount3;
                        max2 = playerCount2;
                        max1 = playerCount1;
                        maxIndex = p;
                    }
                    else if (playerCount3 == max3)
                    {
                        if (playerCount2 > max2)
                        {
                            max = total;
                            max3 = playerCount3;
                            max2 = playerCount2;
                            max1 = playerCount1;
                            maxIndex = p;
                        }
                        else if (playerCount2 == max2)
                        {
                            if (playerCount1 > max1)
                            {
                                max = total;
                                max3 = playerCount3;
                                max2 = playerCount2;
                                max1 = playerCount1;
                                maxIndex = p;
                            }
                            else if (playerCount1 == max1)
                            {
                                maxIndex = -1;
                            }
                        }
                    }
                }
            }
            if (maxIndex != -1)
            {
                print("Bonus " + bonusNum + " goes to player " + maxIndex);// Do stuff
                if (bonusNum == 1)
                {
                    players[maxIndex].miners += 2;
                }
            }
            else
            {
                print("No one gets bonus " + bonusNum);// Do stuff
            }
            adeptBonuses[bonusNum] = maxIndex;
        }
        updateAdepts();
        phase = 5;
        subPhase = 1;
        if (players[turn].hand.deck.Count != 0)
        {
            for (int i = 1; i <= players[turn].hand.deck.Count; i++)
            {
                spellCard(i, players[turn].hand.deck[i - 1].cardId);
            }
        }
        else
        {
            nextTurn();
        }
    }

    public static void advBat()
    {
        //Move battery up for gems in queue 3
        for (int j = 0; j < 8; j++)
        {
            if (stasis != j)
            {
                int add = 1;
                if (j % 2 == players[turn].intensify)
                {
                    add = 2;
                }
                if (players[turn].moratorium != j)
                {
                    for (int k = 0; k < players[turn].queue[2][j]; k++)
                    {
                        players[siphonBat[j]].battery[realignBat[j]] += add;
                        if (players[siphonBat[j]].battery[realignBat[j]] > 10)
                        {
                            players[siphonBat[j]].battery[realignBat[j]] = 10;
                        }
                    }
                }
            }
        }
        nextTurn();
    }

    public static void phase5()
    {
        //Process gems in queue 3 for mana and return them to mine
        
        for(int i = 0; i < PLAYERS; i++)
        {
            int[] negate = new int[players[i].negation];
            for(int l = 0; l < players[i].negation; l++)
            {
                negate[l] = -1;
            }
            for (int f=0;f< players[i].negation;f++)
            {
                int max = 0;
                for(int k = 1; k < 8; k++)
                {
                    if (!Array.Exists(negate, element => element == k) && players[i].queue[2][k] * ((k % 2) + 1) > max)
                    {
                        negate[f] = k;
                        max = players[i].queue[2][k] * ((k % 2) + 1);
                    }
                }
            }
            for (int j = 0; j < 8; j+=2)
            {
                if (!Array.Exists(negate, element => element == j))
                {
                    if (players[i].moratorium != j)
                    {
                        if (players[i].mutability && players[i].queue[2][j] > 0)
                        {
                            players[i].manaCount++;
                        }
                        int temp = 1;
                        if (imperfection[j] != 0)
                        {
                            temp = imperfection[j]*2;
                        }
                        for (int k = 0; k < players[i].queue[2][j]; k += temp)
                        {
                            if (gemBoosted == j)
                            {
                                players[i].manaCount += 2;
                            }
                            players[i].manaCount += 1;
                            mines[j].pool++;
                        }
                        players[i].queue[2][j] = 0;
                    }
                }
            }
            for (int j = 1; j < 8; j += 2)
            {
                if (!Array.Exists(negate, element => element == j))
                {
                    if (players[i].moratorium != j)
                    {
                        if (players[i].mutability && players[i].queue[2][j] > 0)
                        {
                            players[i].manaCount++;
                        }
                        int temp = 1;
                        if (imperfection[j] != 0)
                        {
                            temp = imperfection[j];
                        }
                        for (int k = 0; k < players[i].queue[2][j]; k += temp)
                        {
                            if (gemBoosted == j)
                            {
                                players[i].manaCount += 1;
                            }
                            players[i].manaCount += 2;
                            mines[j].pool++;
                        }
                        players[i].queue[2][j] = 0;
                    }
                }
            }
        }
        //Process gems from queue 1 to 2 and 2 to 3
        for (int i=0; i < PLAYERS; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                if (players[i].moratorium != j)
                {
                    players[i].queue[2][j] = players[i].queue[1][j];
                    players[i].queue[1][j] = players[i].queue[0][j];
                    players[i].queue[0][j] = 0;
                }
            }
        }
        for(int i = 0; i < PLAYERS; i++)
        {
            players[i].mutability = false;
            players[i].moratorium = -1;
            players[i].intensify = -1;
            players[i].negation = 0;
            players[i].siphoned = false;
        }
        siphonBat = new int[8] { -1, -1, -1, -1, -1, -1, -1, -1 };
        imperfection = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
        round++;
        first++;
        if (first == PLAYERS)
        {
            first = 0;
        }
        turn = first;
        if (round == 7)
        {
            endGame();
        }
        else
        {
            phase = 1;
            subPhase = 1;
            if (players[turn].hand.deck.Count != 0)
            {
                for (int i = 1; i <= players[turn].hand.deck.Count; i++)
                {
                    spellCard(i, players[turn].hand.deck[i - 1].cardId);
                }
            }
            else
            {
                nextTurn();
            }
        }
    }

    public static void onyxAdeptBonus()
    {
        for(int i = 0; i < 8; i++)
        {
            if (players[adeptBonuses[6]].queue[2][i] > 0)
            {
                players[adeptBonuses[6]].battery[i]++;
                if (players[adeptBonuses[6]].battery[i] > 10)
                {
                    players[adeptBonuses[6]].battery[i] = 10;
                }
            }
        }
    }

    public static void endGame()
    {
        for(int i = 0; i < PLAYERS; i++)
        {
            for(int j = 0; j < 8; j += 2)
            {
                int gemTotal = players[i].queue[1][j] + players[i].queue[2][j];
                players[i].manaCount += (int)(gemTotal/2.0);
                players[i].queue[1][j] = 0;
                players[i].queue[2][j] = 0;
            }
            for(int j = 1; j < 8; j += 2)
            {
                int gemTotal = players[i].queue[1][j] + players[i].queue[2][j];
                players[i].manaCount += gemTotal;
                players[i].queue[1][j] = 0;
                players[i].queue[2][j] = 0;
            }
            for(int j = 0; j < 8; j++)
            {
                players[i].manaCount += convertBattery(players[i].battery[j]);
                players[i].battery[j] = 0;
            }
        }
        //GameObject slide = Instantiate(MASTER.EndGameSlide,new Vector3(-9.18f,2.04f,0.0f),Quaternion.identity);
        //string endText = "";
        //for(int i = 0; i < PLAYERS; i++)
        //{
        //    endText += "Player " + (i + 1) + ": " + players[i].manaCount + "\n";
        //}
        //((TextMeshPro)(slide.GetComponentInChildren(typeof(TextMeshPro)))).text = endText;
    }

    public static int convertBattery(int batLevel)
    {
        switch (batLevel)
        {
            case 0:
                return 0;
            case 1:
                return 1;
            case 2:
                return 2;
            case 3:
                return 4;
            case 4:
                return 7;
            case 5:
                return 10;
            case 6:
                return 13;
            case 7:
                return 16;
            case 8:
                return 18;
            case 9:
                return 19;
            case 10:
                return 20;
            default:
                return 0;
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        mode = 1;
        int playernum = 5;
        int[] spellOrder = FIRSTGAME;
        GameObject menu = GameObject.Find("MenuData");
        if (menu != null) {
            Play MenuData = menu.GetComponent<Play>();
            playernum = MenuData.PLAYERNUM;
            print(MenuData.spellLine);
            switch (MenuData.spellLine)
            {
                case 0:
                    spellOrder = FIRSTGAME;
                    break;
                case 1:
                    spellOrder = IVORYTOWER;
                    break;
                case 2:
                    spellOrder = BATTLEGROUND;
                    break;
                case 3:
                    spellOrder = PLAYER2;
                    break;
                case 4:
                    spellOrder = PLAYER5;
                    break;
                case 5:
                    spellOrder = CUSTOM;
                    break;
                default:
                    spellOrder = FIRSTGAME;
                    break;
            }
        }
        PLAYERS = playernum;
        boardCam.enabled = true;
        queueCam.enabled = false;
        cardCam.enabled = false;
        discardCam.enabled = false;
        MASTER = this;
        manaMarkers = new GameObject[PLAYERS];
        for(int i = 0; i < PLAYERS; i++)
        {
            manaMarkers[i] = GameObject.Find(("markerP" + (i + 1)));
        }
        setupDeck(spellOrder);
        setupMines();
        setupPlayers();
    }

    Vector3 getMarkerPosition(int p)
    {
        int adjMana = players[p].manaCount % 100;
        float xPos = -9.73f;
        float yPos = 7.35f;
        float zPos = 5.0f;
        for(int i = 0; i < adjMana; i++)
        {
            if (i >= 80)
            {
                yPos += 0.635f;
            }
            else if (i >= 50)
            {
                xPos -= 0.635f;
            }
            else if (i >= 30)
            {
                yPos -= 0.635f;
            }
            else
            {
                xPos += 0.635f;
            }
        }
        xPos += 0.1f * p;
        return new Vector3(xPos, yPos, zPos);
    }

    Material getMarkerMaterial(int index)
    {
        if (players[index].manaCount < 100)
        {
            switch (index)
            {
                case 0:
                    return p1P1;
                case 1:
                    return p2P1;
                case 2:
                    return p3P1;
                case 3:
                    return p4P1;
                case 4:
                    return p5P1;
                default:
                    return p1P1;
            }
        }
        else if(players[index].manaCount < 200)
        {
            switch (index)
            {
                case 0:
                    return p1P2;
                case 1:
                    return p2P2;
                case 2:
                    return p3P2;
                case 3:
                    return p4P2;
                case 4:
                    return p5P2;
                default:
                    return p1P2;
            }
        }
        else if(players[index].manaCount < 300)
        {
            switch (index)
            {
                case 0:
                    return p1P3;
                case 1:
                    return p2P3;
                case 2:
                    return p3P3;
                case 3:
                    return p4P3;
                case 4:
                    return p5P3;
                default:
                    return p1P3;
            }
        }
        else if(players[index].manaCount < 400)
        {
            switch (index)
            {
                case 0:
                    return p1P4;
                case 1:
                    return p2P4;
                case 2:
                    return p3P4;
                case 3:
                    return p4P4;
                case 4:
                    return p5P4;
                default:
                    return p1P4;
            }
        }
        else
        {
            switch (index)
            {
                case 0:
                    return p1P1;
                case 1:
                    return p2P1;
                case 2:
                    return p3P1;
                case 3:
                    return p4P1;
                case 4:
                    return p5P1;
                default:
                    return p1P1;
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            print("Quit");
            Application.Quit();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!queueCam.enabled)
            {
                boardCam.enabled = false;
                queueCam.enabled = true;
                cardCam.enabled = false;
                discardCam.enabled = false;
                boardCanvas.SetActive(false);
                queueCanvas.SetActive(true);
                cardCanvas.SetActive(false);
                discardCanvas.SetActive(false);
                mode = 2;
            }
            else
            {
                mode = 1;
                boardCam.enabled = true;
                queueCam.enabled = false;
                cardCam.enabled = false;
                discardCam.enabled = false;
                boardCanvas.SetActive(true);
                queueCanvas.SetActive(false);
                cardCanvas.SetActive(false);
                discardCanvas.SetActive(false);
            }
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            if (!cardCam.enabled)
            {
                updateSpells();
                boardCam.enabled = false;
                queueCam.enabled = false;
                cardCam.enabled = true;
                discardCam.enabled = false;
                boardCanvas.SetActive(false);
                queueCanvas.SetActive(false);
                cardCanvas.SetActive(true);
                discardCanvas.SetActive(false);
                mode = 3;
            }
            else
            {
                mode = 1;
                boardCam.enabled = true;
                queueCam.enabled = false;
                cardCam.enabled = false;
                discardCam.enabled = false;
                boardCanvas.SetActive(true);
                queueCanvas.SetActive(false);
                cardCanvas.SetActive(false);
                discardCanvas.SetActive(false);
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))// && activeSpell == "Munificence")
        {
            if (!discardCam.enabled)
            {
                updateDiscard();
                boardCam.enabled = false;
                queueCam.enabled = false;
                cardCam.enabled = false;
                discardCam.enabled = true;
                boardCanvas.SetActive(false);
                queueCanvas.SetActive(false);
                cardCanvas.SetActive(false);
                discardCanvas.SetActive(true);
                mode = 4;
            }
            else
            {
                mode = 1;
                boardCam.enabled = true;
                queueCam.enabled = false;
                cardCam.enabled = false;
                discardCam.enabled = false;
                boardCanvas.SetActive(true);
                queueCanvas.SetActive(false);
                cardCanvas.SetActive(false);
                discardCanvas.SetActive(false);
            }
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            GameObject[] cards = GameObject.FindGameObjectsWithTag("Card");
            for(int i = 0; i < cards.Length; i++)
            {
                cards[i].GetComponent<BoxCollider>().enabled = !cards[i].GetComponent<BoxCollider>().enabled;
                for (int j = 0; j < cards[i].transform.childCount; j++)
                {
                    cards[i].transform.GetChild(j).GetComponent<MeshRenderer>().enabled = !cards[i].transform.GetChild(j).GetComponent<MeshRenderer>().enabled;
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            if (activeSpell == "Purge")
            {
                GameObject[] cardObjs = GameObject.FindGameObjectsWithTag("Card");
                for (int i = 0; i < cardObjs.Length; i++)
                {
                    GameObject.Destroy(cardObjs[i]);
                }
                activeSpell = "";
                nextTurn();
            }
            else if (activeSpell == "Spellsteal")
            {
                GameObject[] cardObjs = GameObject.FindGameObjectsWithTag("Card");
                for (int i = 0; i < cardObjs.Length; i++)
                {
                    GameObject.Destroy(cardObjs[i]);
                }
                activeSpell = "";
                nextTurn();
            }
            else if (subPhase == 1 && phase != 4 && activeSpell == "")
            {
                GameObject[] cardObjs = GameObject.FindGameObjectsWithTag("Card");
                for (int i = 0; i < cardObjs.Length; i++)
                {
                    GameObject.Destroy(cardObjs[i]);
                }
                nextTurn();
            }
            else if (subPhase == 2 && phase == 3)
            {
                GameObject[] cardObjs = GameObject.FindGameObjectsWithTag("Card");
                for (int i = 0; i < cardObjs.Length; i++)
                {
                    Card c = cardObjs[i].GetComponent<SpellCase>().spellcard;
                    discard.add(new Card(c.cardId));
                    CardView.updateDiscard();
                    if (turn == adeptBonuses[4])
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            players[turn].battery[j] += c.cost[j];
                            if (players[turn].battery[j] > 10)
                            {
                                players[turn].battery[j] = 10;
                            }
                        }
                    }
                    GameObject.Destroy(cardObjs[i]);
                }
                if (players[turn].hand.deck.Count > 5)
                {
                    checking = true;
                    for (int i = 0; i < 5; i++)
                    {
                        spellCard(i + 1, players[turn].hand.deck[i].cardId);
                    }
                }
                else
                {
                    nextTurn();
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (activeSpell == "Suspension")
            {
                players[0].suspended++;
                activeSpell = "";
                nextTurn();
            }
            else if (activeSpell == "Pilfer")
            {
                players[0].manaCount -= 5;
                if (players[0].manaCount < 0)
                {
                    players[turn].manaCount += players[0].manaCount;
                    players[0].manaCount = 0;
                }
                players[turn].manaCount += 5;
                activeSpell = "";
                nextTurn();
            }
            else if (activeSpell == "Spellsteal" && victim == -1)
            {
                for (int i = 1; i <= players[0].hand.deck.Count; i++)
                {
                    spellCard(i, players[0].hand.deck[i - 1].cardId);
                }
                victim = 0;
            }
            else if(activeSpell == "Transfusion1")
            {
                for(int i = 0; i < players[turn].queue[transfusion[0]][transfusion[1]]; i++)
                {
                    players[turn].battery[transfusion[1]]++;
                    if (players[turn].battery[transfusion[1]] > 10)
                    {
                        players[turn].battery[transfusion[1]] = 10;
                    }
                }
                players[turn].queue[transfusion[0]][transfusion[1]] = 0;
                activeSpell = "";
                nextTurn();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && PLAYERS > 1)
        {
            if (activeSpell == "Suspension")
            {
                players[1].suspended++;
                activeSpell = "";
                nextTurn();
            }
            else if (activeSpell == "Pilfer")
            {
                players[1].manaCount -= 5;
                if (players[1].manaCount < 0)
                {
                    players[turn].manaCount += players[1].manaCount;
                    players[1].manaCount = 0;
                }
                players[turn].manaCount += 5;
                activeSpell = "";
                nextTurn();
            }
            else if (activeSpell == "Spellsteal" && victim == -1)
            {
                for (int i = 1; i <= players[1].hand.deck.Count; i++)
                {
                    spellCard(i, players[1].hand.deck[i - 1].cardId);
                }
                victim = 1;
            }
            else if (activeSpell == "Transfusion1")
            {
                for (int i = 0; i < players[turn].queue[transfusion[0]][transfusion[1]]; i++)
                {
                    if (transfusion[1] % 2 == 0)
                    {
                        players[turn].manaCount++;
                    }
                    else
                    {
                        players[turn].manaCount += 2;
                    }
                }
                players[turn].queue[transfusion[0]][transfusion[1]] = 0;
                activeSpell = "";
                nextTurn();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && PLAYERS > 2)
        {
            if (activeSpell == "Suspension")
            {
                players[2].suspended++;
                activeSpell = "";
                nextTurn();
            }
            else if (activeSpell == "Pilfer")
            {
                players[2].manaCount -= 5;
                if (players[2].manaCount < 0)
                {
                    players[turn].manaCount += players[2].manaCount;
                    players[2].manaCount = 0;
                }
                players[turn].manaCount += 5;
                activeSpell = "";
                nextTurn();
            }
            else if (activeSpell == "Spellsteal" && victim == -1)
            {
                for (int i = 1; i <= players[2].hand.deck.Count; i++)
                {
                    spellCard(i, players[2].hand.deck[i - 1].cardId);
                }
                victim = 2;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && PLAYERS > 3)
        {
            if (activeSpell == "Suspension")
            {
                players[3].suspended++;
                activeSpell = "";
                nextTurn();
            }
            else if (activeSpell == "Pilfer")
            {
                players[3].manaCount -= 5;
                if (players[3].manaCount < 0)
                {
                    players[turn].manaCount += players[3].manaCount;
                    players[3].manaCount = 0;
                }
                players[turn].manaCount += 5;
                activeSpell = "";
                nextTurn();
            }
            else if (activeSpell == "Spellsteal" && victim == -1)
            {
                for (int i = 1; i <= players[3].hand.deck.Count; i++)
                {
                    spellCard(i, players[3].hand.deck[i - 1].cardId);
                }
                victim = 3;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5) && PLAYERS > 4)
        {
            if (activeSpell == "Suspension")
            {
                players[4].suspended++;
                activeSpell = "";
                nextTurn();
            }
            else if (activeSpell == "Pilfer")
            {
                players[4].manaCount -= 5;
                if (players[4].manaCount < 0)
                {
                    players[turn].manaCount += players[4].manaCount;
                    players[4].manaCount = 0;
                }
                players[turn].manaCount += 5;
                activeSpell = "";
                nextTurn();
            }
            else if (activeSpell == "Spellsteal" && victim == -1)
            {
                for (int i = 1; i <= players[4].hand.deck.Count; i++)
                {
                    spellCard(i, players[4].hand.deck[i - 1].cardId);
                }
                victim = 4;
            }
        }
        for(int i = 0; i < manaMarkers.Length; i++)
        {
            manaMarkers[i].transform.position = getMarkerPosition(i);
            manaMarkers[i].GetComponent<MeshRenderer>().material = getMarkerMaterial(i);
        }
        
        
    }
}
