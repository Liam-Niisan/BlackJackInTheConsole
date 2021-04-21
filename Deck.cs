using System;
using System.Collections.Generic;
using System.Linq;

public class Deck : Stack<Card>
{
    public Deck(IEnumerable<Card> collection) : base(collection) { }
    public Deck() : base(52) { }

    //indexer
    public Card this[int index]
    {
        get
        {
            Card item;

            if (index >= 0 && index <= this.Count - 1)
            {
                item = this.ToArray()[index];
            }
            else
            {
                item = null;
            }

            return item;
        }
    }

    //let's get value of the Deck
    public double Value
    {
        get
        {
            return BlackJackRules.HandValue(this);
        }
    }
}