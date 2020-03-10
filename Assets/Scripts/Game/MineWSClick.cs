using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameMaster;

public class MineWSClick : MonoBehaviour
{
    public int mineNum = 0;
    public int wsNum = 1;
    public int[] minerVals;
    static MeshRenderer mr;
    static Material[] mats;

    void Start()
    {
        print("starting: " + mineNum + ":" + wsNum);
        minerVals = mines[mineNum].sites[wsNum];
    }

    void Update()
    {
        if (minerVals != mines[mineNum].sites[wsNum])
        {
            minerVals = mines[mineNum].sites[wsNum];
            changeMiners();
        }
    }

    void changeMiners()
    {
        if (minerVals[0] > 16)
        {
            print("BBBBBAAAAAADDDDD: Mine - " + mineNum + " WS - " + wsNum + " has " + minerVals[0]);
        }
        for(int i = 0; i < minerVals[0]; i++)
        {
            mr = GameObject.Find("minerMarker" + mineNum + "-" + wsNum + "/Miner (" + i + ")").GetComponent<MeshRenderer>();
            mats = mr.materials;
            switch (minerVals[1])
            {
                case -1:
                    mats[0] = MASTER.clearThing;
                    break;
                case 0:
                    mats[0] = MASTER.p1P1;
                    break;
                case 1:
                    mats[0] = MASTER.p2P1;
                    break;
                case 2:
                    mats[0] = MASTER.p3P1;
                    break;
                case 3:
                    mats[0] = MASTER.p4P1;
                    break;
                case 4:
                    mats[0] = MASTER.p5P1;
                    break;
                default:
                    mats[0] = MASTER.p4P3;
                    break;
            }
            mr.materials = mats;
        }
        for(int i = minerVals[0]; i < 16; i++)
        {
            mr = GameObject.Find("minerMarker" + mineNum + "-" + wsNum + "/Miner (" + i + ")").GetComponent<MeshRenderer>();
            mats = mr.materials;
            mats[0] = MASTER.clearThing;
            mr.materials = mats;
        }
    }

    void OnMouseUp()
    {
        if (activeSpell == "Telepathy")
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
