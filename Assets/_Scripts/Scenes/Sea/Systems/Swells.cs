using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Swells
{
    public Swells(Sea.Scene scene) { Scene = scene; }
    readonly Sea.Scene Scene;
    const float SwellFrequency = .58f;
    const float SwellAmplitude = .06f;

    public void EnableSwells() => MonoHelper.OnUpdate += Swell;
    public void DisableSwells() => MonoHelper.OnUpdate -= Swell;

    private float _offset = 0;
    public float Offset
    {
        get => _offset;
        set => _offset = value >= 700f ? value - 700 : value <= -700f ? value + 700 : value;
        //700 is an arbitrary large number
    }
    void Swell()
    {
        int i = Scene.Board.SubGridSize;
        float set = (Time.time * SwellFrequency) + (Scene.Board.SubGridSize * .5f);
        float yPos;

        foreach (SubTile tile in Scene.Board.SubTiles)
        {
            // triangle wave (I think)
            yPos = Mathf.Sin(set + Offset + ((tile.GO.transform.position.x + tile.GO.transform.position.z) % i)) * SwellAmplitude;

            tile.GO.transform.position =
                new Vector3(tile.GO.transform.position.x, yPos, tile.GO.transform.position.z);

            float v = 2.5f * yPos;
            tile.Mesh.material.color = new Color(
                     Scene.SeaColor.r + v,
                     Scene.SeaColor.g + v,
                     Scene.SeaColor.b + v,
                     .25f + v);

            if (tile.GO.transform.position.x < 5.7 && tile.GO.transform.position.x > 5.3 && tile.GO.transform.position.z < 5.7 && tile.GO.transform.position.z > 5.3)
            {
                yPos = Mathf.Sin(set + Offset + ((Scene.Ship.GO.transform.position.x + Scene.Ship.GO.transform.position.z) % (i))) * SwellAmplitude;
                Scene.Ship.GO.transform.position = new Vector3(Scene.Ship.GO.transform.position.x, yPos + .13f, Scene.Ship.GO.transform.position.z);
            }
            i++;
        }
    }





}
