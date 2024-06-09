using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sea;
using Sea.Maps;
using System;

public class PlayerShip
{
    private Vector2 _loc;
    public Vector2 GlobalLoc
    {
        get => _loc;
        set => _loc = value.Smod(GlobalSize);
    }
    public Vector2 PosV2 => new(GO.transform.position.x, GO.transform.position.z);
    public Vector2 LocalLoc(int regionSize) => GlobalLoc.Smod(regionSize);
    public Vector2 Offset => GlobalCoord - GlobalLoc;
    public Vector2Int GlobalCoord => new((int)GlobalLoc.x, (int)GlobalLoc.y);
    public Vector2Int RegionCoord => new((int)(GlobalLoc.x / RegionSize), (int)(GlobalLoc.y / RegionSize));

    public Region Region
    {
        get
        {
            // return WorldMapScene.Io.Map.Territories.GetValueOrDefault(new Vector2Int((int)(GlobalLoc.x / RegionSize), (int)(GlobalLoc.y / RegionSize))).re;
            return WorldMapScene.Io.Map.Regions[WorldMapScene.Io.Map.RegionIndexFromGlobalCoord(GlobalCoord)];
        }
    }


    public Vector2Int LocalCoord(int regionSize) => GlobalCoord.Smod(regionSize);

    private float _rotY;
    public float RotY
    {
        get => _rotY;
        set
        {
            _rotY = value.Smod(360);
        }
    }
    public float MoveSpeed = 2.5f;
    public float RotateSpeed = 40f;

    public Vector3 SeaPos;
    public Quaternion SeaRot;

    // public NPCShipType ShipType;
    public GameObject Parent;
    public GameObject GO;
    public CapsuleCollider CapsuleCollider;
    private readonly int MapSize;
    private readonly int RegionSize;
    private readonly int GlobalSize;

    public ShipStats.ShipStats ShipStats = Data.Two.Manager.Io.ActiveShip.ShipStats;

    //  TO DO get actual data
    //     new(
    //         new ShipStats.HullStats(
    //             new Data.Two.Schooner(),
    //            new Data.Two.Oak()),
    //         new ShipStats.CannonStats(
    //           new Data.Two.Culverin(),
    //            new Data.Two.CastIron()),
    //         new ShipStats.RiggingStats(new Data.Two.Hemp()),
    //         numOfCannons: 32
    //    );


    public PlayerShip(Sea.WorldMapScene scene)
    {
        RotY = 180;
        MapSize = scene.Map.RegionResolution;
        RegionSize = scene.Map.RegionSize;
        GlobalSize = MapSize * RegionSize;
        GlobalLoc = new Vector2((RegionSize * 5) + RegionSize - 2, (RegionSize * 6) + 2);//(RegionSize * 5) + (RegionSize * .5f) + 2, (RegionSize * 6) + (RegionSize * .5f) + 2);
        GO = SetUpTheShip(scene);
    }

    GameObject SetUpTheShip(Sea.WorldMapScene scene)
    {
        Parent = new GameObject(nameof(PlayerShip));
        Parent.transform.rotation = Quaternion.Euler(new Vector3(0, RotY, 0));
        Parent.transform.SetParent(scene.TheSea.transform);
        Parent.transform.position = new Vector3(
            (float)((float)scene.Board.Size * (float)(1f / (float)scene.Board.SubDivision)),
            .1f,
            (float)((float)scene.Board.Size * (float)(1f / (float)scene.Board.SubDivision)));

        GameObject go = Assets.Sloop.gameObject;
        go.name = nameof(PlayerShip);
        go.transform.SetParent(Parent.transform);
        go.transform.SetPositionAndRotation(Parent.transform.position, Quaternion.Euler(new Vector3(0, 180, 0)));
        go.transform.localScale = Vector3.one * .6f;
        CapsuleCollider = go.GetComponentInChildren<CapsuleCollider>();

        return go;
    }

    private Card _confirmPopup;
    public Card ConfirmPopup => _confirmPopup ??= new Card(nameof(ConfirmPopup), GO.transform)
        .SetPositionAll(1, -1)
        .SetFontScale(.5f, .5f)
        .AutoSizeTextContainer(true)
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

    private Card _attackPopup;
    public Card AttackPopup => _attackPopup ??= new Card(nameof(AttackPopup), GO.transform)
        .SetPositionAll(1, 0)
        .SetFontScale(.5f, .5f)
        .AutoSizeTextContainer(true)
        .SetOutlineColor(Color.black)
        .SetTextAlignment(TMPro.TextAlignmentOptions.Right)
        .SetTMPRectPivot(new Vector2(1, .5f))
        .SetOutlineWidth(.15f)
        .AutoSizeFont(true)
        .AllowWordWrap(false)
        .SetImageSprite(Assets.NorthButton)
        .SetImageSize(.5f, .5f)
        .OffsetImageFromTMP(Vector2.right * .5f)
        ;
}

// public static class PlayerShipSystems
// {

//     public static void UpdateShipCoords(this PlayerShip player, Sea.WorldMapScene scene)
//     {
//         scene.HUD.UpdateCoords(player.GlobalCoord, scene.Map.GlobalSize);
//     }

// }