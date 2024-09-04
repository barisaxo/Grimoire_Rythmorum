using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fretboard : MonoBehaviour
{
    private static Fretboard io; public static Fretboard Io => io != null ? io :
        io = new GameObject(nameof(Fretboard)).AddComponent<Fretboard>();
    // bool initialized;

    void Start()
    {
        // GM.Io.Cam.orthographicSize = 6f;
        DrawDots();
        DrawStrings();
        DrawFrets();
        // initialized = true;
    }

    private void OnEnable()
    {
        // if (initialized) { GM.Io.Cam.orthographicSize = 6f; }
    }
    private void DrawDots()
    {

    }

    private void DrawStrings()
    {
        for (int i = 0; i < 6; i++)
        {
            var go = new GameObject("String " + Enum.GetName(typeof(GuitarString), i));
            go.transform.SetParent(transform);
            go.transform.position = new Vector3(0, (i * .8f) - 2f, 0);
            go.transform.localScale = new Vector3(15.3f, .1f, 1);
            SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
            sr.sprite = Assets.White;
            sr.color = new Color(1, 1, 1, .65f);

        }
    }

    private void DrawFrets()
    {
        for (int i = 0; i < 14; i++)
        {
            var go = new GameObject("Fret " + i);
            go.transform.SetParent(transform);
            go.transform.position = new Vector3(FretXPos(i) - 7.7f, 0, 0);
            go.transform.localScale = new Vector3(.1f, 4f, 1);
            SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
            sr.sprite = Assets.White;
            sr.color = new Color(1, 1, 1, .65f);

            if (i == 0)//nut of the guitar
            {
                go.transform.localScale = new Vector3(.25f, 4.5f, 1);
                sr.color = new Color(.5f, .5f, .5f, .65f);
                go.transform.Translate(Vector3.back * .1f);
            }
        }
    }

    float FretXPos(int i)
    {
        //*formula: Dn = [(L – Dn - 1) ÷ 17.817] +Dn - 1
        int scale = 29;
        float distance = 0;

        for (int fret = 1; fret <= i; fret++)
        {
            float location = scale - distance;
            float scaling_factor = location / 17.817f;
            distance += scaling_factor;
        }
        return distance;
    }
}

public class GuitarNote
{
    public GuitarString gString;
    public int fret;
    public GuitarNoteName NoteName => GetNoteName();

    private GuitarNoteName GetNoteName()
    {
        return 0;
    }
}

public enum GuitarNoteName
{
    E2, F2, Gb2, G2, Ab2, A2, Bb2, B2,
    C3, Db3, D3, Eb3, E3, F3, Gb3, G3, Ab3, A3, Bb3, B3,
    C4, Db4, D4, Eb4, E4, F4, Gb4, G4, Ab4, A4, Bb4, B4,
    C5, Db5, D5, Eb5, E5,
}

public enum GuitarString { E1 = 1, B = 2, G = 3, D = 4, A = 5, E = 6 }

// public class FretboardState : MasterState
// {
//     Fretboard Fretboard => Fretboard.Io;

//     public override void Engage()
//     {
//         Fretboard.gameObject.SetActive(true);
//     }


//     public override void Disengage()
//     {
//         Fretboard.gameObject.SetActive(false);
//     }


// }

public class Ukulele : MonoBehaviour
{
    private static Ukulele io; public static Ukulele Io => io != null ? io :
         io = new GameObject(nameof(Ukulele)).AddComponent<Ukulele>();

    public List<UkeNote> UkeNotes = new List<UkeNote>();

    // bool initialized;
    void Start()
    {
        // GM.Io.Cam.orthographicSize = 6f;
        DrawDots();
        DrawFretboard();
        // initialized = true;
        transform.position = Vector3.down * 2;
    }
    private void OnEnable()
    {
        // if (initialized) { GM.Io.Cam.orthographicSize = 6f; }
    }
    private void DrawDots()
    {
        //Uke dots are on frets 5, 7, 10, and 12
    }

    private void DrawStrings(float startPosX, float endPosX, int fret)
    {
        for (UkuleleString i = UkuleleString.G; i <= UkuleleString.A; i++)
        {
            UkeNotes.Add(new UkeNote(startPosX, endPosX, fret, i, transform));
        }
    }

    private void DrawFretboard()
    {
        float posA = 0;
        for (int i = 0; i < 13; i++)
        {
            var go = new GameObject("Fret " + i);
            go.transform.SetParent(transform);
            go.transform.position = new Vector3(FretXPos(i) - 6.7f, 0, 0);
            go.transform.localScale = new Vector3(.1f, 4.5f, 1);
            SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
            sr.sprite = Assets.White;
            sr.color = new Color(1, 1, 1, .65f);

            if (i == 0)//nut of the guitar
            {
                go.transform.localScale = new Vector3(.25f, 4.5f, 1);
                sr.color = new Color(.5f, .5f, .5f, .65f);
            }

            DrawStrings(posA, FretXPos(i), i);
            posA = FretXPos(i);
        }
    }

    float FretXPos(int fret)
    {
        int scale = 28;//overall string length
        float distance = 0;

        for (int i = 0; i < fret; i++)
        {
            //*Arithmetic:
            //*location = scale - distance;
            //*scaling_factor = location / 17.817f;
            //*distance += scaling_factor

            distance += (scale - distance) / 17.817f;
        }

        return distance;
    }
}

public class UkeNote
{
    public int Fret;
    public UkuleleString String;
    public UkeNoteName NoteName;
    public SpriteRenderer SR;
    public BoxCollider2D BC;
    public GameObject GO;

    public UkeNote(float startPosX, float endPosX, int fret, UkuleleString uString, Transform parent)
    {
        Fret = fret;
        String = uString;
        NoteName = GetNoteName(NoteName, String, Fret);

        GO = new GameObject(nameof(UkeNote) + " " + NoteName.ToString());
        GO.transform.SetParent(parent);
        GO.transform.position = new Vector3(Mathf.Lerp(startPosX, endPosX, .5f) - 6.7f, ((int)(uString - 1) * 1.4f) - 2.1f, 0);
        GO.transform.localScale = new Vector3(Mathf.Abs(startPosX - endPosX), .08f, 1);

        SR = GO.AddComponent<SpriteRenderer>();
        SR.sprite = Assets.White;
        SR.color = GetColorByNote(NoteName);

        BC = GO.AddComponent<BoxCollider2D>();
        BC.size = new Vector2(1, 5);

        if (fret == 0)
        {
            GO.transform.Translate(Vector3.left);
            GO.transform.localScale = Vector2.one;
            BC.size = Vector2.one;
        }
    }


    public static Color GetColorByNote(UkeNoteName u) => u switch
    {
        UkeNoteName.C4 => Color.cyan,
        UkeNoteName.Db4 => Color.gray,
        UkeNoteName.D4 => Color.yellow,
        UkeNoteName.Eb4 => Color.gray,
        UkeNoteName.E4 => Color.magenta,
        UkeNoteName.F4 => Color.green,
        UkeNoteName.Gb4 => Color.gray,
        UkeNoteName.G4 => Color.red,
        UkeNoteName.Ab4 => Color.gray,
        UkeNoteName.A4 => Color.blue,
        UkeNoteName.Bb4 => Color.gray,
        UkeNoteName.B4 => new Color(1, .5f, .1f, 1),
        UkeNoteName.C5 => Color.cyan,
        UkeNoteName.Db5 => Color.gray,
        UkeNoteName.D5 => Color.yellow,
        UkeNoteName.Eb5 => Color.gray,
        UkeNoteName.E5 => Color.magenta,
        UkeNoteName.F5 => Color.green,
        UkeNoteName.Gb5 => Color.gray,
        UkeNoteName.G5 => Color.red,
        UkeNoteName.Ab5 => Color.gray,
        UkeNoteName.A5 => Color.blue,
        _ => Color.black,
    };



    public static UkeNoteName GetNoteName(UkeNoteName u, UkuleleString uString, int fret)
    {
        switch (uString)
        {
            case UkuleleString.A:
                return fret switch
                {
                    int i when i == 0 => UkeNoteName.A4,
                    int i when i == 1 => UkeNoteName.Bb4,
                    int i when i == 2 => UkeNoteName.B4,
                    int i when i == 3 => UkeNoteName.C5,
                    int i when i == 4 => UkeNoteName.Db5,
                    int i when i == 5 => UkeNoteName.D5,
                    int i when i == 6 => UkeNoteName.Eb5,
                    int i when i == 7 => UkeNoteName.E5,
                    int i when i == 8 => UkeNoteName.F5,
                    int i when i == 9 => UkeNoteName.Gb5,
                    int i when i == 10 => UkeNoteName.G5,
                    int i when i == 11 => UkeNoteName.Ab5,
                    int i when i == 12 => UkeNoteName.A5,
                    _ => UkeNoteName.A4,
                };
            case UkuleleString.E:
                return fret switch
                {
                    int i when i == 0 => UkeNoteName.E4,
                    int i when i == 1 => UkeNoteName.F4,
                    int i when i == 2 => UkeNoteName.Gb4,
                    int i when i == 3 => UkeNoteName.G4,
                    int i when i == 4 => UkeNoteName.Ab4,
                    int i when i == 5 => UkeNoteName.A4,
                    int i when i == 6 => UkeNoteName.Bb4,
                    int i when i == 7 => UkeNoteName.B4,
                    int i when i == 8 => UkeNoteName.C5,
                    int i when i == 9 => UkeNoteName.Db5,
                    int i when i == 10 => UkeNoteName.D5,
                    int i when i == 11 => UkeNoteName.Eb5,
                    int i when i == 12 => UkeNoteName.E5,
                    _ => UkeNoteName.E4,
                };
            case UkuleleString.C:
                return fret switch
                {
                    int i when i == 0 => UkeNoteName.C4,
                    int i when i == 1 => UkeNoteName.Db4,
                    int i when i == 2 => UkeNoteName.D4,
                    int i when i == 3 => UkeNoteName.Eb4,
                    int i when i == 4 => UkeNoteName.E4,
                    int i when i == 5 => UkeNoteName.F4,
                    int i when i == 6 => UkeNoteName.Gb4,
                    int i when i == 7 => UkeNoteName.G4,
                    int i when i == 8 => UkeNoteName.Ab4,
                    int i when i == 9 => UkeNoteName.A4,
                    int i when i == 10 => UkeNoteName.Bb4,
                    int i when i == 11 => UkeNoteName.B4,
                    int i when i == 12 => UkeNoteName.C5,
                    _ => UkeNoteName.C4,
                };
            case UkuleleString.G:
                return fret switch
                {
                    int i when i == 0 => UkeNoteName.G4,
                    int i when i == 1 => UkeNoteName.Ab4,
                    int i when i == 2 => UkeNoteName.A4,
                    int i when i == 3 => UkeNoteName.Bb4,
                    int i when i == 4 => UkeNoteName.B4,
                    int i when i == 5 => UkeNoteName.C5,
                    int i when i == 6 => UkeNoteName.Db5,
                    int i when i == 7 => UkeNoteName.D5,
                    int i when i == 8 => UkeNoteName.Eb5,
                    int i when i == 9 => UkeNoteName.E5,
                    int i when i == 10 => UkeNoteName.F5,
                    int i when i == 11 => UkeNoteName.Gb5,
                    int i when i == 12 => UkeNoteName.G5,
                    _ => UkeNoteName.G4,
                };
        }
        return UkeNoteName.C4;
    }

}
public enum UkuleleString { A = 4, E = 3, C = 2, G = 1 }
public enum UkeNoteName
{
    C4, Db4, D4, Eb4, E4, F4, Gb4, G4, Ab4, A4, Bb4, B4,
    C5, Db5, D5, Eb5, E5, F5, Gb5, G5, Ab5, A5, Null
}

public class UkuleleState : State
{
    Ukulele Ukulele => Ukulele.Io;
    // WaveGenerator WaveGenerator => WaveGenerator.Io;

    // public override void EngageState()
    // {
    //     Ukulele.gameObject.SetActive(true);
    // }

    // public override void DisengageState()
    // {
    //     // WaveGenerator.UniversalNote(UkeNoteName.Null);
    //     Ukulele.gameObject.SetActive(false);
    //     // InputKey.MouseDownEvent -= Clicked;
    // }

    protected override void ClickedOn(GameObject go)
    {
        Debug.Log(go.name);
        foreach (UkeNote note in Ukulele.UkeNotes)
        {
            if (note.GO == go)
            {
                Debug.Log(note.NoteName);
                // WaveGenerator.UniversalNote(note.NoteName);
                return;
            }
        }
        // WaveGenerator.UniversalNote(UkeNoteName.Null);
    }
}
