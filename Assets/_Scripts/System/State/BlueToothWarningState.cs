// using System;
using UnityEngine;
using System.Collections;

public class BlueToothWarningState : State
{

    protected override void PrepareState(System.Action callback)
    {
        _ = Sym;
        _ = BT;
        _ = Aud;
        _ = Not;
        _ = Rec;
        base.PrepareState(callback);
    }

    protected override void EngageState()
    {
        FadeOutSFX().StartCoroutine();
    }

    protected override void DisengageState()
    {
        _sym?.SelfDestruct();
        _bt?.SelfDestruct();
        _aud?.SelfDestruct();
        _not?.SelfDestruct();
        _rec?.SelfDestruct();
    }

    float timer = 4f;
    float bt, aud, not, rec;

    IEnumerator FadeOutSFX()
    {
        bt = Random.Range(.04f, .13f);
        aud = Random.Range(.04f, .13f);
        not = Random.Range(.04f, .13f);
        rec = Random.Range(.04f, .13f);

        while (timer > 0f)
        {
            yield return null;

            timer -= Time.deltaTime;

            if ((timer < 3 && timer > 2.6f) || (timer < 2 && timer > 1.2f) || (timer < .8 && timer > .4f))
            {
                if ((bt -= Time.deltaTime) < 0) SetNewBT();
                if ((not -= Time.deltaTime) < 0) SetNewNot();
                if ((aud -= Time.deltaTime) < 0) SetNewAudio();
                if ((rec -= Time.deltaTime) < 0) SetNewRec();
            }
            else
            {
                BT.SetImageSprite(Assets.BT1);
                Not.SetImageSprite(Assets.Not1);
                Aud.SetImageSprite(Assets.Audio1);
                Rec.SetImageSprite(Assets.Recommended1);
            }
        }
        BT.SetImageSprite(Assets.BT4);
        Not.SetImageSprite(Assets.Not4);
        Aud.SetImageSprite(Assets.Audio4);
        Rec.SetImageSprite(Assets.Recommended4);
        SetState(new AuralSplashState() { Fade = true });
    }

    Card _bt;
    Card BT => _bt ??= new Card(nameof(BT), null)
        .SetImageSprite(Assets.BT1)
        .SetImageSize(Cam.UIOrthoX * 2, Cam.UIOrthoY * 2)
        .SetImagePosition(0, 0);

    Card _aud;
    Card Aud => _aud ??= new Card(nameof(Aud), null)
        .SetImageSprite(Assets.Audio1)
        .SetImageSize(Cam.UIOrthoX * 2, Cam.UIOrthoY * 2)
        .SetImagePosition(0, 0);

    Card _not;
    Card Not => _not ??= new Card(nameof(Not), null)
        .SetImageSprite(Assets.Not1)
        .SetImageSize(Cam.UIOrthoX * 2, Cam.UIOrthoY * 2)
        .SetImagePosition(0, 0);

    Card _rec;
    Card Rec => _rec ??= new Card(nameof(Rec), null)
        .SetImageSprite(Assets.Recommended1)
        .SetImageSize(Cam.UIOrthoX * 2, Cam.UIOrthoY * 2)
        .SetImagePosition(0, 0);

    Card _sym;
    Card Sym => _sym ??= new Card(nameof(Sym), null)
        .SetImageSprite(Assets.BTSym)
        .SetImageSize(Cam.UIOrthoX * 1.5f, Cam.UIOrthoY * 1.5f)
        .SetImagePosition(0, 0);

    void SetNewBT()
    {
        bt = Random.Range(.04f, .13f);
        BT.SetImageSprite(Random.Range(0, 5) switch
        {
            0 => Assets.BT1,
            1 => Assets.BT2,
            2 => Assets.BT3,
            3 => Assets.BT4,
            4 => Assets.CLEAR,
            _ => throw new System.Exception()
        });
    }
    void SetNewNot()
    {
        not = Random.Range(.04f, .13f);
        Not.SetImageSprite(Random.Range(0, 5) switch
        {
            0 => Assets.Not1,
            1 => Assets.Not2,
            2 => Assets.Not3,
            3 => Assets.Not4,
            4 => Assets.CLEAR,
            _ => throw new System.Exception()
        });
    }
    void SetNewAudio()
    {
        aud = Random.Range(.04f, .13f);
        Aud.SetImageSprite(Random.Range(0, 5) switch
        {
            0 => Assets.Audio1,
            1 => Assets.Audio2,
            2 => Assets.Audio3,
            3 => Assets.Audio4,
            4 => Assets.CLEAR,
            _ => throw new System.Exception()
        });
    }
    void SetNewRec()
    {
        rec = Random.Range(.04f, .13f);
        Rec.SetImageSprite(Random.Range(0, 5) switch
        {
            0 => Assets.Recommended1,
            1 => Assets.Recommended2,
            2 => Assets.Recommended3,
            3 => Assets.Recommended4,
            4 => Assets.CLEAR,
            _ => throw new System.Exception()
        });
    }

}