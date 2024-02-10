// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class UIGO
// {
//     private UIGO() { }

//     public UIGO(string name, GameObject go, Transform parent)
//     {
//         Name = name;
//         GO = go;
//         Parent = parent;

//         GO.transform.SetParent(Canvas.transform);
//     }

//     private UIGO(string name, UIGO parentUIGO, Transform parent)
//     {
//         Name = name;
//         ParentUIGO = parentUIGO;
//         Parent = parent;
//     }

//     private UIGO(string name, UIGO parentUIGO, Transform parent, Canvas _)
//     {
//         Name = name;
//         ParentUIGO = parentUIGO;
//         Parent = parent;
//         _canvas = parentUIGO.Canvas;
//     }

//     public void SelfDestruct()
//     {
//         if (Children != null) { foreach (UIGO child in Children) child.SelfDestruct(); }
//         if (GO != null) GameObject.Destroy(GO);
//     }

//     public string Name { get; private set; }
//     public Transform Parent;

//     public GameObject GO { get; private set; } = null;
//     public UIGO ParentUIGO { get; private set; } = null;
//     public UIGO[] Children { get; private set; } = null;

//     private Canvas _canvas;
//     public Canvas Canvas
//     {
//         get
//         {
//             return _canvas != null ? _canvas : _canvas = SetUpCanvas();
//             Canvas SetUpCanvas()
//             {
//                 Canvas canvas = new GameObject(Name + nameof(Canvas)).AddComponent<Canvas>();
//                 canvas.transform.SetParent(Cam.Io.UI3DCamera.transform, false);
//                 canvas.renderMode = RenderMode.ScreenSpaceCamera;
//                 canvas.worldCamera = Cam.Io.UI3DCamera;
//                 canvas.sortingOrder = 0;
//                 // if (_canvasScaler == null) _canvasScaler = SetUpCanvasScaler(canvas);
//                 return canvas;
//             }
//         }
//     }

//     public UIGO CreateChild(string name, Transform parent)
//     {
//         Children = Children.Add(new UIGO(name, this, parent));
//         return Children[^1];
//     }

//     public UIGO CreateChild(string name, Transform parent, Canvas _)
//     {
//         Children = Children.Add(new UIGO(name, this, parent, Canvas));
//         return Children[^1];
//     }

// }