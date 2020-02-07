﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Play : MonoBehaviour
{
    public TMP_Dropdown playerDropdown;
    public TMP_Dropdown spellDropdown;
    public int PLAYERNUM = 2;
    public int spellLine = 0;
    void Start()
    {
        playerDropdown = GameObject.Find("PlayerNum").GetComponent<TMP_Dropdown>();
        spellDropdown = GameObject.Find("SpellSelect").GetComponent<TMP_Dropdown>();
        DontDestroyOnLoad(this);
    }

    public void UpdatePlayerNum()
    {
        PLAYERNUM = playerDropdown.value + 2;
    }

    public void UpdateSpellLine()
    {
        spellLine = spellDropdown.value;
    }

    public void load()
    {
        SceneManager.LoadSceneAsync("Main");
    }

}