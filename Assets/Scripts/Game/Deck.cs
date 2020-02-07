using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck
{
    System.Random rng;
    public List<Card> deck;

    public Deck()
    {
        rng = new System.Random();
        deck = new List<Card>();
    }

    public void add(Card c)
    {
        this.deck.Add(c);
    }

    public Card draw()
    {
        Card ret = this.deck[deck.Count - 1];
        this.deck.RemoveAt(this.deck.Count - 1);
        return ret;
    }

    public void removeCard(string spellId)
    {
        for(int i = 0; i < this.deck.Count; i++)
        {
            if (this.deck[i].cardId == spellId)
            {
                this.deck.RemoveAt(i);
                return;
            }
        }
    }

    public bool isEmpty()
    {
        return this.deck.Count <= 0;
    }

    public void shuffle()
    {
        int n = this.deck.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            Card value = this.deck[k];
            this.deck[k] = this.deck[n];
            this.deck[n] = value;
        }
    }
}
