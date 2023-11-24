using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaGridTile
{
    public Vector3 Loc { get; private set; }
    public MeshRenderer Mesh { get; private set; }
    public GameObject GO { get; private set; }

    public SeaGridTile(Vector3Int location, SeaScene sea)
    {
        Loc = (Vector3)location * (float)(1f / 3f);
        GO = GameObject.CreatePrimitive(PrimitiveType.Quad);
        GO.transform.SetParent(sea.TheSea.transform);
        GO.transform.position = Loc;
        GO.transform.localScale = Vector3.one * (float)(1f / 3f);
        GO.name = nameof(SeaGridTile) + Loc;
        GO.transform.Rotate(new Vector3(90, 90, 0));

        Mesh = GO.GetComponent<MeshRenderer>();
        Mesh.material = Assets.Overlay_Mat;
        Mesh.material.color = sea.SeaColor;
    }
}