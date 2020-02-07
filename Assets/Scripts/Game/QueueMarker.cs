using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static GameMaster;

public class QueueMarker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mode == 1)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 1; j < 4; j++)
                {
                    GameObject.Find("TurnVals/queue" + i + "-" + j).GetComponent<TextMeshProUGUI>().text = "" + players[turn].queue[j - 1][i];
                }
            }
        }
        if (mode==2)
        {
            for (int k = 1; k <= PLAYERS; k++)
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 1; j < 4; j++)
                    {
                        GameObject.Find("P" + k + "Vals/queue" + i + "-" + j).GetComponent<TextMeshProUGUI>().text = "" + players[k - 1].queue[j - 1][i];
                    }
                }
            }
        }
    }
}
