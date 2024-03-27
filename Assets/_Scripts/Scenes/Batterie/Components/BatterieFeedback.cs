using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatterieFeedback
{
    List<FloatyCard> Cards = new List<FloatyCard>();

    public void CreateCard(string s)
    {
        int r = UnityEngine.Random.Range(-2, 2);

        Card Card = new Card(s, null)
            .SetFontScale(.5f, .5f)
            .SetTextString(s)
            .AutoSizeFont(true)
            .AutoSizeTextContainer(true)
            .AllowWordWrap(false)
            .SetTMPSize(Vector2.one)
            .SetTMPPosition(new Vector2(MathF.Sin(Time.time) + r, 4))
            ;

        Cards.Add(new FloatyCard(Card, r, 0));
    }

    bool Running;
    public void UpdateLoop()
    {
        Running = true;
        Loop().StartCoroutine();

        IEnumerator Loop()
        {
            while (Running)
            {
                for (int i = 0; i < Cards.Count; i++)
                {
                    if (Cards[i].life < 1)
                    {
                        Cards[i].life += Time.deltaTime;
                        Cards[i].Card.SetTMPSize(Vector2.one * (1 - Cards[i].life))
                                     .SetTMPPosition(NewCardPosition(i));

                        Vector2 NewCardPosition(int i) => new(MathF.Sin(Time.time) + Cards[i].ind, 4 + (Cards[i].life * 1.5f));
                    }
                    else
                    {
                        UnityEngine.Object.Destroy(Cards[i].Card.GO);
                        Cards.Remove(Cards[i]);
                    }
                }
                yield return null;
            }
            SelfDestruct();
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
                UnityEngine.Object.Destroy(Cards[i].Card.GO);
            }
            Cards.Clear();
        }
    }

    class FloatyCard
    {
        internal FloatyCard(Card b, int i, float f)
        {
            Card = b; ind = i; life = f;
        }
        public Card Card;
        public int ind;
        public float life;
    }
}
