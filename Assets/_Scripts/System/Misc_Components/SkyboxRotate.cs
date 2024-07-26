using UnityEngine;


internal sealed class SkyboxRotate
{
    private SkyboxRotate() { }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void AutoInit()
    {
        Skybox = RenderSettings.skybox = Assets.Stars;
        Skybox.SetFloat(_Rotation, Random.Range(0f, 360f));
        Direction = Random.value < .5f ? 1 : -1;
        MonoHelper.OnUpdate += RotateSkybox;
    }

    static Material Skybox;
    static int Direction;
    const string _Rotation = nameof(_Rotation);

    static void RotateSkybox() => Skybox.SetFloat(_Rotation,
       (Skybox.GetFloat(_Rotation) + (Direction * Time.deltaTime)).Smod(360));
}