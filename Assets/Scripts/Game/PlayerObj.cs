using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObj
{
    public int manaCount = 0;
    public int[] battery = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };
    public Deck hand;
    public Vector3 color;
    public int[][] queue;
    public Card activeSpell = null;
    public bool tunneling = false;
    public int suspended = 0;
    public int negation = 0;
    public bool concentration = false;
    public bool mutability = false;
    public bool siphoned = false;
    public int[] advancement = new int[2] { -1, -1 };
    public int realigning = 0;
    public bool realignment = false;
    public int moratorium = -1;
    public int intensify = -1;
    public int miners = 10;

    public bool canCast(Card c)
    {
        int[] totals = new int[8] {battery[0]+queue[0][0]+queue[1][0]+queue[2][0],
            battery[1] + queue[0][1] + queue[1][1] + queue[2][1],
            battery[2] + queue[0][2] + queue[1][2] + queue[2][2],
            battery[3] + queue[0][3] + queue[1][3] + queue[2][3],
            battery[4] + queue[0][4] + queue[1][4] + queue[2][4],
            battery[5] + queue[0][5] + queue[1][5] + queue[2][5],
            battery[6] + queue[0][6] + queue[1][6] + queue[2][6],
            battery[7] + queue[0][7] + queue[1][7] + queue[2][7]};
        return totals[0] >= c.cost[0] &&
            totals[1] >= c.cost[1] &&
            totals[2] >= c.cost[2] &&
            totals[3] >= c.cost[3] &&
            totals[4] >= c.cost[4] &&
            totals[5] >= c.cost[5] &&
            totals[6] >= c.cost[6] &&
            totals[7] >= c.cost[7];
    }
}
