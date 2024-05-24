using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PatternViewer
{

    public void SelfDestruct()
    {
        Animate = false;
        tiles = null;
        PatternIDText.SelfDestruct();
        // UnityEngine.Object.Destroy(pattern);
        Resources.UnloadUnusedAssets();
        GameObject.Destroy(Parent);
    }

    #region FIELDS

    // private GameObject pattern;
    // public GameObject Pattern => pattern != null ? pattern : pattern = SetUpPatterns();


    GameObject _parent;
    public GameObject Parent => _parent ? _parent : _parent = SetUpParent();

    private GameObject SetUpParent()
    {
        GameObject go = new(nameof(PatternViewer));
        go.transform.position = new Vector3(14.5f, 1, 4);
        return go;
    }


    private int Patterns => Data.Two.Manager.Io.Player.GetLevel(new Data.Two.PatternsFound());

    private Card _patternIDText;
    public Card PatternIDText => _patternIDText ??= new Card(nameof(PatternIDText), null)
        // .SetTMPPosition(new Vector3(14.5f, 2, 0))
        .SetCanvasRenderMode(RenderMode.WorldSpace)
        .SetCanvasWorldCamera(Cam.Io.Camera)
        .SetTextAlignment(TextAlignmentOptions.Center)
        .SetFontScale(.35f, .35f)
        .SetFontStyle(FontStyles.Normal)
        .SetTMPPosition(new Vector3(16.5f, 1.25f, 3))
        .SetTMPSize(new Vector3(-1, 1, 1));

    // PatternIDText.fontStyle = FontStyles.Normal;
    // PatternIDText.transform.position = new Vector3(16.5f, 1.25f, 3);
    // PatternIDText.fontSize = .35f;
    // PatternIDText.transform.localScale = new Vector3(-1, 1, 1);


    private List<PatternTile> tiles;
    public List<PatternTile> Tiles => tiles ??= SetUpTiles();

    readonly Color tileColor = new(.35f, .2f, .43f, .65f);
    private readonly int ogBoardSize = 100;
    private string PatternID => "Æ " + PatternNo + " (of " + Patterns + ") : ƒ " + (BoardSize + 1) + "(X•Y) % ";
    private readonly string _r = " = ";
    readonly float realSize = 1;
    private float redSpeed, redRange, redAddition,
                         greenSpeed, greenRange, greenAddition,
                         blueSpeed, blueRange, blueAddition;
    public int invertedFramerate = 20;
    int counter;
    int divisor = 3;
    int remainder = 0;

    enum PatternTileDirection { O, N, S, NS, E, NE, SE, NSE, W, NW, SW, NSW, EW, NEW, SEW, NSEW }

    #endregion

    #region FIELD METHODS

    private bool _animate; public bool Animate
    {
        get => _animate;
        set
        {
            if (_animate = value) MonoHelper.OnUpdate += OnUpdate;
            else MonoHelper.OnUpdate -= OnUpdate;
        }
    }

    private int patternNo = 1; public int PatternNo
    {
        get { return patternNo; }
        set
        {
            patternNo = value < 1 ? 1 :
                        value > Patterns ? Patterns :
                        value;
        }
    }

    private int boardSize = 1; public int BoardSize
    {
        get { return boardSize; }
        set
        {
            boardSize = value < 1 ? 1 : value;
            ResizeBoard();
        }
    }

    #endregion

    #region INITIALIZATION

    public void Init()
    {
        Debug.Log("No Of Patterns: " + Patterns);
        _ = Parent;
        _ = PatternIDText;
        //Cam.orthographicSize = ogBoardSize;
        //Cam.transform.position = new Vector3(-0.5f, -0.5f, -10);
        // Canvas.renderMode = RenderMode.WorldSpace;
        // Canvas.worldCamera = Entities.Io.Cam;

        //tiles = null;
        _ = Tiles;
        // PatternIDText.fontStyle = FontStyles.Normal;
        // PatternIDText.transform.position = new Vector3(16.5f, 1.25f, 3);
        // PatternIDText.fontSize = .35f;
        // PatternIDText.transform.localScale = new Vector3(-1, 1, 1);
        PatternNo = 0;
        PatternUp();
        PatternDown();
        MakePattern();
        SetRandomColors();
        Animate = true;
    }

    // private GameObject SetUpPatterns()
    // {
    //     GameObject go = new GameObject(nameof(Pattern));
    //     go.transform.position = new Vector3(14.5f, 2, 0);
    //     return go;
    // }

    private void SetRandomColors()
    {
        redSpeed = UnityEngine.Random.Range(.20f, .80f);
        greenSpeed = UnityEngine.Random.Range(.20f, .80f);
        blueSpeed = UnityEngine.Random.Range(.20f, .80f);
        //How far the color value can change
        redRange = UnityEngine.Random.Range(.2f, .35f);
        greenRange = UnityEngine.Random.Range(.2f, .35f);
        blueRange = UnityEngine.Random.Range(.2f, .35f);
        //Add some color value on top
        redAddition = UnityEngine.Random.Range(.35f, .5f);
        greenAddition = UnityEngine.Random.Range(.35f, .5f);
        blueAddition = UnityEngine.Random.Range(.35f, .5f);
    }

    List<PatternTile> SetUpTiles()
    {
        List<PatternTile> ts = new List<PatternTile>();
        for (int x = 0; x < ogBoardSize; x++)
        {
            for (int y = 0; y < ogBoardSize; y++)
            {
                ts.Add(new PatternTile(
                    new GameObject(nameof(PatternTile) + " X: " + x + " Y: " + y),
                    new Vector2(x, y),
                    Parent.transform));
            }
        }
        return ts;
    }

    #endregion

    #region METHODS

    private void OnUpdate()
    {
        counter++;
        if (invertedFramerate < counter)
        {
            counter = 0;
            PatternUp();
        }
    }

    public void PatternUp()
    {
        if (PatternNo + 1 > Patterns) { return; }
        PatternNo++;
        BoardSize++;
        if (BoardSize > divisor * 2)
        {
            remainder++;
            if (remainder == divisor)
            {
                divisor++;
                remainder = 0;
            }
            BoardSize = divisor - 1;
        }
        PatternIDText.SetTextString(PatternID + divisor + _r + remainder);
        MakePattern();
    }

    public void PatternDown()
    {
        if (PatternNo - 1 == 0) return;

        PatternNo--;
        BoardSize--;

        if (boardSize < divisor - 1)
        {
            remainder--;
            if (remainder < 0)
            {
                divisor--;
                remainder = divisor - 1;
            }
            BoardSize = divisor * 2;
        }
        PatternIDText.SetTextString(PatternID + divisor + _r + remainder);
        MakePattern();
    }

    public void FasterFrameRate()
    {
        invertedFramerate -= invertedFramerate < 2 ? 0 : 1;
    }

    public void SlowerFrameRate()
    {
        invertedFramerate += invertedFramerate > 99 ? 0 : 1;
    }

    private void ResizeBoard() => Parent.transform.localScale =
        new Vector3((float)((float)realSize / (float)BoardSize), (float)((float)realSize / (float)BoardSize), 1);

    private void MakePattern()
    {
        for (int i = 0; i < Tiles.Count; i++)
        {
            Tiles[i].isMod = IsMod(Tiles[i].loc);
        }

        foreach (PatternTile t in Tiles)
        {
            if (t.isMod)
            {
                AssignTileSprites(t);
            }
        }

        ColorTiles();
        return;

        #region INTERNAL
        void AssignTileSprites(PatternTile t)
        {
            t.sr.sprite = BorderedWallTile(t);
            t.go.transform.SetPositionAndRotation(t.go.transform.position,
                Quaternion.Euler(new Vector3(0, 0, WallTileRotation(t))));
        }

        PatternTileDirection AdjacentTiles(PatternTile t)
        {
            PatternTileDirection adjacents = 0;
            if (North() < Tiles.Count && Tiles[North()].isMod) { adjacents += (int)PatternTileDirection.N; }
            if (East() < Tiles.Count && Tiles[East()].isMod) { adjacents += (int)PatternTileDirection.E; }
            if (South() > -1 && Tiles[South()].isMod) { adjacents += (int)PatternTileDirection.S; }
            if (West() > -1 && Tiles[West()].isMod) { adjacents += (int)PatternTileDirection.W; }
            return adjacents;

            int North() => (t.loc + Vector2.up).Vec2ToInt(ogBoardSize);
            int East() => (t.loc + Vector2.right).Vec2ToInt(ogBoardSize);
            int South() => (t.loc + Vector2.down).Vec2ToInt(ogBoardSize);
            int West() => (t.loc + Vector2.left).Vec2ToInt(ogBoardSize);
        }

        Sprite BorderedWallTile(PatternTile t) => AdjacentTiles(t) switch
        {
            PatternTileDirection.N => Assets.Wall1,
            PatternTileDirection.S => Assets.Wall1,
            PatternTileDirection.E => Assets.Wall1,
            PatternTileDirection.W => Assets.Wall1,
            PatternTileDirection.NS => Assets.Wall2a,
            PatternTileDirection.NE => Assets.Wall2,
            PatternTileDirection.NW => Assets.Wall2,
            PatternTileDirection.SE => Assets.Wall2,
            PatternTileDirection.SW => Assets.Wall2,
            PatternTileDirection.EW => Assets.Wall2a,
            PatternTileDirection.NSE => Assets.Wall3,
            PatternTileDirection.NSW => Assets.Wall3,
            PatternTileDirection.NEW => Assets.Wall3,
            PatternTileDirection.SEW => Assets.Wall3,
            PatternTileDirection.NSEW => Assets.Wall4,
            _ => Assets.Wall0,
        };

        int WallTileRotation(PatternTile t) => AdjacentTiles(t) switch
        {
            PatternTileDirection.S => 180,
            PatternTileDirection.E => -90,
            PatternTileDirection.W => 90,
            PatternTileDirection.NW => 90,
            PatternTileDirection.SE => -90,
            PatternTileDirection.SW => 180,
            PatternTileDirection.EW => 90,
            PatternTileDirection.NSE => -90,
            PatternTileDirection.NSW => 90,
            PatternTileDirection.SEW => 180,
            _ => 0,
        };
        #endregion
    }

    bool IsMod(Vector2 pos)
    {
        return
           x() < BoardSize + 1 && y() < BoardSize + 1 &&
          (x() == ogBoardSize - 1 || y() == ogBoardSize - 1 ||
           x() == 0 || y() == 0 || x() == BoardSize || y() == BoardSize ||
           x() * y() % divisor == remainder ||
          (x() - BoardSize) * (y() - BoardSize) % divisor == remainder ||
          (x() - BoardSize) * -y() % divisor == remainder ||
          -x() * (y() - BoardSize) % divisor == remainder);

        int x() { return (int)pos.x; }
        int y() { return (int)pos.y; }
    }

    void ColorTiles()
    {
        float r = Mathf.Sin(Time.time * redSpeed) * redRange;
        float g = Mathf.Sin(Time.time * greenSpeed) * greenRange;
        float b = Mathf.Sin(Time.time * blueSpeed) * blueRange;

        for (int i = 0; i < Tiles.Count; i++)
        {
            if (!Tiles[i].isMod) { Tiles[i].sr.color = Color.clear; continue; }
            Tiles[i].sr.color = new Color(redAddition - r, greenAddition - g, blueAddition - b, .65f);
        }
    }

    #endregion
}

public class PatternTile
{
    public PatternTile(GameObject g, Vector2 l, Transform t)
    {
        go = g;
        sr = go.AddComponent<SpriteRenderer>();
        loc = l;
        go.transform.SetParent(t, false);
        go.transform.position = new Vector3(loc.x + 15.5f, loc.y + 2, 4);
    }
    public GameObject go;
    public SpriteRenderer sr;
    public Vector2 loc;
    public bool isMod;
}