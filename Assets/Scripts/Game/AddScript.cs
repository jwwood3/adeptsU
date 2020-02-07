using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static GameMaster;

public class AddScript : MonoBehaviour
{
    public TMP_Dropdown options;

    void Start()
    {
        options = GetComponent<TMP_Dropdown>();
    }

    void Update()
    {
        if (activeSpell=="" && players[turn].miners < toAdd && options.value != 1)
        {
            options.value = 1;
        }
        if (activeSpell=="" && (options.value == 0 || toAdd>players[turn].miners))
        {
            options.value = 1;
        }
        else if(activeSpell == "Telepathy" && toAdd > players[turn].miners)
        {
            options.value = 0;
        }
    }

    public void changeAddNum()
    {
        if (options.value == 0)
        {
            toAdd = 1;
        }
        else if (options.value == 1)
        {
            toAdd = 2;
        }
        else if(options.value == 2)
        {
            toAdd = 3;
        }
        else if (options.value == 3)
        {
            toAdd = 4;
        }
        
        if (toAdd > players[turn].miners && options.value!=1)
        {
            options.value = 1;
        }
    }
}
