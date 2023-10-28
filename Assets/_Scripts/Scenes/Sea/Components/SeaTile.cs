using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaTile
{
    public Vector3 Loc { get; private set; }
    public MeshRenderer Mesh { get; private set; }
    public GameObject GO { get; private set; }
    public GameObject OverLay { get; private set; }
    public MeshRenderer MeshOL { get; private set; }
    public GameObject Rocks { get; private set; }
    public GameObject TradeShip { get; private set; }
    //public RockTheBoat TradeShipSway { get; private set; }
    public GameObject GameShip { get; private set; }
    //public RockTheBoat GameShipSway { get; private set; }
    public GameObject Island { get; private set; }

    public SeaTile(Vector3 location, SeaScene sea)
    {
        Loc = location;

        GO = GameObject.CreatePrimitive(PrimitiveType.Cube);
        GO.transform.SetParent(sea.TheSea.transform);
        GO.transform.position = Loc;
        GO.name = nameof(SeaTile) + ": " + Loc.x + ", " + Loc.z;

        Rocks = Object.Instantiate(Assets.Rocks);
        Rocks.transform.SetParent(sea.TheSea.transform);
        Rocks.name = nameof(Rocks) + Loc;
        Rocks.transform.position = Loc + (Vector3.up * .5f);
        Rocks.SetActive(false);

        TradeShip = Object.Instantiate(Assets.Schooner);
        TradeShip.transform.SetParent(GO.transform);
        TradeShip.name = nameof(TradeShip) + Loc;
        TradeShip.transform.position = Loc + (Vector3.up * .6f);
        TradeShip.transform.localScale = Vector3.one * .6f;
        //TradeShipSway = TradeShip.AddComponent<RockTheBoat>();
        sea.RockTheBoat.AddBoat(TradeShip.transform);
        TradeShip.SetActive(false);

        GameShip = Object.Instantiate(Assets.CatBoat);//todo Sloop?
        GameShip.transform.SetParent(GO.transform);
        GameShip.name = nameof(GameShip) + Loc;
        GameShip.transform.position = Loc + (Vector3.up * .6f);
        GameShip.transform.localScale = Vector3.one * .6f;
        //GameShipSway = GameShip.AddComponent<RockTheBoat>();
        sea.RockTheBoat.AddBoat(GameShip.transform);
        GameShip.SetActive(false);

        Island = Object.Instantiate(Assets.Island);
        Island.transform.SetParent(sea.TheSea.transform);
        Island.name = nameof(Island) + Loc;
        Island.transform.position = Loc + (Vector3.up * .5f);
        Island.transform.localScale += Vector3.up * .5f;
        Island.SetActive(false);

        OverLay = GameObject.CreatePrimitive(PrimitiveType.Cube);
        OverLay.transform.SetParent(GO.transform);
        OverLay.transform.localScale = new Vector3(1, .6f, 1);
        OverLay.transform.position = Loc + Vector3.up * .8f;
        OverLay.name = nameof(OverLay) + ": " + Loc.x + ", " + Loc.z;

        Mesh = GO.GetComponent<MeshRenderer>();
        Mesh.material = Assets.Overlay_Mat;
        Mesh.material.color = sea.SeaColor;

        MeshOL = OverLay.GetComponent<MeshRenderer>();
        MeshOL.material = Assets.Overlay_Mat;
        MeshOL.material.color = sea.OverlayWhite;

        OverLay.SetActive(false);
    }

}

