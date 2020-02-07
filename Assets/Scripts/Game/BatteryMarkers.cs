using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameMaster;

public class BatteryMarkers : MonoBehaviour
{
    public GameObject p1Indic;
    public GameObject p2Indic;
    public GameObject p3Indic;
    public GameObject p4Indic;
    public GameObject p5Indic;
    GameObject[] diamondIndic;
    GameObject[] onyxIndic;
    GameObject[] sapphireIndic;
    GameObject[] amethystIndic;
    GameObject[] emeraldIndic;
    GameObject[] citrineIndic;
    GameObject[] rubyIndic;
    GameObject[] topazIndic;
    GameObject[][] batteryMarkers;
    // Start is called before the first frame update

    int getBatteryIndex(int gemIndex)
    {
        switch (gemIndex)
        {
            case 0:
                return 6;
            case 1:
                return 2;
            case 2:
                return 4;
            case 3:
                return 1;
            case 4:
                return 5;
            case 5:
                return 3;
            case 6:
                return 7;
            case 7:
                return 0;
            default:
                return 0;
        }
    }

    void Start()
    {
        diamondIndic = new GameObject[PLAYERS];
        onyxIndic = new GameObject[PLAYERS];
        sapphireIndic = new GameObject[PLAYERS];
        amethystIndic = new GameObject[PLAYERS];
        emeraldIndic = new GameObject[PLAYERS];
        citrineIndic = new GameObject[PLAYERS];
        rubyIndic = new GameObject[PLAYERS];
        topazIndic = new GameObject[PLAYERS];
        batteryMarkers = new GameObject[8][] {topazIndic, rubyIndic, citrineIndic, emeraldIndic, amethystIndic, sapphireIndic, onyxIndic, diamondIndic};
        print("PLAYERS=" + PLAYERS);
        for (int i = 0; i < 8; i++)
        {
            for(int j = 0; j < PLAYERS; j++)
            {
                switch (j)
                {
                    case 0:
                        batteryMarkers[i][j] = Instantiate(p1Indic, new Vector3((-8.73f + ((0.1f) * j) + (0.715f * getBatteryIndex(i))), -1.51f, 5.0f), Quaternion.identity);
                        break;
                    case 1:
                        batteryMarkers[i][j] = Instantiate(p2Indic, new Vector3((-8.73f + ((0.1f) * j) + (0.715f * getBatteryIndex(i))), -1.51f, 5.0f), Quaternion.identity);
                        break;
                    case 2:
                        batteryMarkers[i][j] = Instantiate(p3Indic, new Vector3((-8.73f + ((0.1f) * j) + (0.715f * getBatteryIndex(i))), -1.51f, 5.0f), Quaternion.identity);
                        break;
                    case 3:
                        batteryMarkers[i][j] = Instantiate(p4Indic, new Vector3((-8.73f + ((0.1f) * j) + (0.715f * getBatteryIndex(i))), -1.51f, 5.0f), Quaternion.identity);
                        break;
                    case 4:
                        batteryMarkers[i][j] = Instantiate(p5Indic, new Vector3((-8.73f + ((0.1f) * j) + (0.715f * getBatteryIndex(i))), -1.51f, 5.0f), Quaternion.identity);
                        break;
                    default:
                        batteryMarkers[i][j] = Instantiate(p1Indic, new Vector3((-8.73f + ((0.1f) * j) + (0.715f * getBatteryIndex(i))), -1.51f, 5.0f), Quaternion.identity);
                        break;
                }
                
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < 8; i++)
        {
            for(int j = 0; j < PLAYERS; j++)
            {
                batteryMarkers[i][j].transform.position = new Vector3((-8.73f + ((0.1f) * j) + (0.715f * getBatteryIndex(i))), -1.51f+(0.711f*players[j].battery[i]), 5.0f);
            }
        }
    }
}
