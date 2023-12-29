using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sea;
public static class SeaBoardSystems
{


    public static List<SubTile> SetUpBoard(this Scene scene)
    {
        List<SubTile> boardTiles = new();

        for (int x = 0; x < scene.Board.SubGridSize; x++)
            for (int z = 0; z < scene.Board.SubGridSize; z++)
                boardTiles.Add(new SubTile(
                    new Vector3Int(x, 0, z),
                    scene.TheSea.transform,
                    scene.SeaColor));

        return boardTiles;
    }

    // public static Vector3Int CenterV3i(this Board board) => new((int)board.Center(), 0, (int)board.Center());
    public static float Center(this Board board) => board.Size * .5f;
    public static int CenterI(this Board board) => (int)(board.Size * .5f);
    public static Vector2Int CenterV2i(this Board board) => Vector2Int.one * (int)(board.Size * .5f);
    public static Vector2 CenterV2(this Board board) => Vector2.one * (board.Size * .5f);
    public static Color MyGrey(this Scene scene) => new(.45f, .45f, .45f, .35f);
    public static Color MyRed(this Scene scene) => new(.85f, .15f, .35f, .35f);
    public static Color MyGreen(this Scene scene) => new(.25f, .55f, .25f, .35f);
    public static Color MyBlue(this Scene scene) => new(.15f, .35f, .85f, .35f);
    public static Color MyCyan(this Scene scene) => new(.15f, .65f, .55f, .35f);
    public static Color MyYellow(this Scene scene) => new(.75f, .65f, .15f, .35f);
    public static Color MyMagenta(this Scene scene) => new(.85f, .15f, .75f, .35f);
    public static Color GetRandomSeaColor(this Scene scene) => Random.Range(0, 6) switch
    {
        0 => scene.MyRed(),
        1 => scene.MyGreen(),
        2 => scene.MyBlue(),
        3 => scene.MyCyan(),
        4 => scene.MyYellow(),
        _ => scene.MyMagenta()
    };

    public static Color OverlayWhite(this Scene scene) => new(.3f, .3f, .3f, .8f);
    public static Color OverlayGrey(this Scene scene) => new(.5f, .5f, .1f, .2f);
    public static Color OverlayGreen(this Scene scene) => new(0, 1f, .0f, .2f);
    public static Color OverlayRed(this Scene scene) => new(1f, 0f, 0f, .2f);

}
