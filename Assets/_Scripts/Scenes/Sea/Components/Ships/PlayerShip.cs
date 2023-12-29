using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sea;
using System;

public class PlayerShip
{
    private Vector2 _pos;
    public Vector2 GlobalPos
    {
        get => _pos;
        set => _pos = value.Smod(GlobalSize);
    }
    public Vector2 LocalPos(int regionSize) => GlobalPos.Smod(regionSize);
    public Vector2 Offset => GlobalCoord - GlobalPos;
    public Vector2Int GlobalCoord => new((int)GlobalPos.x, (int)GlobalPos.y);
    public Vector2Int Region => new((int)(GlobalPos.x / RegionSize), (int)(GlobalPos.y / RegionSize));
    public Vector2Int LocalCoord(int regionSize) => GlobalCoord.Smod(regionSize);

    public float RotY;
    public float MoveSpeed = 2.5f;
    public float RotateSpeed = 40f;

    public NPCShipType ShipType;
    public GameObject Parent;
    public GameObject GO;
    public CapsuleCollider CapsuleCollider;
    private readonly int MapSize;
    private readonly int RegionSize;
    private readonly int GlobalSize;

    public PlayerShip(Scene scene)
    {
        RotY = 180;
        MapSize = scene.Map.Size;
        RegionSize = scene.Map.RegionSize;
        GlobalSize = MapSize * RegionSize;
        GlobalPos = .5f * RegionSize * Vector2.one;
        GO = SetUpTheShip(scene);
    }

    GameObject SetUpTheShip(Scene scene)
    {
        Parent = new GameObject(nameof(PlayerShip));
        Parent.transform.rotation = Quaternion.Euler(new Vector3(0, RotY, 0));
        Parent.transform.SetParent(scene.TheSea.transform);
        Parent.transform.position = scene.Board.SubTiles[(int)(scene.Board.SubTiles.Count * .5f)].Loc + (Vector3.up * .3f);

        GameObject go = Assets.CatBoat;
        go.name = nameof(Assets.CatBoat);
        go.transform.SetParent(Parent.transform);
        go.transform.position = Parent.transform.position;
        go.transform.localScale = Vector3.one * .6f;
        go.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        CapsuleCollider = go.GetComponentInChildren<CapsuleCollider>();

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
