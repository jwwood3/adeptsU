using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static GameMaster;

public class MineMarker : MonoBehaviour
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
                GameObject.Find("mine" + i).GetComponent<TextMeshPro>().text = "" + mines[i].pool;
            }

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    /*print("/minerMarker" + i + "-" + j + "/Counter");
                    print(GameObject.Find("/minerMarker" + i + "-" + j + "/Counter"));
                    print(GameObject.Find("/minerMarker" + i + "-" + j + "/Counter").GetComponent<TextMeshPro>());*/
                    TextMeshPro counter = GameObject.Find("/minerMarker" + i + "-" + j + "/Counter").GetComponent<TextMeshPro>();
                    counter.text = "" + mines[i].sites[j][0];
                    int colorIndex = 0;
                    if (mines[i].sites[j][1] == -1)
                    {
                        colorIndex = 5;
                    }
                    else
                    {
                        colorIndex = mines[i].sites[j][1];
                    }
                    counter.color = new Color(pColors[colorIndex].x, pColors[colorIndex].y, pColors[colorIndex].z, 1);
                }
            }
            GameObject.Find("minersIndic").GetComponent<TextMeshProUGUI>().text = "Miners: " + players[turn].miners;
        }
    }
}
