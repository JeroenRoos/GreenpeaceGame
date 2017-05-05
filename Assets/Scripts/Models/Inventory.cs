using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        //card.increaseCurrentRewards(); //first increment upon receiving the card
        ownedCards.Add(card);
    }

    public void RemoveCardFromInventory(Card card)
    {
        ownedCards.Remove(card);
    }
}
