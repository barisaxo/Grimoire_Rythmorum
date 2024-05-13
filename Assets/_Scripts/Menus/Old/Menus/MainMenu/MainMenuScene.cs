using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScene : IMenuScene
{
    public void Initialize()
    {

        _ = LightHouse;
        _ = CatBoat;
        RockTheBoat.AddBoat(CatBoat.transform, (.08f, 1, 0));
        RockTheBoat.Rocking = true;
        MonoHelper.OnUpdate += RotateLightHouse;
    }

    public void SelfDestruct()
    {
        RockTheBoat.Rocking = false;
        MonoHelper.OnUpdate -= RotateLightHouse;
        Object.Destroy(_parent.gameObject);
        Resources.UnloadUnusedAssets();
    }

    private Transform _parent;

    public Transform TF =>
        _parent ? _parent : _parent = new GameObject(nameof(MainMenuScene)).transform;

    public readonly RockTheBoat RockTheBoat = new();
    private float LightRotY = -40;

    private GameObject _catBoat;

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
}


