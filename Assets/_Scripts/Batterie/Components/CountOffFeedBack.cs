using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public sealed class CountOffFeedback
{
    public CountOffFeedback((int, string)[] counts)
    {
        Counts = counts;
    }

    readonly (int index, string @string)[] Counts;
    readonly List<FloatyCard> Cards = new();
    private bool Running;
    public int Counter { get; private set; }

    public Card CreateCard(string s)
    {
        int r = Random.Range(-2, 2);

        Card Card = new Card(s, null)
            .SetFontScale(3, 3)
            .SetTextString(s)
            .AutoSizeFont(true)
            .AllowWordWrap(false)
            .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
            .SetTMPSize(Vector2.one * 5)
            .SetTMPPosition(Vector3.zero)
        ;

        Cards.Add(new FloatyCard(Card, r, 0));

        return Card;
    }

    public void UpdateLoop()
    {
        Running = true;
        Loop().StartCoroutine();

        IEnumerator Loop()
        {
            while (Running)
            {
                yield return null;

                for (int i = 0; i < Cards.Count; i++)
                {
                    if (Cards[i].life < 1)
                    {
                        Cards[i].life += Time.deltaTime;
                        Cards[i].Card.SetTextColor(new Color(1, 1, 1, 1.5f - (Cards[i].life * 2)));
                    }
                    else
                    {
                        Object.Destroy(Cards[i].Card.GO);
                        Cards.Remove(Cards[i]);
                    }
                }
            }
        }
    }

    public void SelfDestruct()
    {
        Destruct().StartCoroutine();
        IEnumerator Destruct()
        {
            Running = false;

            yield return null;

            for (int i = 0; i < Cards.Count; i++)
            {
                Object.Destroy(Cards[i].Card.GO);
            }
            Cards.Clear();
        }
    }

    private class FloatyCard
    {
        internal FloatyCard(Card b, int i, float f)
        {
            Card = b; ind = i; life = f;
        }
        public Card Card;
        public int ind;
        public float life;
    }

    public void ReadCountOff()
    {
        CreateCard(Counts[Counter].@string);
        Counter++;
    }
}
