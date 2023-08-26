using UnityEngine;

public class Cam
{
    #region INSTANCE
    private Cam()
    {
        _ = Camera;
        _ = AudioListener;
    }

    public static Cam Io => Instance.Io;

    private class Instance
    {
        static Instance() { }
        static Cam _io;
        internal static Cam Io => _io ??= new Cam();
        internal static void Destruct() => _io = null;
    }

    public void SelfDestruct()
    {
        Object.Destroy(_cam.gameObject);
        Instance.Destruct();
    }
    #endregion INSTANCE

    public static float OrthoX => Io.Camera.orthographicSize * Io.Camera.aspect;
    public static float OrthoY => Io.Camera.orthographicSize;

    private Camera _cam;
    public Camera Camera
    {
        get
        {
            return _cam != null ? _cam : _cam = SetUpCam();
            static Camera SetUpCam()
            {
                Camera c = Object.FindObjectOfType<Camera>() != null ? Object.FindObjectOfType<Camera>() :
                    new GameObject(nameof(Camera)).AddComponent<Camera>();
                Object.DontDestroyOnLoad(c);
                c.orthographicSize = 5;
                c.orthographic = false;
                c.transform.position = Vector3.back * 10;
                c.backgroundColor = new Color(Random.value * .25f, Random.value * .15f, Random.value * .2f);

                return c;
            }
        }
    }

    public void SetObliqueness(Vector2 v2) => SetObliqueness(v2.x, v2.y);
    public void SetObliqueness(float horizObl, float vertObl)
    {
        Matrix4x4 mat = Io.Camera.projectionMatrix;
        mat[0, 2] = horizObl;
        mat[1, 2] = vertObl;
        Io.Camera.projectionMatrix = mat;
        //https://docs.unity3d.com/Manual/ObliqueFrustum.html
    }

    private AudioListener _audioListener;
    public AudioListener AudioListener => _audioListener != null ? _audioListener :
        _audioListener = Camera.gameObject.AddComponent<AudioListener>();
}
