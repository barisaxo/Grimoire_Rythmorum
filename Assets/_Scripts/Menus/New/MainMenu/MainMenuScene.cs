using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScene : IMenuScene
{
    public string Name => nameof(MainMenuScene);

    public void Initialize()
    {
        East.SetTextString("Confirm").SetImageColor(Color.white);
        _ = Quit;
        ((IMenuScene)this).SetCardPos1(East);
        _ = LightHouse;
        _ = CatBoat;
        _ = Title;
        RockTheBoat.AddBoat(CatBoat.transform, (.08f, 1, 0));
        RockTheBoat.Rocking = true;
        MonoHelper.OnUpdate += RotateLightHouse;


        if (Audio.AudioManager.Io.BGMusic.GetClip != Assets.BGMus1)
            Audio.AudioManager.Io.BGMusic.SetClip(Assets.BGMus1);

        if (!Audio.AudioManager.Io.BGMusic.AudioSources[0].isPlaying)
        {
            Audio.AudioManager.Io.BGMusic.Play(false);
            Audio.AudioManager.Io.BGMusic.Loop = true;
        }
    }

    public void SelfDestruct()
    {
        Hud?.SelfDestruct();
        Hud = null;
        South = null;
        West = null;
        East = null;
        North = null;
        L1 = null;
        R1 = null;
        RockTheBoat.Rocking = false;
        MonoHelper.OnUpdate -= RotateLightHouse;
        _title?.SelfDestruct();
        Audio.AudioManager.Io.BGMusic.Pause();
        Object.Destroy(_parent.gameObject);
    }

    private Transform _parent;
    public Transform TF =>
        _parent ? _parent : _parent = new GameObject(nameof(MainMenuScene) + ".TF").transform;

    public readonly RockTheBoat RockTheBoat = new();
    private float LightRotY = -40;

    private GameObject lightHouse;
    public GameObject LightHouse
    {
        get
        {
            return lightHouse != null ? lightHouse : lightHouse = SetUpLightHouse();

            GameObject SetUpLightHouse()
            {
                GameObject lh = new(nameof(LightHouse));
                lh.transform.SetParent(TF.transform);
                lh.transform.position = new Vector3(0, -1.5f, -8);

                for (var i = 0; i < 2; i++)
                {
                    var light = new GameObject(nameof(Light) + i).AddComponent<Light>();
                    // light.lightmapBakeType = LightmapBakeType.Baked;
                    // light.renderMode = LightRenderMode.Auto;
                    light.transform.SetParent(lh.transform);
                    light.transform.SetPositionAndRotation(
                        lh.transform.position,
                        Quaternion.Euler(new Vector3(0, i * 180, 0)));
                    light.type = LightType.Spot;
                    light.range = 40;
                    light.spotAngle = 65;
                    light.intensity = 5;
                    light.shadows = LightShadows.Soft;
                    light.color = new Color(Random.Range(.85f, .95f),
                        Random.Range(.5f, .6f),
                        Random.Range(.05f, .15f));
                }

                return lh;
            }
        }
    }

    private GameObject _catBoat;
    public GameObject CatBoat
    {
        get
        {
            return _catBoat != null ? _catBoat : _catBoat = SetUpCatBoat();

            GameObject SetUpCatBoat()
            {
                var go = Assets.Sloop.gameObject;
                go.transform.SetParent(TF);
                go.transform.SetLocalPositionAndRotation(new Vector3(-2, -1.5f, 0), Quaternion.Euler(0, 180, 0));
                // go.transform.position = new Vector3(-2, -1.5f, 0);
                go.transform.localScale = Vector3.one * 3;
                return go;
            }
        }
    }

    private void RotateLightHouse()
    {
        LightRotY += Time.deltaTime * 25;
        LightHouse.transform.rotation = Quaternion.Euler(0, LightRotY, 0);
    }

    private Card _title;
    public Card Title => _title ??= new Card(nameof(Title), null)
        .SetSprite(Assets.Title)
        .SetSpriteSize(new Vector2(Cam.UIOrthoY, Cam.UIOrthoY) * 10)
        .SetSpritePosition(new Vector3(0, 0, 20));

    public Card Hud { get; set; }
    public Card North { get; set; }
    public Card East { get; set; }
    public Card South { get; set; }
    public Card West { get; set; }
    public Card L1 { get; set; }
    public Card R1 { get; set; }
    private Card _quit;
    public Card Quit => _quit ??= Hud.CreateChild(nameof(Quit), Hud.Canvas)
        .SetTextString("Quit <voffset=-0.07em><size=200%>-<size=75%> + <size=150%>+")
        .SetTMPPosition(-Cam.UIOrthoX + 1f, -Cam.UIOrthoY + .5f);
}


