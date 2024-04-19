using UnityEngine;

public class FPSDisplay
{
    public FPSDisplay()
    {
        for (int i = 0; i < avg.Length; i++) avg[i] = 0;
        MonoHelper.OnUpdate += UpdateFPS;
    }

    private Card _fps;
    public Card FPS => _fps ??= new Card(nameof(FPS), null)
        .SetTMPPosition(-Cam.UIOrthoX + 1, -Cam.MainOrthoY + 1);

    readonly float[] avg = new float[20];

    int _i = 0; int I
    {
        get
        {
            _i = _i + 1 == avg.Length ? 0 : _i + 1;
            return _i;
        }
    }

    void UpdateFPS()
    {
        avg[I] = 1f / Time.deltaTime;
        FPS.SetTextString(getFPS.ToString());
    }

    int getFPS
    {
        get
        {
            float fps = 0;
            for (int i = 0; i < avg.Length; i++) fps += avg[i];
            return (int)(fps / (float)avg.Length);
        }
    }
}