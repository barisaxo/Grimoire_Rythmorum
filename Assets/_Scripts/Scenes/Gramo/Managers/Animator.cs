// using System.Collections;
// using UnityEngine;


// namespace Gramophone
// {
//     public enum Dial { One, Two, Three, Four }

//     public static class Animator
//     {


//         public static void Enable(this GramoScene scene)
//         {
//             // DollyAnimation();
//             PopulateSpinningList();
//             ResetAllisSpinningToFalse();
//             HighlightDial();
//         }

//         internal static void Disable()
//         {
//             ResetAllisSpinningToFalse();
//         }

//         public static void DollyAnimation(this GramoScene scene, System.Action callback)
//         {
//             scene.Gramo.transform.position = new Vector3(0, 0, 100);
//             MonoHelper.Io.StartCoroutine(Dolly());

//             IEnumerator Dolly()
//             {
//                 float moveSpeed = 75f;
//                 yield return new WaitForEndOfFrame();

//                 if (Z() < 0.1f)
//                 {
//                     scene.Gramo.transform.position = new Vector3(0, -.666f, 0);
//                     callback?.Invoke();
//                 }

//                 else
//                 {
//                     scene.Gramo.transform.position = new Vector3(0, -.666f, Z());
//                     MonoHelper.Io.StartCoroutine(Dolly());
//                 }

//                 float Z() => scene.Gramo.transform.position.z - (Time.deltaTime * moveSpeed);
//             }
//         }

//         // public static void PopulateSpinningList()
//         // {
//         //     for (int i = 0; i < 4; i++)
//         //     {
//         //         Gramo.isSpinning.Add(false);
//         //     }
//         // }

//         // static void ResetAllisSpinningToFalse()
//         // {
//         //     for (int i = 0; i < Gramo.isSpinning.Count; i++)
//         //     {
//         //         Gramo.isSpinning[i] = false;
//         //     }
//         // }

//         internal static IEnumerator SpinLeft(this GramoScene scene, MeshRenderer dial)
//         {
//             float rotSpeed = 3f;
//             yield return new WaitForEndOfFrame();
//             if (scene.Gramo.dialMeshes[dial].material.mainTextureOffset.x < -.75f)
//             {
//                 Gramo.dialMeshes[dial].material.mainTextureOffset =
//                     new Vector3(Gramo.dialMeshes[dial].material.mainTextureOffset.x +
//                     (Time.deltaTime * rotSpeed), 0, 0);
//                 MonoHelper.Io.StartCoroutine(SpinLeft(dial));
//             }
//             else { PrevCard(dial); }
//         }

//         internal static IEnumerator SpinLeftReturn(int dial)
//         {
//             float rotSpeed = 3f;
//             yield return new WaitForEndOfFrame();
//             if (Gramo.dialMeshes[dial].material.mainTextureOffset.x < -2)
//             {
//                 Gramo.dialMeshes[dial].material.mainTextureOffset =
//                     new Vector3(Gramo.dialMeshes[dial].material.mainTextureOffset.x +
//                     (Time.deltaTime * rotSpeed), 0, 0);
//                 MonoHelper.Io.StartCoroutine(SpinLeftReturn(dial));
//             }
//             else
//             {
//                 Gramo.dialMeshes[dial].material.mainTextureOffset = Vector3.left * 2;
//                 Gramo.isSpinning[dial] = false;
//             }
//         }

//         internal static IEnumerator SpinRight(int dial)
//         {
//             float rotSpeed = 3f;
//             yield return new WaitForEndOfFrame();
//             if (Gramo.dialMeshes[dial].material.mainTextureOffset.x > -3.25f)
//             {
//                 Gramo.dialMeshes[dial].material.mainTextureOffset =
//                     new Vector3(Gramo.dialMeshes[dial].material.mainTextureOffset.x -
//                     (Time.deltaTime * rotSpeed), 0, 0);
//                 MonoHelper.Io.StartCoroutine(SpinRight(dial));
//             }
//             else { NextCard(dial); }
//         }

//         internal static IEnumerator SpinRightReturn(int dial)
//         {
//             float rotSpeed = 3f;
//             yield return new WaitForEndOfFrame();
//             if (Gramo.dialMeshes[dial].material.mainTextureOffset.x > -2)
//             {
//                 Gramo.dialMeshes[dial].material.mainTextureOffset =
//                     new Vector3(Gramo.dialMeshes[dial].material.mainTextureOffset.x -
//                     (Time.deltaTime * rotSpeed), 0, 0);
//                 MonoHelper.Io.StartCoroutine(SpinRightReturn(dial));
//             }
//             else
//             {
//                 Gramo.dialMeshes[dial].material.mainTextureOffset = Vector3.left * 2;
//                 Gramo.isSpinning[dial] = false;
//             }
//         }

//         internal static void HighlightDial()
//         {
//             foreach (GameObject go in Gramo.leftArrows)
//             {
//                 go.GetComponent<MeshRenderer>().material.color =
//                     go == Gramo.leftArrows[DialToInt()] ?
//                     Color.yellow : Color.white;
//             }

//             foreach (GameObject go in Gramo.rightArrows)
//             {
//                 go.GetComponent<MeshRenderer>().material.color =
//                     go == Gramo.rightArrows[DialToInt()] ?
//                     Color.yellow : Color.white;
//             }
//         }

//         internal static void NextCard(int dial)
//         {
//             if (Gramo.currentAnswer[dial] == 0)
//             {
//                 Gramo.dialMeshes[dial].material = Gramo.dialMats[dial][1];
//                 Gramo.currentAnswer[dial] = 1;
//             }
//             else if (Gramo.currentAnswer[dial] == 1)
//             {
//                 Gramo.dialMeshes[dial].material = Gramo.dialMats[dial][2];
//                 Gramo.currentAnswer[dial] = 2;
//             }
//             else if (Gramo.currentAnswer[dial] == 2)
//             {
//                 Gramo.dialMeshes[dial].material = Gramo.dialMats[dial][0];
//                 Gramo.currentAnswer[dial] = 0;
//             }

//             Gramo.dialMeshes[dial].material.mainTextureOffset = Vector3.left * .85f;
//             MonoHelper.Io.StartCoroutine(SpinRightReturn(dial));
//         }

//         internal static void PrevCard(int dial)
//         {
//             if (Gramo.currentAnswer[dial] == 0)
//             {
//                 Gramo.dialMeshes[dial].material = Gramo.dialMats[dial][2];
//                 Gramo.currentAnswer[dial] = 2;
//             }
//             else if (Gramo.currentAnswer[dial] == 1)
//             {
//                 Gramo.dialMeshes[dial].material = Gramo.dialMats[dial][0];
//                 Gramo.currentAnswer[dial] = 0;
//             }
//             else if (Gramo.currentAnswer[dial] == 2)
//             {
//                 Gramo.dialMeshes[dial].material = Gramo.dialMats[dial][1];
//                 Gramo.currentAnswer[dial] = 1;
//             }

//             Gramo.dialMeshes[dial].material.mainTextureOffset = Vector3.left * 3.25f;
//             MonoHelper.Io.StartCoroutine(SpinLeftReturn(dial));
//         }

//         internal static void Interact(Dir dir)
//         {
//             switch (dir)
//             {
//                 case Dir.Up: PrevDial(); break;
//                 case Dir.Down: NextDial(); break;

//                 case Dir.Left:
//                     if (!Gramo.isSpinning[DialToInt()])
//                     {
//                         Gramo.isSpinning[DialToInt()] = true;
//                         MonoHelper.Io.StartCoroutine(SpinLeft(DialToInt()));
//                     };
//                     break;

//                 case Dir.Right:
//                     if (!Gramo.isSpinning[DialToInt()])
//                     {
//                         Gramo.isSpinning[DialToInt()] = true;
//                         MonoHelper.Io.StartCoroutine(SpinRight(DialToInt()));
//                     };
//                     break;
//             }
//             HighlightDial();

//             #region INTERNAL
//             void NextDial()
//             {
//                 Gramo.currDial = Gramo.currDial != null ? Gramo.currDial : Gramo.answer2;

//                 Gramo.currDial =
//                     Gramo.currDial == Gramo.answer2 ? Gramo.answer3 :
//                     Gramo.currDial == Gramo.answer3 ? Gramo.answer4 :
//                     Gramo.answer4;
//             }

//             void PrevDial()
//             {
//                 Gramo.currDial =
//                    Gramo.currDial == Gramo.answer4 ? Gramo.answer3 :
//                    Gramo.currDial == Gramo.answer3 ? Gramo.answer2 :
//                    Gramo.answer2;
//             }
//             #endregion
//         }

//         internal static int DialToInt() => Gramo.currDial switch
//         {
//             _ when Gramo.currDial == Gramo.answer2 => 1,
//             _ when Gramo.currDial == Gramo.answer3 => 2,
//             _ when Gramo.currDial == Gramo.answer4 => 3,
//             _ => 1,
//         };
//     }
// }