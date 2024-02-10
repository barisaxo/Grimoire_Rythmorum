using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SubTile
{
    public Vector3 Loc { get; private set; }
    public SpriteRenderer SR { get; private set; }
    public GameObject GO { get; private set; }

    public SubTile(Vector3Int location, Transform parent, Color color, float subInversion)
    {
        Loc = (Vector3)location * subInversion;

        GO = new GameObject(nameof(SubTile) + Loc);
        GO.transform.SetParent(parent);
        GO.transform.SetPositionAndRotation(Loc, Quaternion.Euler(Vector3.right * 90));
        GO.transform.localScale = Vector3.one * subInversion;

        SR = GO.AddComponent<SpriteRenderer>();
        SR.sprite = Assets.White;
        SR.color = color;
    }

}

// public MeshRenderer Mesh { get; private set; }

// GO = GameObject.CreatePrimitive(PrimitiveType.Quad);
// GO.transform.SetParent(parent);
// GO.transform.SetPositionAndRotation(Loc, Quaternion.Euler(Vector3.right * 90));
// GO.transform.localScale = Vector3.one * (float)(1f / 3f);
// GO.name = nameof(SubTile) + Loc;

// Mesh = GO.GetComponent<MeshRenderer>();
// Mesh.material = Assets.Overlay_Mat;
// Mesh.material.color = color;