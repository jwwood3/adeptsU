using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameMaster;

public class BatteryClick : MonoBehaviour
{
    public int gemType = 0;
    void OnMouseUp()
    {
        if(activeSpell == "Casting" && players[turn].battery[gemType]>0 && toCast.cost[gemType]>0)
        {
            players[turn].battery[gemType]--;
            toCast.cost[gemType]--;
            if(toCast.cost[0]+toCast.cost[1]+toCast.cost[2]+toCast.cost[3]+ toCast.cost[4] + toCast.cost[5] + toCast.cost[6] + toCast.cost[7]==0)
            {
                activeSpell = "";
                toCast.activate();
                toCast = null;
            }
        }
    }
}
