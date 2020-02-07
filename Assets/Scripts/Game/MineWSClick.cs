using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameMaster;

public class MineWSClick : MonoBehaviour
{
    public int mineNum = 0;
    public int wsNum = 1;

    void OnMouseUp()
    {
        if(activeSpell == "Telepathy")
        {
            if (players[turn].miners == 0 && mines[mineNum].sites[wsNum][1]==turn)
            {
                players[turn].miners = mines[mineNum].sites[wsNum][0];
                mines[mineNum].sites[wsNum][0] = 0;
            }
            else if(players[turn].miners>=toAdd && mines[mineNum].sites[wsNum][1] == turn)
            {
                players[turn].miners -= toAdd;
                mines[mineNum].sites[wsNum][0] += toAdd;
                if (players[turn].miners == 0)
                {
                    activeSpell = "";
                    nextTurn();
                }
            }
        }
        else if(activeSpell == "Teleport")
        {
            if(mines[mineNum].sites[wsNum][1]!=turn && mines[mineNum].sites[wsNum][0] > 0)
            {
                mines[mineNum].sites[wsNum][0] -= 2;
                if (mines[mineNum].sites[wsNum][0] < 0)
                {
                    mines[mineNum].sites[wsNum][0] = 0;
                }
                activeSpell = "";
                nextTurn();
            }
        }
        else if(activeSpell == "Extraction2")
        {
            if(mines[mineNum].sites[wsNum][1]==turn && mines[mineNum].sites[wsNum][0] > 0)
            {
                mines[mineNum].pool--;
                mines[mineNum].sites[wsNum][0]--;
                if (mines[mineNum].pool < 0)
                {
                    mines[mineNum].pool = 0;
                    players[turn].queue[0][mineNum]--;
                }
                players[turn].queue[0][mineNum]++;
                activeSpell = "Extraction1";
            }
        }
        else if (activeSpell == "Extraction1")
        {
            if (mines[mineNum].sites[wsNum][1] == turn && mines[mineNum].sites[wsNum][0] > 0)
            {
                mines[mineNum].pool--;
                mines[mineNum].sites[wsNum][0]--;
                if (mines[mineNum].pool < 0)
                {
                    mines[mineNum].pool = 0;
                    players[turn].queue[0][mineNum]--;
                }
                players[turn].queue[0][mineNum]++;
                activeSpell = "";
                nextTurn();
            }
        }
        else if(activeSpell == "Earthquake")
        {
            if (mines[mineNum].sites[wsNum][1] == turn && mines[mineNum].sites[wsNum][0] > 0)
            {
                mines[mineNum].sites[wsNum][0] = 0;
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
        }
    }
}
