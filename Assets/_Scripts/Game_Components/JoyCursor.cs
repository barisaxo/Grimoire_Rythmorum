using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyCursor
{

    public JoyCursor()
    {

    }

    public void SelfDestruct()
    {
        Object.Destroy(Parent);

    }


    private GameObject _parent;
    public GameObject Parent => _parent ? _parent : _parent = new GameObject(nameof(JoyCursor));

    private Card _cursor;
    public Card Cursor => _cursor ??= new Card(nameof(Cursor), Parent.transform)
        .SetImageSprite(Assets.CircleKeyboard);

}