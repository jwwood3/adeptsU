using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameMaster;

public class Spell : MonoBehaviour
{

    public static Spell spellList;
    public GameObject prefab1A;
    public GameObject prefab1B;
    public GameObject prefab2A;
    public GameObject prefab2B;
    public GameObject prefab3A;
    public GameObject prefab3B;
    public GameObject prefab4A;
    public GameObject prefab4B;
    public GameObject prefab5A;
    public GameObject prefab5B;
    public GameObject prefab6A;
    public GameObject prefab6B;
    public GameObject prefab7A;
    public GameObject prefab7B;
    public GameObject prefab8A;
    public GameObject prefab8B;
    public GameObject prefab9A;
    public GameObject prefab9B;
    public GameObject prefab10A;
    public GameObject prefab10B;
    public GameObject prefab11A;
    public GameObject prefab11B;
    public GameObject prefab12A;
    public GameObject prefab12B;
    public GameObject prefab13A;
    public GameObject prefab13B;
    public GameObject prefab14A;
    public GameObject prefab14B;
    public GameObject prefab15A;
    public GameObject prefab15B;
    public GameObject prefab16A;
    public GameObject prefab16B;
    public GameObject prefab17A;
    public GameObject prefab17B;
    public GameObject prefab18A;
    public GameObject prefab18B;
    public GameObject prefab19A;
    public GameObject prefab19B;
    public GameObject prefab20A;
    public GameObject prefab20B;

    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public static GameObject getSpellCard(string spellId)
    {
        switch (spellId)
        {
            case "1A":
                return spellList.prefab1A;
            case "1B":
                return spellList.prefab1B;
            case "2A":
                return spellList.prefab2A;
            case "2B":
                return spellList.prefab2B;
            case "3A":
                return spellList.prefab3A;
            case "3B":
                return spellList.prefab3B;
            case "4A":
                return spellList.prefab4A;
            case "4B":
                return spellList.prefab4B;
            case "5A":
                return spellList.prefab5A;
            case "5B":
                return spellList.prefab5B;
            case "6A":
                return spellList.prefab6A;
            case "6B":
                return spellList.prefab6B;
            case "7A":
                return spellList.prefab7A;
            case "7B":
                return spellList.prefab7B;
            case "8A":
                return spellList.prefab8A;
            case "8B":
                return spellList.prefab8B;
            case "9A":
                return spellList.prefab9A;
            case "9B":
                return spellList.prefab9B;
            case "10A":
                return spellList.prefab10A;
            case "10B":
                return spellList.prefab10B;
            case "11A":
                return spellList.prefab11A;
            case "11B":
                return spellList.prefab11B;
            case "12A":
                return spellList.prefab12A;
            case "12B":
                return spellList.prefab12B;
            case "13A":
                return spellList.prefab13A;
            case "13B":
                return spellList.prefab13B;
            case "14A":
                return spellList.prefab14A;
            case "14B":
                return spellList.prefab14B;
            case "15A":
                return spellList.prefab15A;
            case "15B":
                return spellList.prefab15B;
            case "16A":
                return spellList.prefab16A;
            case "16B":
                return spellList.prefab16B;
            case "17A":
                return spellList.prefab17A;
            case "17B":
                return spellList.prefab17B;
            case "18A":
                return spellList.prefab18A;
            case "18B":
                return spellList.prefab18B;
            case "19A":
                return spellList.prefab19A;
            case "19B":
                return spellList.prefab19B;
            case "20A":
                return spellList.prefab20A;
            case "20B":
                return spellList.prefab20B;
            default:
                return spellList.prefab2A;
        }
    }
    public static Card getSpellObj(string spellId)
    {
        return new Card(spellId);
    }
    public static void spellCard(int num, string spellId)
    {
        Vector3 tran = new Vector3();
        if (num == 5)
        {
            tran = new Vector3(-3.5f,1.8f,-6f);
        }
        else if (num == 3)
        {
            tran = new Vector3(-1.8f, 1.8f, -6f);
        }
        else if (num == 1)
        {
            tran = new Vector3(0f, 1.8f, -6f);
        }
        else if (num == 2)
        {
            tran = new Vector3(1.8f, 1.8f, -6f);
        }
        else if (num == 4)
        {
            tran = new Vector3(3.5f, 1.8f, -6f);
        }
        GameObject newCard = Instantiate(getSpellCard(spellId), tran, Quaternion.identity);
    }

    public static void dispSpellCard(int num, int player, string spellId)
    {
        Vector3 tran = new Vector3();
        if (num == 5)
        {
            tran = new Vector3(-10.5f, 506.5f - (player * 2.25f), 1f);
        }
        else if (num == 3)
        {
            tran = new Vector3(-8.9f, 506.5f - (player * 2.25f), 1f);
        }
        else if (num == 1)
        {
            tran = new Vector3(-7.3f, 506.5f - (player * 2.25f), 1f);
        }
        else if (num == 2)
        {
            tran = new Vector3(-5.7f, 506.5f - (player * 2.25f), 1f);
        }
        else if (num == 4)
        {
            tran = new Vector3(-4.1f, 506.5f - (player * 2.25f), 1f);
        }
        GameObject newCard = Instantiate(getSpellCard(spellId), tran, Quaternion.identity);
        if (player != turn)
        {
            newCard.transform.Rotate(0f,180f,0f);
        }
        newCard.tag = "DispCard";
    }

    public static void discardSpellCard(int num, string spellId)
    {
        Vector3 tran = new Vector3();
        tran = new Vector3(-510.5f+(1.9f*(num%12)), 7f - (2.5f*(num / 12)), 1f);
        GameObject newCard = Instantiate(getSpellCard(spellId), tran, Quaternion.identity);
        if(activeSpell != "Munificence" && num!=0)
        {
            newCard.transform.Rotate(0f, 180f, 0f);
        }
        newCard.tag = "DisCard";
    }

    public static void activeSpellCard(string spellId)
    {
        Vector3 tran = new Vector3(8.25f, 6.2f, 0f);
        GameObject newCard = Instantiate(getSpellCard(spellId), tran, Quaternion.identity);
        newCard.tag = "ActiveCard";
    }

    public static void spellSelectLayout(string idStub)
    {
        Instantiate(getSpellCard(idStub + "A"), new Vector3(-1f, 1.5f, -6f), Quaternion.identity);
        Instantiate(getSpellCard(idStub + "B"), new Vector3(1f, 1.5f, -6f), Quaternion.identity);
    }

    void Awake()
    {
        spellList = this;
    }
}
