using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card
{
    /// <summary>
    /// The basis for any simple 2D sprite and/or text.
    /// </summary>
    public Card(string name, Transform parent)
    {
        GO = new GameObject(name);
        GO.transform.SetParent(parent, false);
    }

    /// <summary>
    /// The parent GameObject. SpriteRenderer will be attatched to this, Canvas & TMP will be children.
    /// </summary>
    public GameObject GO { get; private set; }

    /// <summary>
    /// Do you want this card and/or Text to be clickable?
    /// </summary>
    public Clickable Clickable;

    private SpriteRenderer _sr;
    /// <summary>
    /// Lives on GO (the parent GameObject).
    /// </summary>
    public SpriteRenderer SpriteRenderer => _sr != null ? _sr : _sr = GO.AddComponent<SpriteRenderer>();

    private Canvas _tmpCanvas;
    public Canvas TMPCanvas
    {
        get
        {
            return _tmpCanvas != null ? _tmpCanvas : _tmpCanvas = SetUpCanvas();
            Canvas SetUpCanvas()
            {
                Canvas canvas = new GameObject(nameof(TMPCanvas)).AddComponent<Canvas>();
                canvas.transform.SetParent(GO.transform, false);
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                canvas.sortingOrder = 10;

                if (_tmpCanvasScaler == null)
                {
                    _tmpCanvasScaler = SetUpCanvasScaler(canvas);
                }

                return canvas;
            }
        }
    }

    private CanvasScaler _tmpCanvasScaler;
    public CanvasScaler TMPCanvasScaler
    {
        get
        {
            if (_tmpCanvasScaler == null) { _tmpCanvasScaler = SetUpCanvasScaler(TMPCanvas); }
            return _tmpCanvasScaler;
        }
    }

    CanvasScaler SetUpCanvasScaler(Canvas canvas)
    {
        if (canvas.gameObject.TryGetComponent<CanvasScaler>(out CanvasScaler ca)) { return ca; }

        CanvasScaler cs = canvas.gameObject.AddComponent<CanvasScaler>();
        cs.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        cs.matchWidthOrHeight = 1;
        cs.referenceResolution = new Vector2(Cam.Io.Camera.pixelWidth, Cam.Io.Camera.pixelHeight);

        return cs;
    }

    private TextMeshProUGUI _tmp;
    public TextMeshProUGUI TMP
    {
        get
        {
            return _tmp != null ? _tmp : _tmp = SetUpTMP();

            TextMeshProUGUI SetUpTMP()
            {
                TextMeshProUGUI t = new GameObject(nameof(TMP)).AddComponent<TextMeshProUGUI>();
                t.transform.SetParent(TMPCanvas.transform, true);
                t.fontSizeMin = 8;
                t.fontSizeMax = 300;

                return t;
            }
        }
    }

    private Image _image;
    public Image Image
    {
        get
        {
            return _image = _image != null ? _image : SetUpImage();

            Image SetUpImage()
            {
                Image i = new GameObject(nameof(Image)).AddComponent<Image>();
                i.transform.SetParent(ImageCanvas.transform, true);
                i.sprite = null;
                return i;
            }
        }
    }

    private Canvas _imageCanvas;
    public Canvas ImageCanvas
    {
        get
        {
            return _imageCanvas != null ? _imageCanvas : _imageCanvas = SetUpCanvas();
            Canvas SetUpCanvas()
            {
                Canvas canvas = new GameObject(nameof(ImageCanvas)).AddComponent<Canvas>();
                canvas.transform.SetParent(GO.transform, false);
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                canvas.sortingOrder = 5;

                if (_imageCanvasScaler == null)
                {
                    _imageCanvasScaler = SetUpCanvasScaler(canvas);
                }

                return canvas;
            }
        }
    }

    private CanvasScaler _imageCanvasScaler;
    public CanvasScaler ImageCanvasScaler
    {
        get
        {
            if (_imageCanvasScaler == null) { _imageCanvasScaler = SetUpCanvasScaler(ImageCanvas); }
            return _imageCanvasScaler;
        }
    }


    public string TextString { get => TMP.text; set => TMP.text = value; }
}


