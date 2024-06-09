using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Swells
{
    public Swells(Sea.WorldMapScene scene) { Scene = scene; }
    readonly Sea.WorldMapScene Scene;
    const float SwellFrequency = .58f;
    const float SwellAmplitude = .06f;

    public void EnableSwells() => MonoHelper.OnUpdate += Swell;
    public void DisableSwells() => MonoHelper.OnUpdate -= Swell;

    private float _offset = 0;
    public float Offset
    {
        get => _offset;
        set => _offset = value >= 1700f ? value - 1700 : value <= -1700f ? value + 1700 : value;
        //1700 is an arbitrary large number
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
            tile.SR.color = new Color(
                     Scene.Board.SeaColor.r + v,
                     Scene.Board.SeaColor.g + v,
                     Scene.Board.SeaColor.b + v,
                     .25f + v);

            if (tile.GO.transform.position.x < Scene.Board.HalfSize + .25f &&
                tile.GO.transform.position.x > Scene.Board.HalfSize - .25f &&
                tile.GO.transform.position.z < Scene.Board.HalfSize + .25f &&
                tile.GO.transform.position.z > Scene.Board.HalfSize - .25f)
            {
                yPos = Mathf.Sin(set + Offset + ((Scene.Ship.GO.transform.position.x + Scene.Ship.GO.transform.position.z) % (i))) * SwellAmplitude;
                Scene.Ship.GO.transform.position = new Vector3(Scene.Ship.GO.transform.position.x, yPos, Scene.Ship.GO.transform.position.z);
            }
            i++;
        }
    }





}
