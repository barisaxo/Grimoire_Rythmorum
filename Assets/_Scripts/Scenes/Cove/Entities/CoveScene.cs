using UnityEngine;

public class CoveScene
{
    #region INSTANCE
    public static CoveScene Io => Instance.Io;

    private CoveScene()
    {
        _ = PracticeRoom;
        _ = Player;
        _ = Light;
        _ = NorthLight;
        _ = SouthLight;
        _ = WestLight;
        _ = EastLight;
        // _ = SkillOrb;
        _ = Ship;
        PatternViewer.Init();
    }

    private class Instance
    {
        static Instance() { }
        static CoveScene _io;
        internal static CoveScene Io => _io ??= new CoveScene();
        internal static void Destruct() => _io = null;
    }

    public void SelfDestruct()
    {
        RockTheBoat.SelfDestruct();
        if (Parent) Object.Destroy(Parent.gameObject);
        Instance.Destruct();
        PatternViewer.SelfDestruct();
    }
    #endregion INSTANCE

    private PatternViewer _patternViewer;
    public PatternViewer PatternViewer => _patternViewer ??= new();

    private Transform _parent;
    private Transform Parent => _parent ? _parent : _parent = new GameObject(nameof(Parent)).transform;

    private PracticeRoom _practiceRoom;
    public PracticeRoom PracticeRoom => _practiceRoom ??= new(Parent);

    private Player _player;
    public Player Player => _player ??= new(Parent);

    private Light _nlight;
    public Light NorthLight => _nlight ? _nlight : _nlight = SetUpNLight();
    private Light _slight;
    public Light SouthLight => _slight ? _slight : _slight = SetUpSLight();

    private Light _wlight;
    public Light WestLight => _wlight ? _wlight : _wlight = SetUpWLight();

    private Light _elight;
    public Light EastLight => _elight ? _elight : _elight = SetUpELight();

    private Light _light;
    public Light Light => _light ? _light : _light = SetUpLight();

    private GameObject _ship;
    public GameObject Ship => _ship ? _ship : _ship = SetUpShip();

    // private GameObject _skillOrb;
    // public GameObject SkillOrb => _skillOrb ? _skillOrb : _skillOrb = SetUpSkillOrb();

    private GameObject SetUpSkillOrb()
    {
        throw new System.NotImplementedException();
    }

    public RockTheBoat RockTheBoat;


    private Light SetUpLight()
    {
        GameObject go = new GameObject(nameof(Light));
        go.transform.SetParent(Parent);
        go.transform.rotation = Quaternion.Euler(new Vector3(66, 0, 0));
        Light light = go.AddComponent<Light>();
        light.type = LightType.Directional;
        light.color = new Color(.9f, .9f, .8f);
        light.intensity = .45f;
        return light;
    }

    private Light SetUpWLight()
    {
        var l = new GameObject(nameof(WestLight)).AddComponent<Light>();
        l.gameObject.transform.SetParent(Parent);
        l.gameObject.transform.position = new Vector3(24, 5, 17);
        l.gameObject.transform.Rotate(new Vector3(25, 90, 0));
        l.type = LightType.Spot;
        l.spotAngle = 110;
        l.color = new Color(.95f, .85f, .35f);
        l.intensity = 3;
        l.shadows = LightShadows.Soft;
        // l.lightmapBakeType = LightmapBakeType.Baked;
        l.range = 20;
        return l;
    }

    private Light SetUpELight()
    {
        var l = new GameObject(nameof(EastLight)).AddComponent<Light>();
        l.gameObject.transform.SetParent(Parent);
        l.gameObject.transform.position = new Vector3(9, 5, 17);
        l.gameObject.transform.Rotate(new Vector3(25, -90, 0));
        l.type = LightType.Spot;
        l.spotAngle = 110;
        l.color = new Color(.5f, .95f, .75f);
        l.intensity = 3;
        l.shadows = LightShadows.Soft;
        // l.lightmapBakeType = LightmapBakeType.Baked;
        l.range = 20;
        return l;
    }

    private Light SetUpNLight()
    {
        var l = new GameObject(nameof(NorthLight)).AddComponent<Light>();
        l.gameObject.transform.SetParent(Parent);
        l.gameObject.transform.position = new Vector3(17, 5, 9);
        l.gameObject.transform.Rotate(new Vector3(25, 180, 0));
        l.type = LightType.Spot;
        l.spotAngle = 110;
        l.color = new Color(.65f, .65f, .95f);
        l.intensity = 3;
        l.shadows = LightShadows.Soft;
        // l.lightmapBakeType = LightmapBakeType.Baked;
        l.range = 20;
        return l;
    }

    private Light SetUpSLight()
    {
        var l = new GameObject(nameof(SouthLight)).AddComponent<Light>();
        l.gameObject.transform.SetParent(Parent);
        l.gameObject.transform.position = new Vector3(17, 5, 24);
        l.gameObject.transform.Rotate(new Vector3(25, 0, 0));
        l.type = LightType.Spot;
        l.spotAngle = 110;
        l.color = new Color(.95f, .5f, .75f);
        l.intensity = 3;
        l.shadows = LightShadows.Soft;
        // l.lightmapBakeType = LightmapBakeType.Baked;
        l.range = 20;
        return l;
    }

    private GameObject SetUpShip()
    {
        var go = Assets.Sloop.gameObject;
        go.transform.SetParent(Parent);
        go.transform.SetLocalPositionAndRotation(new Vector3(6.5f, 1.6f, 17), Quaternion.Euler(0, 90, 0));
        CapsuleCollider c = go.GetComponentInChildren<CapsuleCollider>();
        c.isTrigger = false;
        // BoxCollider bc = go.AddComponent<BoxCollider>();
        // bc.size = new Vector3(.4f, 1, 1.5f);
        go.transform.localScale = Vector3.one * 2f;
        RockTheBoat = new();
        RockTheBoat.AddBoat(go.transform, (.08f, 1, 0));
        RockTheBoat.Rocking = true;
        return go;
    }


}
