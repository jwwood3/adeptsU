using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using static GameMaster;

public class Mineclick : MonoBehaviour
{
    public int mineNum = -1;


    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    void OnMouseUp()
    {
        print("phase: " + phase + " sub: " + subPhase + " ");
        print(mineNum);
        print(mines.Length);
        print(toAdd);
        print(mines[mineNum].isOpen());
        print("turn: " + turn + " miners: " + players[turn].miners);
        //GameMaster.clickedMine = mineNum;
        if (phase == 1 && subPhase == 2 && mines[mineNum].isOpenSpec() && adeptBonuses[0] != -1 && players[adeptBonuses[0]].miners >= toAdd)
        {
            mines[mineNum].placeSpec(toAdd, adeptBonuses[0]);
            subPhase = 3;
            turn = first;
        }
        else if (phase == 1 && subPhase == 3 && mines[mineNum].isOpen() && players[turn].miners >= toAdd && activeSpell == "")
        {
            if (!(subsidence && mines[mineNum].isEmpty() && toAdd > 2))
            {
                mines[mineNum].place(toAdd, turn);
                if (players[turn].tunneling)
                {
                    if (mines[mineNum].pool >= 1)
                    {
                        mines[mineNum].pool -= 1;
                        players[turn].queue[0][mineNum] += 1;
                    }
                }
                nextTurn();
            }
        }
        else if (phase == 2 && subPhase == 2)
        {
            mines[mineNum].isBoosted = true;
            subPhase = 3;
            turn = first;
            phase2();
        }
        else if (phase == 5 && subPhase == 3)
        {
            gemBoosted = mineNum;
            turn = first;
            subPhase = 4;
            altTurn = turn;
            siphonBat = new int[8] { turn, turn, turn, turn, turn, turn, turn, turn };
            if (siphoning)
            {
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
                        if (i != altTurn && players[i].siphoned)
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
            else
            {
                advBat();
            }
        }
        else if (activeSpell == "Ambition2")
        {
            if (mines[mineNum].isOpen() && players[turn].miners >= 1)
            {
                mines[mineNum].place(1, turn);
            }
            activeSpell = "Ambition1";
        }
        else if (activeSpell == "Ambition1" && players[turn].miners >= 1)
        {
            if (mines[mineNum].isOpen())
            {
                mines[mineNum].place(1, turn);
            }
            activeSpell = "";
            nextTurn();
        }
        else if (activeSpell == "Initiative" && players[turn].miners >= toAdd)
        {
            if (mines[mineNum].isOpen())
            {
                mines[mineNum].place(toAdd, turn);
            }
            activeSpell = "";
            nextTurn();
        }
        else if (activeSpell == "Backlash" && mines[mineNum].isOpen())
        {
            if (mines[mineNum].sites[3][0] == 0)
            {
                mines[mineNum].sites[3] = mines[mineNum].sites[2];
                mines[mineNum].sites[2] = mines[mineNum].sites[1];
                mines[mineNum].sites[1] = new int[2] { 0, -1 };
                mines[mineNum].place(toAdd, turn);
            }
            else if (mines[mineNum].sites[2][0] == 0)
            {
                mines[mineNum].sites[2] = mines[mineNum].sites[1];
                mines[mineNum].sites[1] = new int[2] { 0, -1 };
                mines[mineNum].place(toAdd, turn);
            }
            else
            {
                mines[mineNum].place(toAdd, turn);
            }
            activeSpell = "";
            nextTurn();
            
        }
        else if (activeSpell == "Barrier")
        {
            mines[mineNum].closed = true;
            activeSpell = "";
            nextTurn();
        }
        else if (activeSpell == "Gateways" && mines[mineNum].isOpen())
        {
            mines[mineNum].place(toAdd, turn);
            activeSpell = "";
        }
        else if (activeSpell == "Precedence" && mines[mineNum].isOpen())
        {
            mines[mineNum].place(toAdd, turn);
            if (players[turn].miners <= 1)
            {
                activeSpell = "";
                nextTurn();
            }
        }
        else if (activeSpell == "Telepathy" && players[turn].miners == 0)
        {
            for (int i = 3; i > 0; i--)
            {
                if (mines[mineNum].sites[i][1] == turn && mines[mineNum].sites[i][0] > 0)
                {
                    players[turn].miners = mines[mineNum].sites[i][0];
                    mines[mineNum].sites[i][0] = 0;
                    break;
                }
            }
        }
        else if (activeSpell == "Telepathy" && players[turn].miners >= toAdd)
        {
            for (int i = 1; i < 4; i++)
            {
                if (mines[mineNum].sites[i][1] == turn && mines[mineNum].sites[i][0] > 0)
                {
                    mines[mineNum].sites[i][0] += toAdd;
                    players[turn].miners -= toAdd;
                    break;
                }
            }
            if (players[turn].miners == 0)
            {
                activeSpell = "";
                nextTurn();
            }
        }
        else if (activeSpell == "Teleport" && !mines[mineNum].isEmpty())
        {
            for (int i = 1; i < 4; i++)
            {
                if (mines[mineNum].sites[i][1] != turn && mines[mineNum].sites[i][1] != -1 && mines[mineNum].sites[i][0] > 0)
                {
                    mines[mineNum].sites[i][0] -= 2;
                    if (mines[mineNum].sites[i][0] < 0)
                    {
                        mines[mineNum].sites[i][0] = 0;
                    }
                    activeSpell = "";
                    nextTurn();
                    break;
                }
            }
        }
        else if (activeSpell == "Extraction2")
        {
            for (int i = 3; i > 0; i--)
            {
                if (mines[mineNum].sites[i][1] == turn && mines[mineNum].sites[i][0] > 0)
                {
                    mines[mineNum].sites[i][0] -= 1;
                    mines[mineNum].pool -= 1;
                    if (mines[mineNum].pool < 0)
                    {
                        mines[mineNum].pool = 0;
                        players[turn].queue[0][mineNum] -= 1;
                    }
                    players[turn].queue[0][mineNum] += 1;
                    activeSpell = "Extraction1";
                    break;
                }
            }
        }
        else if (activeSpell == "Extraction1")
        {
            for (int i = 3; i > 0; i--)
            {
                if (mines[mineNum].sites[i][1] == turn && mines[mineNum].sites[i][0] > 0)
                {
                    mines[mineNum].sites[i][0] -= 1;
                    mines[mineNum].pool -= 1;
                    if (mines[mineNum].pool < 0)
                    {
                        mines[mineNum].pool = 0;
                        players[turn].queue[0][mineNum] -= 1;
                    }
                    players[turn].queue[0][mineNum] += 1;
                    activeSpell = "";
                    nextTurn();
                    break;
                }
            }
        }
        else if (activeSpell == "Telekinesis")
        {
            mines[mineNum].pool -= 2;
            if (mines[mineNum].pool < 0)
            {
                players[turn].queue[0][mineNum] += mines[mineNum].pool;
                mines[mineNum].pool = 0;
            }
            players[turn].queue[0][mineNum] += 2;
            activeSpell = "";
            nextTurn();
        }
        else if (activeSpell == "Contagion")
        {
            for (int i = 1; i < 4; i++)
            {
                mines[mineNum].sites[i][0] -= 1;
                if (mines[mineNum].sites[i][0] < 0)
                {
                    mines[mineNum].sites[i][0] = 0;
                }
            }
            activeSpell = "";
            nextTurn();
        }
        else if (activeSpell == "Earthquake")
        {
            for (int i = 3; i > 0; i--)
            {
                if (mines[mineNum].sites[i][1] == turn && mines[mineNum].sites[i][0] > 0)
                {
                    mines[mineNum].sites[i][0] = 0;
                    turn++;
                    if (turn == PLAYERS)
                    {
                        turn = 0;
                    }
                    if (turn == altTurn)
                    {
                        altTurn = -1;
                        activeSpell = "";
                        nextTurn();
                    }
                    break;
                }
            }
        }
        else if (activeSpell == "Depletion")
        {
            players[turn].battery[mineNum] -= 2;
            if (players[turn].battery[mineNum] < 0)
            {
                players[turn].battery[mineNum] = 0;
            }
            turn++;
            if (turn == PLAYERS)
            {
                turn = 0;
            }
            if (turn == altTurn)
            {
                altTurn = -1;
                activeSpell = "";
                nextTurn();
            }
        }
        else if(activeSpell == "Moratorium")
        {
            players[turn].moratorium = mineNum;
            activeSpell = "";
            nextTurn();
        }
        else if (activeSpell == "Imperfections")
        {
            imperfection[mineNum]++;
            activeSpell = "";
            nextTurn();
        }
        else if(activeSpell == "Inflation")
        {
            players[turn].battery[mineNum] += 5;
            if (players[turn].battery[mineNum] > 10)
            {
                players[turn].battery[mineNum] = 10;
            }
            activeSpell = "";
            nextTurn();
        }
        else if(activeSpell == "Stasis")
        {
            stasis = mineNum;
            activeSpell = "";
            nextTurn();
        }
        else if(activeSpell == "Intensify")
        {
            players[turn].intensify = mineNum % 2;
            activeSpell = "";
            nextTurn();
        }
        else if(activeSpell == "Siphon")
        {
            if (siphonBat[mineNum] == altTurn)
            {
                siphonBat[mineNum] = turn;
                turn++;
                if (turn == PLAYERS)
                {
                    turn = 0;
                }
                while (!players[turn].siphoned)
                {
                    if (turn == altTurn)
                    {
                        break;
                    }
                    turn++;
                    if (turn == PLAYERS)
                    {
                        turn = 0;
                    }
                }
                if (turn == altTurn)
                {
                    activeSpell = "";
                    altTurn = -1;
                    advBat();
                }
            }
        }
        else if(activeSpell == "Advancement2")
        {
            players[turn].advancement[0] = mineNum;
            activeSpell = "Advancement1";
        }
        else if(activeSpell == "Realignment")
        {
            if(!Array.Exists(realignBat, element => element == mineNum))
            {
                realignBat[players[turn].realigning] = mineNum;
                players[turn].realigning++;
                if (players[turn].realigning == 8)
                {
                    activeSpell = "";
                    players[turn].realigning = 0;
                    players[turn].realignment = false;
                    advBat();
                }
            }
        }
            
        
    }
}
