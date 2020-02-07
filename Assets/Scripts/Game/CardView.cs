using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameMaster;
using static Spell;

public class CardView : MonoBehaviour
{
    public GameObject topazBonus;
    public GameObject rubyBonus;
    public GameObject citrineBonus;
    public GameObject emeraldBonus;
    public GameObject amethystBonus;
    public GameObject sapphireBonus;
    public GameObject onyxBonus;
    public GameObject diamondBonus;
    public static GameObject[] bonusesAr;

    void Start()
    {
        bonusesAr = new GameObject[8] {topazBonus, rubyBonus, citrineBonus, emeraldBonus, amethystBonus, sapphireBonus, onyxBonus, diamondBonus};
    }

    public static void updateSpells()
    {
        GameObject[] dispCards = GameObject.FindGameObjectsWithTag("DispCard");
        for(int i = 0; i < dispCards.Length; i++)
        {
            GameObject.Destroy(dispCards[i]);
        }
        for (int i = 0; i < PLAYERS; i++)
        {
            for(int j = 0; j < players[i].hand.deck.Count; j++)
            {
                dispSpellCard(j+1, i, players[i].hand.deck[j].cardId);
            }
        }
    }

    public static void updateAdepts()
    {
        for(int i = 0; i < PLAYERS; i++)
        {
            int bonuses = 0;
            for(int j = 0; j < 8; j++)
            {
                if (adeptBonuses[j] == i)
                {
                    bonusesAr[j].GetComponent<RectTransform>().anchorMin = new Vector2(0.43f + (bonuses * 0.07f), 0.77f - (0.18f * i));
                    bonusesAr[j].GetComponent<RectTransform>().anchorMax = new Vector2(0.49f + (bonuses * 0.07f), 0.94f - (0.18f * i));
                    bonuses++;
                }
            }
        }
        for(int i = 0; i < 8; i++)
        {
            if (adeptBonuses[i] == -1)
            {
                bonusesAr[i].GetComponent<RectTransform>().anchorMin = new Vector2(1f, 1f);
                bonusesAr[i].GetComponent<RectTransform>().anchorMax = new Vector2(1f, 1f);
            }
        }
    }

    public static void updateDiscard()
    {
        GameObject[] disCards = GameObject.FindGameObjectsWithTag("DisCard");
        for (int i = 0; i < disCards.Length; i++)
        {
            GameObject.Destroy(disCards[i]);
        }
        for (int i = 0; i < discard.deck.Count; i++)
        {
            discardSpellCard(i, discard.deck[i].cardId);
        }
    }

    void Update()
    {

    }
}
