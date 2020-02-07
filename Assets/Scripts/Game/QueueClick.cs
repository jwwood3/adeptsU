using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static GameMaster;

public class QueueClick : MonoBehaviour, IPointerClickHandler
{
    public int queueNum = 0;
    public int gemType = 0;
    public int playerNum = 0;
    bool followTurn = false;

    void Start()
    {
        if(playerNum == 6)
        {
            followTurn = true;
        }
    }

    void Update()
    {
        if (followTurn)
        {
            playerNum = turn;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        print(activeSpell + ": " + queueNum + " " + gemType + " " + playerNum);
        Debug.Log("adwad");
        if (activeSpell == "Casting" && players[turn].queue[queueNum][gemType]>0 && toCast.cost[gemType] > 0)
        {
            toCast.cost[gemType]--;
            players[turn].queue[queueNum][gemType]--;
            mines[gemType].pool++;
            if (toCast.cost[0] + toCast.cost[1] + toCast.cost[2] + toCast.cost[3] + toCast.cost[4] + toCast.cost[5] + toCast.cost[6] + toCast.cost[7] == 0)
            {
                activeSpell = "";
                toCast.activate();
                toCast = null;
            }
        }
        else if(activeSpell == "Plunder" && playerNum != turn && plundered[playerNum] == 1 && players[playerNum].queue[queueNum][gemType]>0)
        {
            players[playerNum].queue[queueNum][gemType]--;
            players[turn].queue[queueNum][gemType]++;
            plundered[playerNum] = 0;
            int plunderedSum = 0;
            for(int i = 0; i < PLAYERS; i++)
            {
                plunderedSum += plundered[i];
            }
            if (plunderedSum == 0)
            {
                activeSpell = "";
                nextTurn();
            }
        }
        else if(activeSpell == "Transmission" && players[turn].queue[queueNum][gemType] > 0)
        {
            players[turn].queue[queueNum][gemType]--;
            players[turn].battery[gemType] += 3;
            if (players[turn].battery[gemType] > 10)
            {
                players[turn].battery[gemType] = 10;
            }
            activeSpell = "";
            nextTurn();
        }
        else if(activeSpell == "Transfiguration" && transfiguration[0]==-1 && players[turn].queue[queueNum][gemType]>0)
        {
            if (playerNum == turn)
            {
                transfiguration[0] = queueNum;
                transfiguration[1] = gemType;
            }
        }
        else if(activeSpell == "Transfiguration" && transfiguration[0] != -1 && players[playerNum].queue[queueNum][gemType]>0)
        {
            if (playerNum != turn)
            {
                players[playerNum].queue[queueNum][gemType] -= 2;
                if (players[playerNum].queue[queueNum][gemType] < 0)
                {
                    players[turn].queue[queueNum][gemType] += players[playerNum].queue[queueNum][gemType];
                    players[playerNum].queue[queueNum][gemType] = 0;
                }
                players[turn].queue[queueNum][gemType] += 2;
                players[turn].queue[transfiguration[0]][transfiguration[1]]--;
                players[playerNum].queue[transfiguration[0]][transfiguration[1]]++;
                activeSpell = "";
                transfiguration = new int[2] { -1, -1 };
                nextTurn();
            }
        }
        else if(activeSpell == "Advancement1")
        {
            players[turn].advancement[1] = queueNum;
            activeSpell = "";
            nextTurn();
        }
        else if(activeSpell == "Transfusion2" && playerNum == turn)
        {
            transfusion[0] = queueNum;
            transfusion[1] = gemType;
            activeSpell = "Transfusion1";
        }
        else if(activeSpell == "Overload" && playerNum == turn && queueNum == 2 && players[playerNum].queue[queueNum][gemType] > 0)
        {
            players[playerNum].queue[queueNum][gemType]--;
            mines[gemType].pool++;
            turn++;
            if (turn == PLAYERS)
            {
                turn = 0;
            }
            if(turn == altTurn)
            {
                altTurn = -1;
                activeSpell = "";
                nextTurn();
            }
        }
    }
}
