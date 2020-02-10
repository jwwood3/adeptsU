using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameMaster;
using static Spell;

public class SpellCase : MonoBehaviour
{

    public string spellId;
    public Card spellcard;
    // Start is called before the first frame update
    void Start()
    {
        spellcard = new Card(spellId);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseUp()
    {
        if (this.tag == "Card")
        {
            if(activeSpell == "Setup")
            {
                int currentNum = Int32.Parse(spellcard.cardId.Substring(0, spellcard.cardId.Length - 1));
                switch (spellcard.cardId[spellcard.cardId.Length - 1])
                {
                    case 'A':
                        CUSTOM[currentNum - 1] = 1;
                        break;
                    case 'B':
                        CUSTOM[currentNum - 1] = 2;
                        break;
                    default:
                        CUSTOM[currentNum - 1] = 1;
                        break;
                }
                GameObject[] cardObjs = GameObject.FindGameObjectsWithTag("Card");
                for (int i = 0; i < cardObjs.Length; i++)
                {
                    if (cardObjs[i] != this)
                    {
                        Destroy(cardObjs[i]);
                    }
                }
                if (currentNum != 20)
                {
                    spellSelectLayout("" + (currentNum + 1));
                }
                else
                {
                    activeSpell = "";
                    SceneManager.LoadSceneAsync("Main");
                }
                Destroy(this);
                
            }
            else if (activeSpell == "Purge")
            {
                print("The game thinks you're discarding a spell");
                this.gameObject.transform.localScale = new Vector3(0, 0, 0);
                players[turn].hand.removeCard(spellcard.cardId);
                discard.add(new Card(spellcard.cardId));
                CardView.updateDiscard();
                players[turn].manaCount += 5;
                GameObject[] cardObjs = GameObject.FindGameObjectsWithTag("Card");
                if (cardObjs.Length < players[turn].hand.deck.Count)
                {
                    for (int i = 0; i < cardObjs.Length; i++)
                    {
                        if (cardObjs[i] != this.gameObject)
                        {
                            GameObject.Destroy(cardObjs[i]);
                        }
                    }
                    activeSpell = "";
                    nextTurn();
                }
                GameObject.Destroy(this.gameObject);
            }
            else if (activeSpell == "Spellsteal")
            {
                this.gameObject.transform.localScale = new Vector3(0, 0, 0);
                players[victim].hand.removeCard(spellcard.cardId);
                players[turn].hand.add(new Card(spellcard.cardId));
                GameObject[] cardObjs = GameObject.FindGameObjectsWithTag("Card");
                for (int i = 0; i < cardObjs.Length; i++)
                {
                    if (cardObjs[i] != this.gameObject)
                    {
                        GameObject.Destroy(cardObjs[i]);
                    }
                }
                if (players[turn].hand.deck.Count > 5)
                {
                    activeSpell = "";
                    victim = -1;
                    checking = true;
                    for (int i = 0; i < 5; i++)
                    {
                        spellCard(i + 1, players[turn].hand.deck[i].cardId);
                    }
                }
                else
                {
                    activeSpell = "";
                    victim = -1;
                    nextTurn();
                }
                GameObject.Destroy(this.gameObject);
            }
            else if (checking)
            {
                print("The game thinks you're discarding a spell");
                this.gameObject.transform.localScale = new Vector3(0, 0, 0);
                players[turn].hand.removeCard(spellcard.cardId);
                discard.add(new Card(spellcard.cardId));
                CardView.updateDiscard();
                GameObject[] cardObjs = GameObject.FindGameObjectsWithTag("Card");
                if (players[turn].hand.deck.Count > 5)
                {

                }
                else
                {
                    checking = false;
                    for (int i = 0; i < cardObjs.Length; i++)
                    {
                        if (cardObjs[i] != this.gameObject)
                        {
                            GameObject.Destroy(cardObjs[i]);
                        }
                    }
                    nextTurn();
                }
                GameObject.Destroy(this.gameObject);
            }
            else if (subPhase == 1)
            {
                if (players[turn].canCast(spellcard) && phase == spellcard.usePhase)
                {
                    print("The game thinks you're casting a spell");
                    this.gameObject.transform.localScale = new Vector3(0, 0, 0);
                    activeSpell = "Casting";
                    toCast = new Card(spellcard.cardId);
                    discard.add(new Card(spellcard.cardId));
                    CardView.updateDiscard();
                    players[turn].hand.removeCard(spellcard.cardId);
                    GameObject[] cardObjs = GameObject.FindGameObjectsWithTag("Card");
                    for (int i = 0; i < cardObjs.Length; i++)
                    {
                        if (cardObjs[i] != this.gameObject)
                        {
                            GameObject.Destroy(cardObjs[i]);
                        }
                    }
                    GameObject.Destroy(this.gameObject);
                }
            }
            else if (subPhase == 2 && phase == 3)
            {
                print("The game thinks you're buying a card");
                this.gameObject.transform.localScale = new Vector3(0, 0, 0);
                players[turn].hand.add(new Card(spellcard.cardId));
                if (turn == adeptBonuses[5])
                {
                    GameObject[] cardObjs = GameObject.FindGameObjectsWithTag("Card");
                    if (cardObjs.Length == 2)
                    {
                        for (int i = 0; i < cardObjs.Length; i++)
                        {
                            if (cardObjs[i] != this.gameObject)
                            {
                                Card c = cardObjs[i].GetComponent<SpellCase>().spellcard;
                                discard.add(new Card(c.cardId));
                                CardView.updateDiscard();
                                if (turn == adeptBonuses[4])
                                {
                                    for (int j = 0; j < 8; j++)
                                    {
                                        players[turn].battery[j] += c.cost[j];
                                        if (players[turn].battery[j] > 10)
                                        {
                                            players[turn].battery[j] = 10;
                                        }
                                    }
                                }
                                GameObject.Destroy(cardObjs[i]);
                            }
                        }
                        if (players[turn].hand.deck.Count > 5)
                        {
                            checking = true;
                            for (int i = 0; i < 5; i++)
                            {
                                spellCard(i + 1, players[turn].hand.deck[i].cardId);
                            }
                        }
                        else
                        {
                            nextTurn();
                        }
                    }

                }
                else
                {

                    GameObject[] cardObjs = GameObject.FindGameObjectsWithTag("Card");
                    for (int i = 0; i < cardObjs.Length; i++)
                    {
                        if (cardObjs[i] != this.gameObject)
                        {
                            //print(cardObjs[i]);
                            //print(cardObjs[i].GetComponent<SpellCase>());
                            //print(cardObjs[i].GetComponent<SpellCase>().spellcard);
                            Card c = cardObjs[i].GetComponent<SpellCase>().spellcard;
                            //print(c);
                            //print(c.cardId);
                            //print(new Card(c.cardId));
                            //print(discard);
                            discard.add(new Card(c.cardId));
                            CardView.updateDiscard();
                            if (turn == adeptBonuses[4])
                            {
                                for (int j = 0; j < 8; j++)
                                {
                                    players[turn].battery[j] += c.cost[j];
                                    if (players[turn].battery[j] > 10)
                                    {
                                        players[turn].battery[j] = 10;
                                    }
                                }
                            }
                            GameObject.Destroy(cardObjs[i]);
                        }
                    }
                    if (players[turn].hand.deck.Count > 5)
                    {
                        checking = true;
                        for (int i = 0; i < 5; i++)
                        {
                            spellCard(i + 1, players[turn].hand.deck[i].cardId);
                        }
                    }
                    else
                    {
                        nextTurn();
                    }
                }
                GameObject.Destroy(this.gameObject);

            }
        }
        else if (this.tag == "DisCard")
        {
            if(activeSpell == "Munificence")
            {
                discard.removeCard(spellcard.cardId);
                players[turn].hand.add(new Card(spellcard.cardId));

                mode = 1;
                MASTER.boardCam.enabled = true;
                MASTER.queueCam.enabled = false;
                MASTER.cardCam.enabled = false;
                MASTER.discardCam.enabled = false;
                MASTER.boardCanvas.SetActive(true);
                MASTER.queueCanvas.SetActive(false);
                MASTER.cardCanvas.SetActive(false);

                if (players[turn].hand.deck.Count > 5)
                {
                    activeSpell = "";
                    checking = true;
                    for (int i = 0; i < 5; i++)
                    {
                        spellCard(i + 1, players[turn].hand.deck[i].cardId);
                    }
                }
                else
                {
                    activeSpell = "";
                    nextTurn();
                }

                CardView.updateDiscard();
            }
        }
        else if(this.tag == "ActiveCard")
        {
            players[turn].activeSpell = null;
            spellcard.activate();
            GameObject.Destroy(this);
        }
    }
}
