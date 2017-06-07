using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//The inventory class was created with having more than just cards within it in mind.

[Serializable]
public class Inventory
{
    public List<Card> ownedCards { get; private set; }

    public Inventory()
    {
        ownedCards = new List<Card>();
    }

    public void AddCardToInventory(Card card)
    {
        ownedCards.Add(card);
    }

    public void RemoveCardFromInventory(Card card)
    {
        ownedCards.Remove(card);
    }
}
