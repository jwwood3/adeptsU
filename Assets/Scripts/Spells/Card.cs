using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameMaster;
using static Spell;

public class Card
{
    public string cardId = "1A";
    public string cardName = "";
    public int usePhase = 1;
    public int[] cost = new int[8] { 1, 0, 0, 0, 0, 0, 0, 0 };

    public Card()
    {
        cardId = "1A";
        cardName = "";
        usePhase = 1;
        cost = new int[8] { 1, 0, 0, 0, 0, 0, 0, 0 };
    }

    public Card(string cId, string cName, int uPhase, int[] cCost)
    {
        cardId = cId;
        cardName = cName;
        usePhase = uPhase;
        cost = cCost;
    }

    public Card(string cId)
    {
        cardId = cId;
        switch (cId)
        {
            case "1A":
                this.cardName = "Ambition";
                this.usePhase = 1;
                this.cost = new int[8] { 1, 0, 0, 0, 0, 0, 0, 0 };
                break;
            case "1B":
                this.cardName = "Suspension";
                this.usePhase = 1;
                this.cost = new int[8] { 1, 0, 0, 0, 0, 0, 0, 0 };
                break;
            case "2A":
                this.cardName = "Initiative";
                this.usePhase = 1;
                this.cost = new int[8] { 0, 1, 0, 0, 0, 0, 0, 0 };
                break;
            case "2B":
                this.cardName = "Backlash";
                this.usePhase = 1;
                this.cost = new int[8] { 0, 1, 0, 0, 0, 0, 0, 0 };
                break;
            case "3A":
                this.cardName = "Gateways";
                this.usePhase = 1;
                this.cost = new int[8] { 2, 0, 0, 0, 0, 0, 0, 0 };
                break;
            case "3B":
                this.cardName = "Barrier";
                this.usePhase = 1;
                this.cost = new int[8] { 2, 0, 0, 0, 0, 0, 0, 0 };
                break;
            case "4A":
                this.cardName = "Tunneling";
                this.usePhase = 1;
                this.cost = new int[8] { 1, 1, 0, 0, 0, 0, 0, 0 };
                break;
            case "4B":
                this.cardName = "Subsidence";
                this.usePhase = 1;
                this.cost = new int[8] { 1, 1, 0, 0, 0, 0, 0, 0 };
                break;
            case "5A":
                this.cardName = "Precedence";
                this.usePhase = 1;
                this.cost = new int[8] { 0, 2, 0, 0, 0, 0, 0, 0 };
                break;
            case "5B":
                this.cardName = "Lassitude";
                this.usePhase = 1;
                this.cost = new int[8] { 0, 2, 0, 0, 0, 0, 0, 0 };
                break;
            case "6A":
                this.cardName = "Telepathy";
                this.usePhase = 2;
                this.cost = new int[8] { 0, 0, 1, 0, 0, 0, 0, 0 };
                break;
            case "6B":
                this.cardName = "Teleport";
                this.usePhase = 2;
                this.cost = new int[8] { 0, 0, 1, 0, 0, 0, 0, 0 };
                break;
            case "7A":
                this.cardName = "Extraction";
                this.usePhase = 2;
                this.cost = new int[8] { 0, 0, 0, 1, 0, 0, 0, 0 };
                break;
            case "7B":
                this.cardName = "Transfiguration";
                this.usePhase = 2;
                this.cost = new int[8] { 0, 0, 0, 1, 0, 0, 0, 0 };
                break;
            case "8A":
                this.cardName = "Telekinesis";
                this.usePhase = 2;
                this.cost = new int[8] { 0, 0, 2, 0, 0, 0, 0, 0 };
                break;
            case "8B":
                this.cardName = "Contagion";
                this.usePhase = 2;
                this.cost = new int[8] { 0, 0, 2, 0, 0, 0, 0, 0 };
                break;
            case "9A":
                this.cardName = "Advancement";
                this.usePhase = 2;
                this.cost = new int[8] { 0, 0, 1, 1, 0, 0, 0, 0 };
                break;
            case "9B":
                this.cardName = "Plunder";
                this.usePhase = 2;
                this.cost = new int[8] { 0, 0, 1, 1, 0, 0, 0, 0 };
                break;
            case "10A":
                this.cardName = "Phantasms";
                this.usePhase = 2;
                this.cost = new int[8] { 0, 0, 0, 2, 0, 0, 0, 0 };
                break;
            case "10B":
                this.cardName = "Earthquake";
                this.usePhase = 2;
                this.cost = new int[8] { 0, 0, 0, 2, 0, 0, 0, 0 };
                break;
            case "11A":
                this.cardName = "Purge";
                this.usePhase = 3;
                this.cost = new int[8] { 0, 0, 0, 0, 1, 0, 0, 0 };
                break;
            case "11B":
                this.cardName = "Dissipate";
                this.usePhase = 3;
                this.cost = new int[8] { 0, 0, 0, 0, 1, 0, 0, 0 };
                break;
            case "12A":
                this.cardName = "Transmission";
                this.usePhase = 3;
                this.cost = new int[8] { 0, 0, 0, 0, 0, 1, 0, 0 };
                break;
            case "12B":
                this.cardName = "Pilfer";
                this.usePhase = 3;
                this.cost = new int[8] { 0, 0, 0, 0, 0, 1, 0, 0 };
                break;
            case "13A":
                this.cardName = "Pilfer";
                this.usePhase = 3;
                this.cost = new int[8] { 0, 0, 0, 0, 2, 0, 0, 0 };
                break;
            case "13B":
                this.cardName = "Depletion";
                this.usePhase = 3;
                this.cost = new int[8] { 0, 0, 0, 0, 2, 0, 0, 0 };
                break;
            case "14A":
                this.cardName = "Transfusion";
                this.usePhase = 3;
                this.cost = new int[8] { 0, 0, 0, 0, 1, 1, 0, 0 };
                break;
            case "14B":
                this.cardName = "Nullify";
                this.usePhase = 3;
                this.cost = new int[8] { 0, 0, 0, 0, 1, 1, 0, 0 };
                break;
            case "15A":
                this.cardName = "Munificence";
                this.usePhase = 3;
                this.cost = new int[8] { 0, 0, 0, 0, 0, 2, 0, 0 };
                break;
            case "15B":
                this.cardName = "Spellsteal";
                this.usePhase = 3;
                this.cost = new int[8] { 0, 0, 0, 0, 0, 2, 0, 0 };
                break;
            case "16A":
                this.cardName = "Mutability";
                this.usePhase = 5;
                this.cost = new int[8] { 0, 0, 0, 0, 0, 0, 1, 0 };
                break;
            case "16B":
                this.cardName = "Overload";
                this.usePhase = 5;
                this.cost = new int[8] { 0, 0, 0, 0, 0, 0, 1, 0 };
                break;
            case "17A":
                this.cardName = "Moratorium";
                this.usePhase = 5;
                this.cost = new int[8] { 0, 0, 0, 0, 0, 0, 0, 1 };
                break;
            case "17B":
                this.cardName = "Imperfections";
                this.usePhase = 5;
                this.cost = new int[8] { 0, 0, 0, 0, 0, 0, 0, 1 };
                break;
            case "18A":
                this.cardName = "Inflation";
                this.usePhase = 5;
                this.cost = new int[8] { 0, 0, 0, 0, 0, 0, 2, 0 };
                break;
            case "18B":
                this.cardName = "Stasis";
                this.usePhase = 5;
                this.cost = new int[8] { 0, 0, 0, 0, 0, 0, 2, 0 };
                break;
            case "19A":
                this.cardName = "Realignment";
                this.usePhase = 5;
                this.cost = new int[8] { 0, 0, 0, 0, 0, 0, 1, 1 };
                break;
            case "19B":
                this.cardName = "Siphon";
                this.usePhase = 5;
                this.cost = new int[8] { 0, 0, 0, 0, 0, 0, 1, 1 };
                break;
            case "20A":
                this.cardName = "Intensify";
                this.usePhase = 5;
                this.cost = new int[8] { 0, 0, 0, 0, 0, 0, 0, 2 };
                break;
            case "20B":
                this.cardName = "Negation";
                this.usePhase = 5;
                this.cost = new int[8] { 0, 0, 0, 0, 0, 0, 0, 2 };
                break;
            default:
                this.cardName = "Ambition";
                this.usePhase = 1;
                this.cost = new int[8] { 1, 0, 0, 0, 0, 0, 0, 0 };
                break;
        }
    }

    public virtual void activate()
    {
        switch (cardId)
        {
            case "1A":
                activeSpell = "Ambition2";
                break;
            case "1B":
                activeSpell = "Suspension";
                break;
            case "2A":
                activeSpell = "Initiative";
                break;
            case "2B":
                if (subPhase == 1)
                {
                    players[turn].activeSpell = new Card("2B");
                    nextTurn();
                }
                else
                {
                    activeSpell = "Backlash";
                }
                break;
            case "3A":
                if (subPhase == 1)
                {
                    players[turn].activeSpell = new Card("3A");
                    nextTurn();
                }
                else
                {
                    activeSpell = "Gateways";
                }
                break;
            case "3B":
                activeSpell = "Barrier";
                break;
            case "4A":
                players[turn].tunneling = true;
                nextTurn();
                break;
            case "4B":
                subsidence = true;
                nextTurn();
                break;
            case "5A":
                activeSpell = "Precedence";
                break;
            case "5B":
                for (int i = 0; i < PLAYERS; i++)
                {
                    if (i != turn)
                    {
                        players[i].miners -= 2;
                        if (players[i].miners < 0)
                        {
                            players[i].miners = 0;
                        }
                    }
                }
                nextTurn();
                break;
            case "6A":
                activeSpell = "Telepathy";
                telepathyFrom = -1;
                break;
            case "6B":
                activeSpell = "Teleport";
                break;
            case "7A":
                activeSpell = "Extraction2";
                break;
            case "7B":
                activeSpell = "Transfiguration";
                break;
            case "8A":
                activeSpell = "Telekinesis";
                break;
            case "8B":
                activeSpell = "Contagion";
                break;
            case "9A":
                activeSpell = "Advancement2";
                break;
            case "9B":
                activeSpell = "Plunder";
                plundered = new int[PLAYERS];
                for (int i = 0; i < PLAYERS; i++)
                {
                    if (i != turn)
                    {
                        plundered[i] = 1;
                    }
                    else
                    {
                        plundered[i] = 0;
                    }
                }
                break;
            case "10A":
                for (int i = 0; i < 8; i++)
                {
                    bool hasMiners = false;
                    for (int j = 0; j < 4; j++)
                    {
                        if (mines[i].sites[j][1] == turn && mines[i].sites[j][0] > 0)
                        {
                            hasMiners = true;
                        }
                    }
                    if (!hasMiners)
                    {
                        mines[i].pool -= 1;
                        if (mines[i].pool < 0)
                        {
                            mines[i].pool = 0;
                            players[turn].queue[0][i] -= 1;
                        }
                        players[turn].queue[0][i] += 1;
                    }
                }
                nextTurn();
                break;
            case "10B":
                altTurn = turn;
                turn++;
                if (turn == PLAYERS)
                {
                    turn = 0;
                }
                activeSpell = "Earthquake";
                break;
            case "11A":
                activeSpell = "Purge";
                for (int i = 1; i <= players[turn].hand.deck.Count; i++)
                {
                    spellCard(i, players[turn].hand.deck[i - 1].cardId);
                }
                break;
            case "11B":
                for (int i = 0; i < PLAYERS; i++)
                {
                    if (i != turn)
                    {
                        players[i].manaCount -= 3;
                        if (players[i].manaCount < 0)
                        {
                            players[i].manaCount = 0;
                        }
                    }
                }
                nextTurn();
                break;
            case "12A":
                activeSpell = "Transmission";
                break;
            case "12B":
                activeSpell = "Pilfer";
                break;
            case "13A":
                players[turn].manaCount += 10;
                players[turn].concentration = true;
                nextTurn();
                break;
            case "13B":
                altTurn = turn;
                turn++;
                if (turn == PLAYERS)
                {
                    turn = 0;
                }
                activeSpell = "Depletion";
                break;
            case "14A":
                activeSpell = "Transfusion2";
                break;
            case "14B":
                System.Random gen = new System.Random();
                for (int i = 0; i < PLAYERS; i++)
                {
                    if (i != turn && players[i].hand.deck.Count > 0)
                    {
                        players[i].hand.deck.RemoveAt(gen.Next(players[i].hand.deck.Count));
                        players[turn].manaCount += 2;
                    }
                }
                nextTurn();
                break;
            case "15A":
                activeSpell = "Munificence";
                break;
            case "15B":
                activeSpell = "Spellsteal";
                break;
            case "16A":
                players[turn].mutability = true;
                nextTurn();
                break;
            case "16B":
                activeSpell = "Overload";
                altTurn = turn;
                turn++;
                if (turn == PLAYERS)
                {
                    turn = 0;
                }
                break;
            case "17A":
                activeSpell = "Moratorium";
                break;
            case "17B":
                activeSpell = "Imperfections";
                break;
            case "18A":
                if (players[turn].manaCount >= 5)
                {
                    players[turn].manaCount -= 5;
                    activeSpell = "Inflation";
                }
                else
                {
                    nextTurn();
                }
                break;
            case "18B":
                activeSpell = "Stasis";
                break;
            case "19A":
                players[turn].realignment = true;
                nextTurn();
                break;
            case "19B":
                siphoning = true;
                players[turn].siphoned = true;
                nextTurn();
                break;
            case "20A":
                activeSpell = "Intensify";
                break;
            case "20B":
                for (int i = 0; i < PLAYERS; i++)
                {
                    if (i != turn)
                    {
                        players[i].negation++;
                    }
                }
                nextTurn();
                break;
            default:
                nextTurn();
                break;
        }
    }


}
