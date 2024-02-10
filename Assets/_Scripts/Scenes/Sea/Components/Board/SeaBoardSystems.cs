using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sea;
public static class SeaBoardSystems
{
    // public static Vector3Int CenterV3i(this Board board) => new((int)board.Center(), 0, (int)board.Center());
    public static float Center(this Board board) => board.Size * .5f;
    public static int CenterI(this Board board) => (int)(board.Size * .5f);
    public static Vector2Int CenterV2i(this Board board) => Vector2Int.one * (int)(board.Size * .5f);
    public static Vector2 CenterV2(this Board board) => Vector2.one * (board.Size * .5f);
    public static Color MyGrey(this WorldMapScene scene) => new(.45f, .45f, .45f, .35f);
    public static Color MyRed(this WorldMapScene scene) => new(.85f, .15f, .35f, .35f);
    public static Color MyGreen(this WorldMapScene scene) => new(.25f, .55f, .25f, .35f);
    public static Color MyBlue(this WorldMapScene scene) => new(.15f, .35f, .85f, .35f);
    public static Color MyCyan(this WorldMapScene scene) => new(.15f, .65f, .55f, .35f);
    public static Color MyYellow(this WorldMapScene scene) => new(.75f, .65f, .15f, .35f);
    public static Color MyMagenta(this WorldMapScene scene) => new(.85f, .15f, .75f, .35f);
    public static Color GetRandomSeaColor(this WorldMapScene scene) => Random.Range(0, 6) switch
    {
        0 => scene.MyRed(),
        1 => scene.MyGreen(),
        2 => scene.MyBlue(),
        3 => scene.MyCyan(),
        4 => scene.MyYellow(),
        _ => scene.MyMagenta()
    };

    public static Color OverlayWhite(this WorldMapScene scene) => new(.3f, .3f, .3f, .8f);
    public static Color OverlayGrey(this WorldMapScene scene) => new(.5f, .5f, .1f, .2f);
    public static Color OverlayGreen(this WorldMapScene scene) => new(0, 1f, .0f, .2f);
    public static Color OverlayRed(this WorldMapScene scene) => new(1f, 0f, 0f, .2f);

}
