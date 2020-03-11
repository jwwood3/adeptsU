using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameMaster;

public class Mine
{
    public int pool = 0;
    public int[][] sites = new int[4][] { new int[2] { 0, -1 }, new int[2] { 0, -1 }, new int[2] { 0, -1 }, new int[2] { 0, -1 } };
    public bool isBoosted = false;
    public int closed = -1;

    public Mine(int poolNum)
    {
        this.pool = poolNum;
    }

    public bool isOpen(int turnNum)
    {
        return (closed==-1 || closed==turnNum) && (this.sites[1][0] == 0 || this.sites[2][0] == 0 || this.sites[3][0] == 0);
    }

    public bool isEmpty()
    {
        return (this.sites[1][0] == 0 && this.sites[2][0] == 0 && this.sites[3][0] == 0);
    }

    public bool isOpenSpec()
    {
        return this.sites[0][0] == 0;
    }

    public void place(int miners, int player)
    {
        if (this.sites[1][0] == 0)
        {
            this.sites[1] = new int[2] { miners, player };
        }
        else if (this.sites[2][0] == 0)
        {
            this.sites[2] = new int[2] { miners, player };
        }
        else if(this.sites[3][0] == 0)
        {
            this.sites[3] = new int[2] { miners, player };
        }
        players[player].miners -= miners;
    }

    public void placeSpec(int miners, int player)
    {
        this.sites[0] = new int[2] { miners, player };
        players[player].miners -= miners;
    }
}
