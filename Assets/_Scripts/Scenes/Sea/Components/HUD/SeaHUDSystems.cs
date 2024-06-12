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
        foreach (Card card in hud.HidableHud)
            card.SetImageColor(
                    new Color(card.Image.color.r, card.Image.color.g, card.Image.color.b, t))
                .SetTextColor(c);
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

    public static void UpdateCoords(this HUD hud, string latLongs)
    {
        hud.CoordText.TextString = latLongs;
    }

    public static void UpdateHealthBar(this HUD hud, int cur, int max)
    {
        hud.HealthBar.SetImageSize(new Vector2((float)((float)cur / (float)max) * 1.6f, .2f))
            .SetImageColor(new Color(1f - (float)((float)cur / (float)max), (float)((float)cur / (float)max), 0))
            .SetTextString(cur + "/" + max);
        ;
    }

    public static void UpdateRations(this HUD hud, int rations)
    {
        hud.RationsText.SetTextString("Rations: " + rations)
            .SetTextColor((float)((float)rations / (float)Data.Manager.Io.ActiveShip.GetLevel(new Data.RationStorage())) switch
            {
                < .2f => Color.red,
                < .35f => new Color(1, .5f, 0),
                _ => Color.white
            });
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
