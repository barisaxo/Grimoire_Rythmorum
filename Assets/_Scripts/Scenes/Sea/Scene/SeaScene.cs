using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SeaScene
{
    public void SelfDestruct()
    {
        Swells.DisableSwells();
        RockTheBoat.SelfDestruct();
        Object.Destroy(TheSea);
        Resources.UnloadUnusedAssets();
    }

    #region FIELDS

    public bool ShowShipCoords = false;
    public bool ShowIslandCoords = false;

    private GameObject _theSea;
    public GameObject TheSea => _theSea != null ? _theSea : _theSea = new GameObject(nameof(TheSea));

    private GameObject ship;
    public GameObject Ship => ship != null ? ship : ship = SetUpTheShip();

    public bool Sailing;
    public bool haveMoved = false;

    private Swells _swells;
    public Swells Swells => _swells ??= new(this);

    private RockTheBoat _rockTheBoat;
    public RockTheBoat RockTheBoat => _rockTheBoat ??= new();
    //public void ShipRotY(float y) => rockTheBoat.SetRotY(y);
    public float restoreShipRotY;

    public Vector3 SeaLoc;
    public Vector3 SeaGridCenter;
    public Vector3 MapLoc;
    public Vector3 restoreMapLoc;

    public int StartingShips;
    public int SeaGridSize { get; private set; } = 7;
    public int MapSize { get; private set; } = 80;
    public int NumOfCardShips;

    public Vector2 IslandLoc;

    public Color SeaColor;
    public Color MyGrey => new Color(.45f, .45f, .45f, .35f);
    public Color MyRed => new Color(.85f, .15f, .35f, .35f);
    public Color MyGreen => new Color(.25f, .55f, .25f, .35f);
    public Color MyBlue => new Color(.15f, .35f, .85f, .35f);
    public Color MyCyan => new Color(.15f, .65f, .55f, .35f);
    public Color MyYellow => new Color(.75f, .65f, .15f, .35f);
    public Color MyMagenta => new Color(.85f, .15f, .75f, .35f);

    public Color OverlayWhite => new Color(.5f, .5f, .5f, .01f);
    public Color OverlayGrey => new Color(.5f, .5f, .1f, .2f);
    public Color OverlayGreen => new Color(0, 1f, .0f, .2f);
    public Color OverlayRed => new Color(1f, 0f, 0f, .2f);

    private List<SeaTile> seaGrid;
    public List<SeaTile> SeaGrid => seaGrid ??= SetUpSeaGrid();

    private List<SeaMapTile> mapGrid;
    public List<SeaMapTile> MapGrid => mapGrid ??= SetUpMapGrid();

    public readonly string _ship = "SHIP ";
    public readonly string _island = "ISLAND ";
    public readonly string _commaSpace = ", ";

    #endregion

    #region INITIALIZATION

    public SeaScene()
    {
        SeaColor = MyCyan;

        SeaGridSize = 7;
        MapSize = 80;
        //TheSea.SetActive(true);
        _ = SeaGrid;
        _ = MapGrid;
        SetUpSeaCam();
        _ = DayCounterText;
        _ = Ship;
        Swells.EnableSwells();
        RockTheBoat.Rocking = true;
    }

    GameObject SetUpTheShip()
    {
        GameObject PlayerShip = Object.Instantiate(Assets.CatBoat);
        PlayerShip.name = nameof(PlayerShip);
        RockTheBoat.AddBoat(PlayerShip.transform);
        PlayerShip.transform.SetParent(SeaGrid[(int)(SeaGrid.Count * .5f)].GO.transform);
        PlayerShip.transform.position = SeaGrid[(int)(SeaGrid.Count * .5f)].Loc + (Vector3.up * .75f);
        PlayerShip.transform.localScale = Vector3.one * .6f;

        return PlayerShip;
    }

    List<SeaTile> SetUpSeaGrid()
    {
        List<SeaTile> seaTiles = new();
        for (int x = 0; x < SeaGridSize; x++)
        {
            for (int z = 0; z < SeaGridSize; z++)
            {
                seaTiles.Add(new SeaTile(new Vector3(x, 0, z), this));
            }
        }
        return seaTiles;
    }

    List<SeaMapTile> SetUpMapGrid()
    {
        List<SeaMapTile> mapTiles = new();
        for (int x = 0; x < MapSize; x++)
        {
            for (int z = 0; z < MapSize; z++)
            {
                mapTiles.Add(new SeaMapTile(new Vector3(x, 0, z)));
            }
        }
        return mapTiles;
    }

    void SetUpSeaCam()
    {
        Cam.Io.Camera.transform.SetParent(TheSea.transform, false);
        Cam.Io.Camera.transform.SetPositionAndRotation(
               new Vector3(CenterPos().x, CamPosY(), CenterPos().z - CamPosZ()),
               Quaternion.Euler(new Vector3(CamRotX(), 0, 0)));

        var l = new GameObject(nameof(Light)).AddComponent<Light>();
        l.type = LightType.Directional;
        l.color = new Color(.9f, .8f, .65f);
        l.shadows = LightShadows.None;
        l.transform.SetPositionAndRotation(Cam.Io.Camera.transform.position, Cam.Io.Camera.transform.rotation);
        l.intensity = 3;

        float CamPosZ() => 4f;
        float CamPosY() => 5f;
        float CamRotX() => 55;
        Vector3 CenterPos() => SeaGrid[(int)(SeaGrid.Count * .5f)].Loc;
    }


    private Card _dayCounterText;
    public Card DayCounterText => _dayCounterText ??= new Card(nameof(DayCounterText), TheSea.transform)
        .SetTMPPosition(new Vector2(Cam.UIOrthoX - 1, Cam.UIOrthoY - 1))
        .SetTMPSize(new Vector2(3, 1))
        .SetTextAlignment(TextAlignmentOptions.Right)
        .SetFontScale(.55f, .55f)
        .AutoSizeFont(true);

    private Card _sextantText;
    public Card SextantText => _sextantText ??= new Card(nameof(SextantText), TheSea.transform)
        .SetTMPPosition(new Vector2(0, Cam.UIOrthoY - 1))
        .SetTMPSize(new Vector2(3, 1))
        .SetTextAlignment(TextAlignmentOptions.Center)
        .SetFontScale(.55f, .55f)
        .AutoSizeFont(true);

    private Card _islandCoordText;
    public Card IslandCoordText => _islandCoordText ??= new Card(nameof(IslandCoordText), TheSea.transform)
        .SetTMPPosition(new Vector2(-Cam.UIOrthoX + 1, Cam.UIOrthoY - 1))
        .SetTMPSize(new Vector2(3, 1))
        .SetTextAlignment(TextAlignmentOptions.Left)
        .SetFontScale(.55f, .55f)
        .AutoSizeFont(true);

    #endregion
}
