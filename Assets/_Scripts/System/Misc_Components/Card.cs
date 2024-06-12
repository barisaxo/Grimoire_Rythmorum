using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections;
using System;

public class Card
{
    private Card() { }

    public Card(string name, Transform parent)
    {
        Name = name;
        GO = new GameObject(name);
        GO.transform.SetParent(parent, false);
    }

    private Card(string name, Card parentCard, Transform parent)
    {
        Name = name;
        ParentCard = parentCard;
        Parent = parent;
    }

    private Card(string name, Card parentCard, Transform parent, Canvas _)
    {
        Name = name;
        ParentCard = parentCard;
        Parent = parent;
        _canvas = parentCard.Canvas;
        _canvasScaler = parentCard.CanvasScaler;
    }

    public void SelfDestruct()
    {
        if (Children != null) { foreach (Card child in Children) child.SelfDestruct(); }
        if (GO != null) UnityEngine.Object.Destroy(GO);
        if (_uigo != null) UnityEngine.Object.Destroy(_uigo);
        // if (_uigoCanvas != null) UnityEngine.Object.Destroy(_uigoCanvas);
    }

    public string Name { get; private set; }
    public Card ParentCard { get; private set; } = null;
    public Card[] Children { get; private set; } = null;
    public GameObject GO { get; private set; } = null;
    public Clickable Clickable { get; private set; } = null;
    private readonly Transform Parent;

    public string TextString { get => TMP.text; set => TMP.text = value; }
    private SpriteRenderer _sr = null;
    public SpriteRenderer SpriteRenderer => _sr != null ? _sr : _sr = GO.AddComponent<SpriteRenderer>();

    private TextMeshProUGUI _tmp;
    public TextMeshProUGUI TMP
    {
        get
        {
            return _tmp != null ? _tmp : _tmp = SetUpTMP();

            TextMeshProUGUI SetUpTMP()
            {
                TextMeshProUGUI t = new GameObject(Name + nameof(TMP)).AddComponent<TextMeshProUGUI>();
                t.transform.SetParent(Parent != null ? Parent : Canvas.transform, true);
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
            return _image != null ? _image : _image = SetUpImage();
            Image SetUpImage()
            {
                Image i = new GameObject(Name + nameof(Image)).AddComponent<Image>();
                i.transform.SetParent(Parent != null ? Parent : Canvas.transform, true);
                i.sprite = null;
                return i;
            }
        }
    }

    private Canvas _canvas;
    public Canvas Canvas
    {
        get
        {
            return _canvas != null ? _canvas : _canvas = SetUpCanvas();
            Canvas SetUpCanvas()
            {
                Canvas canvas = new GameObject(Name + nameof(Canvas)).AddComponent<Canvas>();
                if (GO == null)
                {
                    GO = new GameObject(Name);
                    GO.transform.SetParent(Parent, false);
                }
                canvas.transform.SetParent(GO.transform, false);
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                canvas.sortingOrder = 0;
                if (_canvasScaler == null) _canvasScaler = SetUpCanvasScaler(canvas);
                return canvas;
            }
        }
    }

    private GameObject _uigo;
    public GameObject UIGO
    {
        get => _uigo;
        set
        {
            foreach (Transform tf in value.GetComponentsInChildren<Transform>())
                tf.gameObject.layer = 5;

            value.transform.SetParent(Cam.Io.UI3DCanvas.transform, true);
            value.transform.SetPositionAndRotation(new Vector3(0, 0, 10), Quaternion.identity);
            value.transform.localScale = Vector3.one * 500;
            _uigo = value;
        }
    }


    // private Canvas _uigoCanvas;
    // public Canvas UIGOCanvas
    // {
    //     get
    //     {
    //         return _uigoCanvas != null ? _uigoCanvas : _uigoCanvas = SetUpCanvas();
    //         Canvas SetUpCanvas()
    //         {
    //             Canvas canvas = new GameObject(Name + nameof(UIGOCanvas)).AddComponent<Canvas>();
    //             canvas.transform.SetParent(Cam.Io.UI3DCamera.transform, false);
    //             canvas.renderMode = RenderMode.ScreenSpaceOverlay;
    //             canvas.worldCamera = Cam.Io.UI3DCamera;
    //             canvas.sortingOrder = 0;
    //             return canvas;
    //         }
    //     }
    // }

    private CanvasScaler _canvasScaler;
    public CanvasScaler CanvasScaler
    {
        get => _canvasScaler != null ? _canvasScaler : _canvasScaler = SetUpCanvasScaler(Canvas);
    }

    CanvasScaler SetUpCanvasScaler(Canvas canvas)
    {
        if (canvas.gameObject.TryGetComponent(out CanvasScaler ca)) return ca;
        CanvasScaler cs = canvas.gameObject.AddComponent<CanvasScaler>();
        cs.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        cs.matchWidthOrHeight = 1;
        cs.referenceResolution = new Vector2(Cam.Io.Camera.pixelWidth, Cam.Io.Camera.pixelHeight);
        return cs;
    }

    public Card CreateChild(string name, Transform parent)
    {
        Children = Children.Added(new Card(name, this, parent));
        return Children[^1];
    }

    public Card CreateChild(string name, Transform parent, Canvas _)
    {
        Children = Children.Added(new Card(name, this, parent, Canvas));
        return Children[^1];
    }

    public Card CreateChild(string name, Canvas canvas)
    {
        Children = Children.Added(new Card(name, this, canvas.transform, canvas));
        return Children[^1];
    }

    public Card SetClickable(Clickable clickable) { Clickable = clickable; return this; }

    private List<Action> _builderSteps = null;
    public List<Action> BuilderSteps
    {
        get
        {
            if (_builderSteps == null)
            {
                _builderSteps = new();

                WaitAStep().StartCoroutine();

                IEnumerator WaitAStep()
                {
                    yield return null;

                    foreach (Action ac in _builderSteps) ac?.Invoke();

                    _builderSteps = null;
                }
            }
            return _builderSteps;
        }
    }

    public bool CanvasExists => _canvas != null;
    public bool TMPExists => _tmp != null;
    public bool ImageExists => _image != null;
    public bool SRExists => _sr != null;
}


