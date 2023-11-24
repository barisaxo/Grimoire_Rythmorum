using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Swells
{
    public Swells(SeaScene sea) { Sea = sea; }
    readonly SeaScene Sea;
    const float SwellFrequency = .58f;
    const float SwellAmplitude = .06f;

    public void EnableSwells() => MonoHelper.OnUpdate += Swell;
    public void DisableSwells() => MonoHelper.OnUpdate -= Swell;

    private float _offset = 0;
    public float Offset
    {
        get => _offset;
        set => _offset = value >= 700f ? value - 700 : value <= -700f ? value + 700 : value;
    }
    void Swell()
    {
        int ii = Sea.SeaGridSize;
        float set = (Time.time * SwellFrequency) + (Sea.SeaGridSize * .5f);
        float yPos;

        foreach (SeaGridTile tile in Sea.Board)
        {
            // triangle wave i think
            yPos = Mathf.Sin(set + Offset + ((tile.GO.transform.position.x + tile.GO.transform.position.z) % (ii))) * SwellAmplitude;

            tile.GO.transform.position =
                new Vector3(tile.GO.transform.position.x, yPos, tile.GO.transform.position.z);

            float v = 2.5f * yPos;
            tile.Mesh.material.color = new Color(
                     Sea.SeaColor.r + v,
                     Sea.SeaColor.g + v,
                     Sea.SeaColor.b + v,
                     .25f + v);

            if (tile.GO.transform.position.x < 5.7 && tile.GO.transform.position.x > 5.3 && tile.GO.transform.position.z < 5.7 && tile.GO.transform.position.z > 5.3)
            {
                yPos = Mathf.Sin(set + Offset + ((Sea.Ship.GO.transform.position.x + Sea.Ship.GO.transform.position.z) % (ii))) * SwellAmplitude;
                Sea.Ship.GO.transform.position = new Vector3(Sea.Ship.GO.transform.position.x, yPos + .13f, Sea.Ship.GO.transform.position.z);
            }
            ii++;
        }
    }
}
