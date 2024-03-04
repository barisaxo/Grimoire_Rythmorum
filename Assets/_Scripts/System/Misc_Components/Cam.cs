using UnityEngine;

public class Cam
{
    #region INSTANCE
    private Cam()
    {
        _ = Camera;
        _ = UICamera;
        _ = UI3DCamera;
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

    public static float MainOrthoX => Io.Camera.orthographicSize * Io.Camera.aspect;
    public static float MainOrthoY => Io.Camera.orthographicSize;

    public static float UIOrthoX => Io.UICamera.orthographicSize * Io.Camera.aspect;
    public static float UIOrthoY => Io.UICamera.orthographicSize;

    public static Vector3 StoredCamRot;
    public static Vector3 StoredCamPos;

    private Camera _cam;
    public Camera Camera
    {
        get
        {
            return _cam != null ? _cam : _cam = SetUpCam();
            static Camera SetUpCam()
            {
                Camera c = GameObject.Find("Camera") != null ? GameObject.Find("Camera").GetComponent<Camera>() :
                   Object.Instantiate(Resources.Load<Camera>("Prefabs/Cameras/Camera"));
                Object.DontDestroyOnLoad(c);
                c.orthographic = false;
                c.fieldOfView = 60;
                c.transform.position = Vector3.back * 10;
                c.backgroundColor = new Color(Random.Range(.9f, 1f), Random.Range(.8f, 1f), Random.Range(.85f, 1f));
                c.gameObject.SetActive(true);
                return c;
            }
        }
    }

    private Camera _uiCam;
    public Camera UICamera
    {
        get
        {
            return _uiCam != null ? _uiCam : _uiCam = SetUpCam();
            static Camera SetUpCam()
            {
                Camera c = GameObject.Find("UICamera") != null ? GameObject.Find("UICamera").GetComponent<Camera>() :
                   Object.Instantiate(Resources.Load<Camera>("Prefabs/Cameras/UICamera"));
                Object.DontDestroyOnLoad(c);
                c.orthographicSize = 5;
                c.orthographic = true;
                c.transform.position = Vector3.back * 10;
                c.gameObject.SetActive(true);
                return c;
            }
        }
    }

    private Camera _ui3DCam;
    public Camera UI3DCamera
    {
        get
        {
            return _ui3DCam != null ? _ui3DCam : _ui3DCam = SetUpCam();
            static Camera SetUpCam()
            {
                Camera c = GameObject.Find("UIGOCamera") != null ? GameObject.Find("UIGOCamera").GetComponent<Camera>() :
                   Object.Instantiate(Resources.Load<Camera>("Prefabs/Cameras/UIGOCamera"));
                Object.DontDestroyOnLoad(c);
                c.orthographicSize = 5;
                c.orthographic = true;
                c.transform.position = Vector3.back * 1000;
                c.gameObject.SetActive(true);
                return c;
            }
        }
    }

    private Canvas _ui3dCanvas;
    public Canvas UI3DCanvas => _ui3dCanvas != null ? _ui3dCanvas : _ui3dCanvas = UI3DCamera.GetComponentInChildren<Canvas>();

    private AudioListener _audioListener;
    public AudioListener AudioListener => _audioListener != null ? _audioListener :
         Camera.TryGetComponent(out AudioListener ao) ? _audioListener = ao :
        _audioListener = Camera.gameObject.AddComponent<AudioListener>();

    public void SetObliqueness(Vector2 v2) => SetObliqueness(v2.x, v2.y);
    public void SetObliqueness(float horizObl, float vertObl)
    {
        Matrix4x4 mat = Io.Camera.projectionMatrix;
        mat[0, 2] = horizObl;
        mat[1, 2] = vertObl;
        Io.Camera.projectionMatrix = mat;
    }

}




//SetObliqueness(1, 2);
//void SetObliqueness(float horizObl, float vertObl)
//{
//    Matrix4x4 mat = c.projectionMatrix;
//    mat[0, 2] = horizObl;
//    mat[1, 2] = vertObl;
//    c.projectionMatrix = mat;
//https://docs.unity3d.com/Manual/ObliqueFrustum.html
//}
