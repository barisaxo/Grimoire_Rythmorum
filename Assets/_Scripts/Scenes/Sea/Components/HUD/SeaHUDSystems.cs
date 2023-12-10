using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SeaHUDSystems
{


    public static void Show(this SeaHUD hud)
    {
        if (hud.HUD.Children[0].Image.color.a < 1) hud.CurrentCoroutine ??= RestoreColor();

        IEnumerator RestoreColor()
        {
            float t = hud.HUD.Children[0].Image.color.a;
            while (t < 1)
            {
                t += Time.deltaTime;
                Color c = new(1, 1, 1, t);

                foreach (Card card in hud.HUD.Children) card.SetImageColor(c).SetTextColor(c);

                yield return null;
            }
            foreach (Card card in hud.HUD.Children) card.SetImageColor(Color.white).SetTextColor(Color.white);

            hud.CurrentCoroutine = null;
        }
    }

    public static void Hide(this SeaHUD hud)
    {
        if (hud.HUD.Children[0].Image.color.a > 0) hud.CurrentCoroutine ??= RemoveColor();
        IEnumerator RemoveColor()
        {
            float t = hud.HUD.Children[0].Image.color.a;
            while (t > 0)
            {
                t -= Time.deltaTime;
                Color c = new(1, 1, 1, t);

                foreach (Card card in hud.HUD.Children) card.SetImageColor(c).SetTextColor(c);

                yield return null;
            }

            Color solidClear = new(1, 1, 1, 0);
            foreach (Card card in hud.HUD.Children) card.SetImageColor(solidClear).SetTextColor(solidClear);

            hud.CurrentCoroutine = null;
        }
    }

    public static SeaHUD SetNorthToInteract(this SeaHUD hud)
    {
        hud.North.SetTextString("Interact")
                 .SetImageColor(Color.white);
        return hud;
    }

    public static SeaHUD HideNorth(this SeaHUD hud)
    {
        hud.North.SetTextString("")
                 .SetImageColor(Color.clear);
        return hud;
    }


}
