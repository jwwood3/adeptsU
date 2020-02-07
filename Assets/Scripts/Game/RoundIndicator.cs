using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameMaster;

public class RoundIndicator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = getRoundMarkerPos(round);
    }

    Vector3 getRoundMarkerPos(int r)
    {
        float xPos = -8.51f;
        for(int i = 1; i < r; i++)
        {
            xPos += 0.715f;
        }
        return new Vector3(xPos, -3.59f, 5.0f);
    }
}
