using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaretBlink
{
    Image CaretImage;
    SpriteRenderer CaretSR;
    Color nativeColor = Color.white;
    bool Paused = true;

    public CaretBlink() { }

    public CaretBlink(Image img)
    {
        CaretImage = img;
        nativeColor = img.color;
        Paused = false;
        MonoHelper.OnUpdate += BlinkCarets;
    }

    public CaretBlink(SpriteRenderer sr)
    {
        CaretSR = sr;
        nativeColor = sr.color;
        Paused = false;
        MonoHelper.OnUpdate += BlinkCarets;
    }

    public void SelfDestruct()
    {
        MonoHelper.OnUpdate -= BlinkCarets;
        Paused = true;
        CaretImage = null;
        CaretSR = null;
    }

    public CaretBlink SetCaret(SpriteRenderer sr)
    {
        if (CaretSR != null) CaretSR.color = nativeColor;
        CaretSR = null;

        if (CaretImage != null) CaretImage.color = nativeColor;
        CaretImage = null;

        CaretSR = sr;
        nativeColor = sr.color;
        return this;
    }

    public CaretBlink SetCaret(Image img)
    {
        if (CaretSR != null) CaretSR.color = nativeColor;
        CaretSR = null;

        if (CaretImage != null) CaretImage.color = nativeColor;
        CaretImage = null;

        CaretImage = img;
        nativeColor = img.color;
        return this;
    }

    public CaretBlink Pause()
    {
        if (!Paused)
        {
            MonoHelper.OnUpdate -= BlinkCarets;
            Paused = true;
            if (CaretImage != null) CaretImage.color = nativeColor;
            if (CaretSR != null) CaretSR.color = nativeColor;
        }
        return this;
    }

    public CaretBlink Start()
    {
        if (Paused)
        {
            MonoHelper.OnUpdate += BlinkCarets;
            Paused = false;
        }
        return this;
    }

    public CaretBlink ClearCaret()
    {
        if (CaretImage != null)
        {
            CaretImage.color = nativeColor;
            CaretImage = null;
        }

        else if (CaretSR != null)
        {
            CaretSR.color = nativeColor;
            CaretSR = null;
        }
        return this;
    }

    private void BlinkCarets()
    {
        float delta = (Mathf.Sin(Time.time * 2.5f) * .35f) + .15f;
        Color deltaColor = new(nativeColor.r + delta, nativeColor.g + delta, nativeColor.b + delta, nativeColor.a);
        if (CaretImage != null) CaretImage.color = deltaColor;
        else if (CaretSR != null) CaretSR.color = deltaColor;
    }

}