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

    void Swell()
    {
        int i = 0;

        for (int x = 0; x < Sea.SeaGridSize; x++)
        {
            for (int z = 0; z < Sea.SeaGridSize; z++)
            {
                float yPos = Mathf.Sin((Time.time * SwellFrequency) + ((x + z) % (i + 1))) * SwellAmplitude; // triangle wave i think

                Sea.SeaGrid[i].GO.transform.position =
                    new Vector3(Sea.SeaGrid[i].Loc.x, yPos, Sea.SeaGrid[i].Loc.z);

                float v = 2.5f * yPos;
                Sea.SeaGrid[i].Mesh.material.color = new Color(
                         Sea.SeaColor.r + v,
                         Sea.SeaColor.g + v,
                         Sea.SeaColor.b + v,
                         .25f + v);

                i++;
            }
        }
    }
}
