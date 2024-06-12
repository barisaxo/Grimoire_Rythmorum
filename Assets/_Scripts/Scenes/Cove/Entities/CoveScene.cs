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
        _ = Sloop;
        _ = Cutter;
        _ = Brig;
        _ = Schooner;
        _ = Frigate;
        _ = Barque;
        _ = Gramo;
        _ = Bottle;
        _ = Fish;
        _ = HUD;
        _ = Cannon;
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
        HUD.SelfDestruct();
        RockTheBoat.SelfDestruct();
        if (Parent) Object.Destroy(Parent.gameObject);
        Instance.Destruct();
        PatternViewer.SelfDestruct();
    }
    #endregion INSTANCE

    private Cove.ButtonHUD _hud;
    public Cove.ButtonHUD HUD => _hud ??= new();

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

    private GameObject _sloop;
    public GameObject Sloop => _sloop ? _sloop : _sloop = SetUpSloop();

    private GameObject _cutter;
    public GameObject Cutter => _cutter ? _cutter : _cutter = SetUpCutter();


    private GameObject _schooner;
    public GameObject Schooner => _schooner ? _schooner : _schooner = SetUpSchooner();

    private GameObject _brig;
    public GameObject Brig => _brig ? _brig : _brig = SetUpBrig();

    private GameObject _frigate;
    public GameObject Frigate => _frigate ? _frigate : _frigate = SetUpFrigate();

    private GameObject _barque;
    public GameObject Barque => _barque ? _barque : _barque = SetUpBarque();

    private GameObject _skillSheet;
    public GameObject SkillSheet => _skillSheet ? _skillSheet : _skillSheet = SetUpSkillSheet();

    private GameObject _cannon;
    public GameObject Cannon => _cannon ? _cannon : _cannon = SetUpCannon();

    private GameObject SetUpCannon()
    {
        var go = Assets.Cannon;
        go.transform.SetParent(Parent);
        go.transform.SetLocalPositionAndRotation(new Vector3(26.5f, 1.6f, 14), Quaternion.Euler(0, 90, 0));
        go.transform.localScale = Vector3.one;
        return go;
    }

    private GameObject _fish;
    public GameObject Fish => _fish ? _fish : _fish = SetUpFish();

    private GameObject SetUpFish()
    {
        var go = Assets.SailFishPrefab.gameObject;
        go.transform.SetParent(Parent);
        go.transform.SetLocalPositionAndRotation(new Vector3(26.5f, 1.6f, 17), Quaternion.Euler(0, 90, 0));
        go.transform.localScale = Vector3.one;
        return go;
    }

    private GameObject _bottle;
    public GameObject Bottle => _bottle ? _bottle : _bottle = SetUpBottle();

    private GameObject SetUpBottle()
    {
        var go = Assets.Bottle.gameObject;
        go.transform.SetParent(Parent);
        go.transform.SetLocalPositionAndRotation(new Vector3(26.5f, 1.6f, 20), Quaternion.Euler(0, 90, 0));
        go.transform.localScale = Vector3.one;
        return go;
    }

    private GameObject _gramo;
    public GameObject Gramo => _gramo ? _gramo : _gramo = SetUpGramo();

    private GameObject SetUpGramo()
    {
        var go = Assets.Gramo.gameObject;
        go.transform.SetParent(Parent);
        go.transform.SetLocalPositionAndRotation(new Vector3(26.5f, 1.6f, 23), Quaternion.Euler(0, 90, 0));
        go.transform.localScale = Vector3.one;
        return go;
    }

    // private GameObject _skillOrb;
    // public GameObject SkillOrb => _skillOrb ? _skillOrb : _skillOrb = SetUpSkillOrb();

    // private GameObject SetUpSkillOrb()
    // {
    //     throw new System.NotImplementedException();
    // }

    private RockTheBoat _rockTheBoat;
    public RockTheBoat RockTheBoat => _rockTheBoat ??= new();


    private Light SetUpLight()
    {
        // GameObject go = new(nameof(Light));
        // go.transform.SetParent(Parent);
        // go.transform.rotation = Quaternion.Euler(new Vector3(76, 0, 0));
        // Light light = go.AddComponent<Light>();
        // light.type = LightType.Directional;
        // light.shadows = LightShadows.None;
        // light.color = new Color(.9f, .9f, .8f);
        // light.intensity = .35f;


        Light l = new GameObject(nameof(Light)).AddComponent<Light>();
        l.transform.SetParent(Parent);
        l.type = LightType.Directional;
        l.color = new(.9f, .8f, .65f);
        l.shadows = LightShadows.None;
        l.transform.SetPositionAndRotation(Cam.Io.Camera.transform.position, Cam.Io.Camera.transform.rotation);
        l.transform.Rotate(new Vector3(45, 0, 0));
        l.intensity = .35f;

        return l;
    }

    private Light SetUpWLight()
    {
        var l = new GameObject(nameof(WestLight)).AddComponent<Light>();
        l.gameObject.transform.SetParent(Parent);
        l.gameObject.transform.position = new Vector3(24, 5, 17);
        l.gameObject.transform.Rotate(new Vector3(25, 90, 0));
        l.type = LightType.Spot;
        l.spotAngle = 140;
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
        l.spotAngle = 140;
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
        l.spotAngle = 140;
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
        l.spotAngle = 140;
        l.color = new Color(.95f, .5f, .75f);
        l.intensity = 3;
        l.shadows = LightShadows.Soft;
        // l.lightmapBakeType = LightmapBakeType.Baked;
        l.range = 20;
        return l;
    }

    private GameObject SetUpSloop()
    {
        var go = Assets.Sloop.gameObject;
        go.transform.SetParent(Parent);
        go.transform.SetLocalPositionAndRotation(new Vector3(6.5f, 1.6f, 25), Quaternion.Euler(0, 90, 0));
        CapsuleCollider c = go.GetComponentInChildren<CapsuleCollider>();
        c.isTrigger = false;
        go.transform.localScale *= .6f;
        RockTheBoat.AddBoat(go.transform, (.08f, 1, 0));
        RockTheBoat.Rocking = true;
        return go;
    }

    private GameObject SetUpCutter()
    {
        var go = Assets.Cutter.gameObject;
        go.transform.SetParent(Parent);
        go.transform.SetLocalPositionAndRotation(new Vector3(6.5f, 1.6f, 22), Quaternion.Euler(0, 90, 0));
        CapsuleCollider c = go.GetComponentInChildren<CapsuleCollider>();
        c.isTrigger = false;
        go.transform.localScale *= .6f;
        RockTheBoat.AddBoat(go.transform, (.08f, 1, 0));
        RockTheBoat.Rocking = true;
        return go;
    }

    private GameObject SetUpSchooner()
    {
        var go = Assets.Schooner.gameObject;
        go.transform.SetParent(Parent);
        go.transform.SetLocalPositionAndRotation(new Vector3(6.5f, 1.6f, 19), Quaternion.Euler(0, 90, 0));
        CapsuleCollider c = go.GetComponentInChildren<CapsuleCollider>();
        c.isTrigger = false;
        go.transform.localScale *= .6f;
        RockTheBoat.AddBoat(go.transform, (.08f, 1, 0));
        RockTheBoat.Rocking = true;
        return go;
    }

    private GameObject SetUpBrig()
    {
        var go = Assets.Brig.gameObject;
        go.transform.SetParent(Parent);
        go.transform.SetLocalPositionAndRotation(new Vector3(6.5f, 1.6f, 16), Quaternion.Euler(0, 90, 0));
        CapsuleCollider c = go.GetComponentInChildren<CapsuleCollider>();
        c.isTrigger = false;
        go.transform.localScale *= .6f;
        RockTheBoat.AddBoat(go.transform, (.08f, 1, 0));
        RockTheBoat.Rocking = true;
        return go;
    }

    private GameObject SetUpFrigate()
    {
        var go = Assets.Frigate.gameObject;
        go.transform.SetParent(Parent);
        go.transform.SetLocalPositionAndRotation(new Vector3(6.5f, 1.6f, 13), Quaternion.Euler(0, 90, 0));
        CapsuleCollider c = go.GetComponentInChildren<CapsuleCollider>();
        c.isTrigger = false;
        go.transform.localScale *= .6f;
        RockTheBoat.AddBoat(go.transform, (.08f, 1, 0));
        RockTheBoat.Rocking = true;
        return go;
    }

    private GameObject SetUpBarque()
    {
        var go = Assets.Barque.gameObject;
        go.transform.SetParent(Parent);
        go.transform.SetLocalPositionAndRotation(new Vector3(6.5f, 1.6f, 10), Quaternion.Euler(0, 90, 0));
        CapsuleCollider c = go.GetComponentInChildren<CapsuleCollider>();
        c.isTrigger = false;
        go.transform.localScale *= .6f;
        RockTheBoat.AddBoat(go.transform, (.08f, 1, 0));
        RockTheBoat.Rocking = true;
        return go;
    }


    private GameObject SetUpSkillSheet()
    {
        var go = new GameObject(nameof(SkillSheet));
        go.transform.SetParent(Parent);
        go.transform.position = new Vector3(14.5f, 1, 4);
        return go;
    }
}
