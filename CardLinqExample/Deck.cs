using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardLinqExample;
public class Deck : ObservableCollection<Card>
{
    private static Random _random = new();

    public Deck()
    {
        Reset();
    }

    /// <summary>
    /// Reset the deck to factory default
    /// </summary>
    public void Reset()
    {
        Clear();
        for (int i = 0; i < 4; i++)
        {
            for (int j = 1; j <= 13; j++)
            {
                Add(new Card((Values)j, (Suits)i));
            }
        }
    }

    public Card Deal(int index)
    {
        var card = base[index];
        RemoveAt(index);

        return card;
    }

    public Deck Shuffle()
    {
        var deckCopy = new List<Card>(this);
        Clear();

        while (deckCopy.Count > 0)
        {
            int index = _random.Next(deckCopy.Count);
            var card = deckCopy[index];
            deckCopy.RemoveAt(index);
            Add(card);
        }

        return this;
    }

    public void Sort()
    {
        List<Card> sortedCards = new List<Card>(this);
        sortedCards.Sort(new CardComparerByValue());
        Clear();
        foreach (Card card in sortedCards)
        {
            Add(card);
        }
    }
}
