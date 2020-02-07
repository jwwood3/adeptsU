using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class turnIndicator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<TextMeshProUGUI>().text = "Player " + (GameMaster.turn + 1);
    }
}
