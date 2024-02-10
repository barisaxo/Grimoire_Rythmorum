using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sea;

public static class SeaHUDSystems
{
    public static void Disable(this HUD hud)
    {
        hud.Hud.GO.SetActive(false);
    }
    public static void Enable(this HUD hud)
    {
        hud.Hud.GO.SetActive(true);
    }

    public static void Show(this HUD hud)
    {
        if (hud.HidableHud[0].Image.color.a < 1) hud.CurrentCoroutine ??= RestoreColor();

        IEnumerator RestoreColor()
        {
            float t = hud.HidableHud[0].Image.color.a;
            if (t < 1)
            {
                t += Time.deltaTime;
                Color c = new(1, 1, 1, t);

                foreach (Card card in hud.HidableHud) card.SetImageColor(c).SetTextColor(c);

                yield return null;

                hud.CurrentCoroutine = hud.CurrentCoroutine;
            }
            else
            {
                foreach (Card card in hud.HidableHud) card.SetImageColor(Color.white).SetTextColor(Color.white);

                hud.CurrentCoroutine = null;
            }
        }
    }

    public static void Hide(this HUD hud, float t)
    {
        Color c = new(1, 1, 1, t);
        foreach (Card card in hud.HidableHud) card.SetImageColor(c).SetTextColor(c);
    }

    public static HUD SetNorthToInteract(this HUD hud)
    {
        hud.North.SetTextString("Interact")
                 .SetImageColor(Color.white);
        return hud;
    }

    public static HUD HideNorth(this HUD hud)
    {
        hud.North.SetTextString("")
                 .SetImageColor(Color.clear);
        return hud;
    }

    public static void UpdateCoords(this HUD hud, Vector2Int globalCoord, int globalMapSize, Vector2Int regionCoord)
    {
        hud.CoordText.TextString =
           Math.Round(Mathf.Abs((float)((float)(globalMapSize * .25f) - (float)(globalCoord.y * .5f))), 2).ToString() +
           (globalCoord.y > (globalMapSize * .5f) ? "ºN" : "ºS")
            + " : " +
           Math.Round(Mathf.Abs((float)((float)(globalMapSize * .5f) - globalCoord.x)), 2).ToString() +
           (globalCoord.x > (globalMapSize * .5f) ? "ºE" : "ºW")
        //    + "\n" + regionCoord.x + " : " + regionCoord.y
           ;
    }

    public static void BlipRegionUpdate(this HUD hud, MusicTheory.RegionalMode region)
    {
        if (region == (MusicTheory.RegionalMode)(-1))
            hud.Header.SetTextString("Uncontrolled" + waters);

        else hud.Header.SetTextString(region + waters);

        hud.HeaderTimer = hud.HeaderActiveTime;
        MonoHelper.OnUpdate += hud.TickHudHeaderTimer;
    }

    public readonly static string waters = " waters";
}
