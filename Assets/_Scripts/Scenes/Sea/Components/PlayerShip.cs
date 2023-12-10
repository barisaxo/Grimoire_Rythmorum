using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip
{
    public Vector3 Pos;
    public Vector3 Offset => Coord - Pos;
    public Vector3Int Coord => new(Mathf.FloorToInt(Pos.x), 0, Mathf.FloorToInt(Pos.z));

    public float RotY;
    public float MoveSpeed = 2.5f;
    public float RotateSpeed = 40f;

    public NPCShipType ShipType;
    public GameObject Parent;
    public GameObject GO;
    public CapsuleCollider CapsuleCollider;

    public PlayerShip(SeaScene sea)
    {
        RotY = 180;
        GO = SetUpTheShip(sea);
        CapsuleCollider = GO.GetComponent<CapsuleCollider>();
    }

    GameObject SetUpTheShip(SeaScene sea)
    {
        Parent = new GameObject(nameof(PlayerShip));
        Parent.transform.rotation = Quaternion.Euler(new Vector3(0, RotY, 0));

        GameObject go = Assets.CatBoat;
        go.name = nameof(Assets.CatBoat);
        go.transform.SetParent(Parent.transform);
        go.transform.position = Parent.transform.position;
        go.transform.localScale = Vector3.one * .6f;

        return go;
    }

    private Card _bark;
    public Card Bark => _bark ??= new Card(nameof(Bark), GO.transform)
        .SetPositionAll(1, -1)
        .SetFontScale(.5f, .5f)
        .AutoSizeTextContainer(true)
        .SetTextString("Hail")
        .SetOutlineColor(Color.black)
        .SetTextAlignment(TMPro.TextAlignmentOptions.Right)
        .SetTMPRectPivot(new Vector2(1, .5f))
        .SetOutlineWidth(.15f)
        .AutoSizeFont(true)
        .AllowWordWrap(false)
        .SetImageSprite(Assets.EastButton)
        .SetImageSize(.5f, .5f)
        .OffsetImageFromTMP(Vector2.right * .5f)
        ;


}

/*

        .SetPositionAll(Cam.UIOrthoX - 1.5f, Cam.UIOrthoY - 3.15f)
        .SetFontScale(.5f, .5f)
        .AutoSizeTextContainer(true)
        .SetImageSprite(Assets.NorthButton)
        .SetImageSize(.5f, .5f)
        .SetTextString("Inventory")
        .SetTextAlignment(TextAlignmentOptions.Right)
        .SetTMPRectPivot(new Vector2(1, .5f))
        .SetOutlineWidth(.15f)
        .OffsetImagePosition(Vector2.right)

*/