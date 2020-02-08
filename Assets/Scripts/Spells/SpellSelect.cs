using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Spell;
using static GameMaster;

public class SpellSelect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CUSTOM = new int[20];
        activeSpell = "Setup";
        spellSelectLayout("1");
    }
}
