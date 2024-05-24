using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MusicTheory;
using Muscopa;

public static class GramoSystems
{
    public static MeshRenderer ScrollDials(this GramoMB gramo, MeshRenderer curSelection, Dir dir)
    {
        return curSelection switch
        {
            _ when curSelection == gramo.AnswerMesh2 => dir == Dir.Up ? gramo.AnswerMesh2 : gramo.AnswerMesh3,
            _ when curSelection == gramo.AnswerMesh3 => dir == Dir.Up ? gramo.AnswerMesh2 : gramo.AnswerMesh4,
            _ when curSelection == gramo.AnswerMesh4 => dir == Dir.Up ? gramo.AnswerMesh3 : gramo.AnswerMesh4,
            _ => throw new System.ArgumentException(curSelection.name)
        };
    }

    public static HarmonicFunction ChangeAnswer(this GramoScene scene, Dir dir)
    {
        var cur = scene.GetAnswer();
        if (scene.GetSpinningBool(scene.CurSelection)) return cur;

        return cur switch
        {
            HarmonicFunction.Secondary => dir == Dir.Left ? HarmonicFunction.Dominant : HarmonicFunction.Tonic,
            HarmonicFunction.Tonic => dir == Dir.Left ? HarmonicFunction.Dominant : HarmonicFunction.Predominant,
            HarmonicFunction.Predominant => dir == Dir.Left ? HarmonicFunction.Tonic : HarmonicFunction.Dominant,
            HarmonicFunction.Dominant => dir == Dir.Left ? HarmonicFunction.Predominant : HarmonicFunction.Tonic,
            _ => throw new System.ArgumentException(cur.ToString())
        };
    }

    public static HarmonicFunction GetAnswer(this GramoScene scene)
    {
        return scene.CurSelection switch
        {
            _ when scene.CurSelection == scene.Gramo.AnswerMesh1 => scene.CurAnswers[0],
            _ when scene.CurSelection == scene.Gramo.AnswerMesh2 => scene.CurAnswers[1],
            _ when scene.CurSelection == scene.Gramo.AnswerMesh3 => scene.CurAnswers[2],
            _ when scene.CurSelection == scene.Gramo.AnswerMesh4 => scene.CurAnswers[3],
            _ => throw new System.ArgumentException(scene.CurSelection.name)
        };
    }

    public static void SetAnswer(this GramoScene scene, HarmonicFunction answer)
    {
        Debug.Log(nameof(SetAnswer) + " " + scene.CurSelection.name + " " + answer);
        if (scene.CurSelection == scene.Gramo.AnswerMesh1) scene.CurAnswers[0] = answer;
        else if (scene.CurSelection == scene.Gramo.AnswerMesh2) scene.CurAnswers[1] = answer;
        else if (scene.CurSelection == scene.Gramo.AnswerMesh3) scene.CurAnswers[2] = answer;
        else if (scene.CurSelection == scene.Gramo.AnswerMesh4) scene.CurAnswers[3] = answer;
    }

    public static bool GetSpinningBool(this GramoScene scene, MeshRenderer dial)
    {
        return dial switch
        {
            _ when dial == scene.Gramo.AnswerMesh2 => scene.Spinning[0],
            _ when dial == scene.Gramo.AnswerMesh3 => scene.Spinning[1],
            _ when dial == scene.Gramo.AnswerMesh4 => scene.Spinning[2],
            _ => throw new System.ArgumentException(scene.CurSelection.name)
        };
    }


    public static void SetSpinningBool(this GramoScene scene, MeshRenderer dial, bool tf)
    {
        if (dial == scene.Gramo.AnswerMesh2) scene.Spinning[0] = tf;
        else if (dial == scene.Gramo.AnswerMesh3) scene.Spinning[1] = tf;
        else if (dial == scene.Gramo.AnswerMesh4) scene.Spinning[2] = tf;
    }

    public static bool AllAnswered(this GramoScene scene)
    {
        foreach (var ans in scene.CurAnswers)
            if (ans == HarmonicFunction.Secondary)
                return false;
        return true;
    }

    public static bool CorrectAnswers(this GramoScene scene)
    {
        for (int i = 0; i < scene.AnswerSheet.Length; i++)
            if (scene.AnswerSheet[i] != scene.CurAnswers[i])
                return false;
        return true;
    }

    public static void DollyAnimation(this GramoScene scene, System.Action callback)
    {
        scene.Gramo.transform.position = new Vector3(Cam.Io.Camera.transform.position.x, Cam.Io.Camera.transform.position.y, 100);
        MonoHelper.Io.StartCoroutine(Dolly());

        IEnumerator Dolly()
        {
            const float moveSpeed = 75f;
            yield return new WaitForEndOfFrame();

            if (Z() < 12.1f)
            {
                scene.Gramo.transform.position = new Vector3(scene.Gramo.transform.position.x, scene.Gramo.transform.position.y, 12);
                callback?.Invoke();
            }

            else
            {
                scene.Gramo.transform.position = new Vector3(scene.Gramo.transform.position.x, scene.Gramo.transform.position.y, Z());
                MonoHelper.Io.StartCoroutine(Dolly());
            }

            float Z() => scene.Gramo.transform.position.z - (Time.deltaTime * moveSpeed);
        }
    }

    internal static IEnumerator SpinLeft(this GramoScene scene, MeshRenderer dial)
    {
        Debug.Log(nameof(SpinLeft));
        const float rotSpeed = 3f;
        scene.SetSpinningBool(scene.CurSelection, true);

        while (dial.material.mainTextureOffset.x < -.75f)
        {
            dial.material.mainTextureOffset =
                new Vector3(dial.material.mainTextureOffset.x +
                (Time.deltaTime * rotSpeed), 0, 0);
            yield return new WaitForEndOfFrame();
        }

        dial.material = NewAnswerMat(scene, dial);
        dial.material.mainTextureOffset = Vector3.left * 3.25f;
        MonoHelper.Io.StartCoroutine(scene.SpinLeftReturn(dial));
    }

    internal static IEnumerator SpinLeftReturn(this GramoScene scene, MeshRenderer dial)
    {
        Debug.Log(nameof(SpinLeftReturn));
        const float rotSpeed = 3f;
        while (dial.material.mainTextureOffset.x < -2)
        {

            dial.material.mainTextureOffset =
                new Vector3(dial.material.mainTextureOffset.x +
                (Time.deltaTime * rotSpeed), 0, 0);

            yield return new WaitForEndOfFrame();
        }

        dial.material.mainTextureOffset = Vector3.left * 2;
        scene.SetSpinningBool(dial, false);
    }

    internal static IEnumerator SpinRight(this GramoScene scene, MeshRenderer dial)
    {
        Debug.Log(nameof(SpinRight));
        const float rotSpeed = 3f;
        scene.SetSpinningBool(scene.CurSelection, true);

        if (Mathf.Approximately(dial.material.mainTextureOffset.x, 0))
            dial.material.mainTextureOffset = Vector3.right * -3f;

        while (dial.material.mainTextureOffset.x > -3f)
        {
            dial.material.mainTextureOffset =
                new Vector3(dial.material.mainTextureOffset.x -
                (Time.deltaTime * rotSpeed), 0, 0);
            yield return new WaitForEndOfFrame();
        }

        dial.material = NewAnswerMat(scene, dial);
        dial.material.mainTextureOffset = Vector3.left * .75f;
        MonoHelper.Io.StartCoroutine(scene.SpinRightReturn(dial));
    }

    internal static IEnumerator SpinRightReturn(this GramoScene scene, MeshRenderer dial)
    {
        Debug.Log(nameof(SpinRightReturn));
        float rotSpeed = 3f;

        while (dial.material.mainTextureOffset.x > -2)
        {
            dial.material.mainTextureOffset =
                 new Vector3(dial.material.mainTextureOffset.x -
                 (Time.deltaTime * rotSpeed), 0, 0);
            yield return new WaitForEndOfFrame();
        }

        dial.material.mainTextureOffset = Vector3.left * 2;
        scene.SetSpinningBool(dial, false);
    }

    internal static Material NewAnswerMat(this GramoScene scene, MeshRenderer dial)
    {
        Debug.Log(nameof(NewAnswerMat));
        var mats = Assets.DialMats;
        return scene.GetAnswer() switch
        {
            HarmonicFunction.Tonic => scene.CurSelection switch
            {
                _ when scene.CurSelection == scene.Gramo.AnswerMesh1 => mats[0][0],
                _ when scene.CurSelection == scene.Gramo.AnswerMesh2 => mats[1][0],
                _ when scene.CurSelection == scene.Gramo.AnswerMesh3 => mats[2][0],
                _ when scene.CurSelection == scene.Gramo.AnswerMesh4 => mats[3][0],
                _ => throw new System.ArgumentException(dial.material.name)
            },

            HarmonicFunction.Predominant => scene.CurSelection switch
            {
                _ when scene.CurSelection == scene.Gramo.AnswerMesh1 => mats[0][1],
                _ when scene.CurSelection == scene.Gramo.AnswerMesh2 => mats[1][1],
                _ when scene.CurSelection == scene.Gramo.AnswerMesh3 => mats[2][1],
                _ when scene.CurSelection == scene.Gramo.AnswerMesh4 => mats[3][1],
                _ => throw new System.ArgumentException(dial.material.name)
            },

            HarmonicFunction.Dominant => scene.CurSelection switch
            {
                _ when scene.CurSelection == scene.Gramo.AnswerMesh1 => mats[0][2],
                _ when scene.CurSelection == scene.Gramo.AnswerMesh2 => mats[1][2],
                _ when scene.CurSelection == scene.Gramo.AnswerMesh3 => mats[2][2],
                _ when scene.CurSelection == scene.Gramo.AnswerMesh4 => mats[3][2],
                _ => throw new System.ArgumentException(dial.material.name)
            },
            _ => throw new System.ArgumentException(dial.material.name)
        };
    }

    internal static void HighlightDial(this GramoScene scene)
    {
        Debug.Log(nameof(HighlightDial));
        // scene.Gramo.Answer1L.material.color = scene.Gramo.AnswerMesh1 == scene.CurSelection ? Color.yellow : Color.white;
        scene.Gramo.Answer2L.material.color = scene.Gramo.AnswerMesh2 == scene.CurSelection ? Color.yellow : Color.white;
        scene.Gramo.Answer3L.material.color = scene.Gramo.AnswerMesh3 == scene.CurSelection ? Color.yellow : Color.white;
        scene.Gramo.Answer4L.material.color = scene.Gramo.AnswerMesh4 == scene.CurSelection ? Color.yellow : Color.white;
        // scene.Gramo.Answer1R.material.color = scene.Gramo.AnswerMesh1 == scene.CurSelection ? Color.yellow : Color.white;
        scene.Gramo.Answer2R.material.color = scene.Gramo.AnswerMesh2 == scene.CurSelection ? Color.yellow : Color.white;
        scene.Gramo.Answer3R.material.color = scene.Gramo.AnswerMesh3 == scene.CurSelection ? Color.yellow : Color.white;
        scene.Gramo.Answer4R.material.color = scene.Gramo.AnswerMesh4 == scene.CurSelection ? Color.yellow : Color.white;
    }

}
