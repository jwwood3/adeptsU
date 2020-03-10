using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static GameMaster;

public class MineMarker : MonoBehaviour
{
    static MeshRenderer mr;
    static Material[] newMats;

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

            /*for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (mines[i].sites[j][0] > 16)
                    {
                        print("BBBBBAAAAAADDDDD: Mine - " + i + " WS - " + j + " has " + mines[i].sites[j][0]);
                    }
                    for(int k = 0; k < mines[i].sites[j][0]; k++)
                    {
                        MeshRenderer mr = GameObject.Find("minerMarker" + i + "-" + j + "/Miner (" + k + ")").GetComponent<MeshRenderer>();
                        Material[] newMats = mr.materials;
                        print("Mine - " + i + " Ws - " + j + " has " + mines[i].sites[j][0] + " of p - " + mines[i].sites[j][1]);
                        switch (mines[i].sites[j][1])
                        {
                            case -1:
                                print("clear");
                                newMats[0] = MASTER.clearThing;
                                break;
                            case 0:
                                print("p1red");
                                newMats[0] = MASTER.p1P1;
                                break;
                            case 1:
                                print("p2blue");
                                newMats[0] = MASTER.p2P1;
                                break;
                            case 2:
                                print("p3green");
                                newMats[0] = MASTER.p3P1;
                                break;
                            case 3:
                                print("p4purple");
                                newMats[0] = MASTER.p4P1;
                                break;
                            case 4:
                                print("p5orange");
                                newMats[0] = MASTER.p5P1;
                                break;
                            default:
                                print("badColor");
                                newMats[0] = MASTER.p4P3;
                                break;
                        }
                        mr.materials = newMats;
                    }
                    for(int k = mines[i].sites[j][0]; k < 16; k++)
                    {
                        mr = GameObject.Find("minerMarker" + i + "-" + j + "/Miner (" + k + ")").GetComponent<MeshRenderer>();
                        newMats = mr.materials;
                        newMats[0] = MASTER.clearThing;
                        mr.materials = newMats;
                    }
                }
            }*/
            GameObject.Find("minersIndic").GetComponent<TextMeshProUGUI>().text = "Miners: " + players[turn].miners;
        }
    }
}
