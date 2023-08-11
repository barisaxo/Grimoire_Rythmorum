using System.Collections.Generic;
using MusicTheory.Rhythms;
using UnityEngine;

namespace SheetMusic
{
    public static class SheetMusicScribingSystems
    {
        public static MusicSheet DrawRhythms(this MusicSheet ms)
        {
            ms.SetUpStaff();
            ms.SetUpTimeSig();
            ms.SetUpCounts(ms.SubCountsPerMeasure());
            ms.AssignNoteSprites();
            ms.AssignTies();
            return ms;
        }

        public static void SetUpTimeSig(this MusicSheet ms)
        {
            ms.TimeSig = new Card(nameof(TimeSignature), ms.Parent)
                .SetTextString((int)ms.RhythmSpecs.Time.Signature.Quantity + "\n" + (int)ms.RhythmSpecs.Time.Signature.Quality)
                .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
                .SetTMPPosition(MeasurePos(MeasureNumber.One) + (.56f * Cam.Io.OrthoX() * Vector3.left))
                .SetFontScale(.76f, .76f)
                .AutoSizeFont(true)
                .AutoSizeTextContainer(true)
                ;
        }

        private static void SetUpStaff(this MusicSheet ms)
        {
            ms.ScribedStaves = new Card[ms.Measures.Length == 1 ? 2 : ms.Measures.Length];

            ms.ScribedStaves[0] = new Card("Measure 1 staff", ms.Parent)
                 .SetImageSprite(Assets.StaffDoubleLeft)
                 .SetImageSize(new Vector3(Cam.Io.OrthoX(), Cam.Io.OrthoX() * .7f, 1))
                 .SetImagePosition(MeasurePos(MeasureNumber.One));

            switch (ms.Measures.Length)
            {
                case 1:
                    ms.ScribedStaves[1] = new Card("Measure 1 staff", ms.Parent)
                        .SetImageSprite(Assets.StaffDoubleRight)
                        .SetImageSize(new Vector3(Cam.Io.OrthoX(), Cam.Io.OrthoX() * .7f, 1))
                        .SetImagePosition(MeasurePos(MeasureNumber.One));
                    break;

                case 2:
                    ms.ScribedStaves[1] = new Card("Measure 2 staff", ms.Parent)
                        .SetImageSprite(Assets.StaffDoubleRight)
                        .SetImageSize(new Vector3(Cam.Io.OrthoX(), Cam.Io.OrthoX() * .7f, 1))
                        .SetImagePosition(MeasurePos(MeasureNumber.Two));
                    break;

                case 4:
                    ms.ScribedStaves[1] = new Card("Measure 2 staff", ms.Parent)
                        .SetImageSprite(Assets.Staff)
                        .SetImageSize(new Vector3(Cam.Io.OrthoX(), Cam.Io.OrthoX() * .7f, 1))
                        .SetImagePosition(MeasurePos(MeasureNumber.Two));
                    ms.ScribedStaves[2] = new Card("Measure 3 staff", ms.Parent)
                        .SetImageSprite(Assets.Staff)
                        .SetImageSize(new Vector3(Cam.Io.OrthoX(), Cam.Io.OrthoX() * .7f, 1))
                        .SetImagePosition(MeasurePos(MeasureNumber.Thr));
                    ms.ScribedStaves[3] = new Card("Measure 4 staff", ms.Parent)
                        .SetImageSprite(Assets.StaffDoubleRight)
                        .SetImageSize(new Vector3(Cam.Io.OrthoX(), Cam.Io.OrthoX() * .7f, 1))
                        .SetImagePosition(MeasurePos(MeasureNumber.For));
                    break;
            };
        }

        private static void SetUpCounts(this MusicSheet ms, SubBeatAssignment[] beats)
        {
            ms.ScribedCounts = new Card[beats.Length];

            for (int i = 0; i < ms.Measures.Length; i++)
            {
                int c = 0;
                int count = 0;
                for (int ii = 0; ii < beats.Length; ii++)
                {
                    if (beats[c] == SubBeatAssignment.D) count++;
                    ms.ScribedCounts[c] = new Card(nameof(Count) + ": " + count + beats[c].ToString(), ms.Parent)
                        .SetTextString(beats[c] == SubBeatAssignment.D ? count.ToString() : beats[c].ToString())
                        .SetTMPPosition(ms.NotePosition(new BeatLocation()
                        {
                            BeatAssignment = new()
                            {
                                Count = (Count)count,
                                SubBeatAssignment = beats[c]
                            },
                            MeasureNumber = (MeasureNumber)i + 1,
                        }) + (Vector3.up * Cam.Io.Camera.aspect))
                        .SetFontScale(.4f, .4f)
                        .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
                        .AutoSizeTextContainer(true)
                        .AutoSizeFont(true);

                    if (ms.ScribedCounts[c].TMP.text == "N")
                    {
                        ms.ScribedCounts[c].TMP.text = "+";
                    }

                    c++;
                }
            }
        }

        public static Vector3 NotePosition(this MusicSheet ms, BeatLocation bl)
        {
            Vector3 pos = Vector3.zero;
            pos += MeasurePos(bl.MeasureNumber);
            pos += CountPos(ms.RhythmSpecs.Time.Signature.Quantity, bl.BeatAssignment.Count, bl.BeatAssignment.SubBeatAssignment);
            return pos;
        }

        static Vector3 MeasurePos(MeasureNumber m) => m switch
        {
            _ when m == MeasureNumber.One => (Cam.Io.OrthoX() * .515f * Vector2.left) + (Vector2.up * 3),
            _ when m == MeasureNumber.Two => (Cam.Io.OrthoX() * .515f * Vector2.right) + (Vector2.up * 3),
            _ when m == MeasureNumber.Thr => (Cam.Io.OrthoX() * .515f * Vector2.left) + (Vector2.down * 1),
            _ when m == MeasureNumber.For => (Cam.Io.OrthoX() * .515f * Vector2.right) + (Vector2.down * 1),
            _ => Vector2.zero
        };

        static Vector3 CountPos(Count counts, Count c, SubBeatAssignment b)
        {
            float pointA = -Cam.Io.OrthoX() * .45f;
            float pointB = pointA * -1;

            float interval = (pointB - pointA) / ((int)counts * 12);

            float x = (((int)c - 1) * 12) + (int)b;
            return new Vector3(pointA + (x * interval), 0, 0);
        }

        static void AssignNoteSprites(this MusicSheet ms)
        {
            List<Note> ns = ms.Notes;

            GetNoteParents();
            GetNoteHeads();
            GetBeamsOrFlags();
            void GetNoteParents()
            {
                for (int i = 0; i < ns.Count; i++)
                {
                    ns[i].TF = new GameObject(ns[i].BeatLocation.MeasureNumber + " " +
                            ns[i].BeatLocation.BeatAssignment.Count + " " +
                            ns[i].BeatLocation.BeatAssignment.SubBeatAssignment + " " +
                            ns[i].QuantizedRhythmicValue).transform;
                    ns[i].TF.SetParent(ms.Parent);
                    ns[i].TF.position = NotePosition(ms, ns[i].BeatLocation);
                }
            }

            void GetNoteHeads()
            {
                for (int i = 0; i < ns.Count; i++)
                {
                    ns[i].SR = new GameObject(nameof(Sprite)).AddComponent<SpriteRenderer>();
                    ns[i].SR.transform.SetParent(ns[i].TF);
                    ns[i].SR.transform.position = ns[i].TF.position;//ns[i].TF.gameObject.AddComponent<SpriteRenderer>();

                    if (ns[i].Rest)
                    {
                        ns[i].SR.sprite = ns[i].QuantizedRhythmicValue switch
                        {
                            RhythmicValue.Half => Assets.HalfRest,
                            RhythmicValue.TripHalf => Assets.HalfRest,
                            RhythmicValue.DotHalf => Assets.HalfRest,
                            RhythmicValue.Eighth => Assets.EighthRest,
                            RhythmicValue.DotEighth => Assets.EighthRest,
                            RhythmicValue.TripEighth => Assets.EighthRest,
                            RhythmicValue.Whole => Assets.WholeRest,
                            //RhythmicValue.TripWhole => Assets.WholeRest,
                            RhythmicValue.Sixteenth => Assets.SixteenthRest,
                            _ => Assets.QuarterRest,
                        };
                    }
                    else
                    {
                        ns[i].SR.sprite = ns[i].QuantizedRhythmicValue switch
                        {
                            RhythmicValue.Half => Assets.WhiteNote,
                            RhythmicValue.DotHalf => Assets.WhiteNote,
                            RhythmicValue.TripHalf => Assets.WhiteNote,
                            RhythmicValue.Whole => Assets.WhiteNote,
                            //RhythmicValue.TripWhole => Assets.WhiteNote,
                            _ => Assets.BlackNote,
                        };
                    }
                    ns[i].TF.localScale = Vector3.one * 10;

                    if (ns[i].QuantizedRhythmicValue == RhythmicValue.DotEighth ||
                        ns[i].QuantizedRhythmicValue == RhythmicValue.DotQuarter ||
                        ns[i].QuantizedRhythmicValue == RhythmicValue.DotHalf)
                    {
                        Transform dot = new GameObject(nameof(dot)).transform;
                        dot.SetParent(ns[i].TF);
                        var dotsr = dot.gameObject.AddComponent<SpriteRenderer>();
                        dotsr.sprite = Assets.Dot;
                        dot.position = ns[i].TF.position;
                        dot.localScale = Vector3.one;
                    }

                    if ((ns[i].QuantizedRhythmicValue == RhythmicValue.Half ||
                         ns[i].QuantizedRhythmicValue == RhythmicValue.DotHalf ||
                         ns[i].QuantizedRhythmicValue == RhythmicValue.TripHalf) &&
                        !ns[i].Rest)
                    {
                        Transform stem = new GameObject(nameof(stem)).transform;
                        stem.SetParent(ns[i].TF);
                        var stemsr = stem.gameObject.AddComponent<SpriteRenderer>();
                        stemsr.sprite = Assets.Stem;
                        stem.position = ns[i].TF.position;
                        stem.localScale = Vector3.one;
                    }
                }

            }

            void GetBeamsOrFlags()
            {
                for (int i = 0; i < ns.Count; i++)
                {
                    if ((ns[i].QuantizedRhythmicValue != RhythmicValue.DotEighth &&
                         ns[i].QuantizedRhythmicValue != RhythmicValue.Eighth &&
                         ns[i].QuantizedRhythmicValue != RhythmicValue.TripEighth &&
                         ns[i].QuantizedRhythmicValue != RhythmicValue.Sixteenth) ||
                         ns[i].Rest) continue;

                    bool toflag = true;
                    bool ef = false, er = false, sf = false, sr = false;
                    bool sfb = false, srb = false, efb = false, erb = false;

                    if (i < ns.Count - 1 && ns[i + 1].ParentCell == ns[i].ParentCell &&
                       (ns[i + 1].QuantizedRhythmicValue == RhythmicValue.Sixteenth ||
                        ns[i + 1].QuantizedRhythmicValue == RhythmicValue.Eighth ||
                        ns[i + 1].QuantizedRhythmicValue == RhythmicValue.DotEighth ||
                        ns[i + 1].QuantizedRhythmicValue == RhythmicValue.TripEighth))
                    {
                        ef = true;
                        efb = true;
                        toflag = false;
                    }

                    if (i > 0 && !ns[i - 1].Rest && ns[i - 1].ParentCell == ns[i].ParentCell &&
                       (ns[i - 1].QuantizedRhythmicValue == RhythmicValue.Sixteenth ||
                        ns[i - 1].QuantizedRhythmicValue == RhythmicValue.Eighth ||
                        ns[i - 1].QuantizedRhythmicValue == RhythmicValue.DotEighth ||
                        ns[i - 1].QuantizedRhythmicValue == RhythmicValue.TripEighth))
                    {
                        er = true;
                        erb = true;
                        toflag = false;
                    }

                    if (ns[i].QuantizedRhythmicValue == RhythmicValue.Sixteenth && !ns[i].Rest)
                    {
                        switch (ns[i].ParentCell.Shape)
                        {
                            case CellShape.LS:
                                if (!ns[i - 1].Rest)
                                {
                                    toflag = false;
                                    sr = erb = true;
                                }
                                break;

                            case CellShape.SL:
                                toflag = false;
                                sf = efb = true;
                                break;

                            case CellShape.SLS:
                                toflag = false;
                                sf = efb = i < ns.Count - 1 && ns[i + 1].ParentCell == ns[i].ParentCell;
                                sr = erb = i > 0 && ns[i - 1].ParentCell == ns[i].ParentCell && !ns[i - 1].Rest;
                                break;

                            case CellShape.SSL:
                                toflag = false;
                                sf = sfb = i < ns.Count - 1 && ns[i + 1].ParentCell == ns[i].ParentCell && ns[i + 1].QuantizedRhythmicValue == RhythmicValue.Sixteenth;
                                sr = srb = erb = i > 0 && ns[i - 1].ParentCell == ns[i].ParentCell && ns[i - 1].QuantizedRhythmicValue == RhythmicValue.Sixteenth;// && !ns[i - 1].Rest;
                                efb = true;
                                break;

                            case CellShape.LSS:
                                toflag = false;
                                sf = sfb = efb = i < ns.Count - 1 && ns[i + 1].ParentCell == ns[i].ParentCell && ns[i + 1].QuantizedRhythmicValue == RhythmicValue.Sixteenth;
                                sr = srb = i > 0 && !ns[i - 1].Rest && ns[i - 1].ParentCell == ns[i].ParentCell && ns[i - 1].QuantizedRhythmicValue == RhythmicValue.Sixteenth; //&& !ns[i - 1].Rest;
                                erb = !ns[i - 1].Rest;
                                break;

                            case CellShape.SSSS:
                                toflag = false;
                                sf = sfb = efb = i < ns.Count - 1 && ns[i + 1].ParentCell == ns[i].ParentCell && ns[i + 1].QuantizedRhythmicValue == RhythmicValue.Sixteenth;
                                sr = srb = erb = i > 0 && !ns[i - 1].Rest && ns[i - 1].ParentCell == ns[i].ParentCell && ns[i - 1].QuantizedRhythmicValue == RhythmicValue.Sixteenth;
                                break;
                        }


                        //if (i < ns.Count - 1 && ns[i + 1].ParentCell == ns[i].ParentCell &&
                        //   (ns[i + 1].QuantizedRhythmicValue == RhythmicValue.Sixteenth ||
                        //    ns[i + 1].QuantizedRhythmicValue == RhythmicValue.Eighth ||
                        //    ns[i + 1].QuantizedRhythmicValue == RhythmicValue.DotEighth))
                        //{
                        //    sf = true;
                        //    efb = true;
                        //    sfb = ns[i + 1].QuantizedRhythmicValue == RhythmicValue.Sixteenth;
                        //    toflag = false;
                        //}

                        //if (i > 0 && !ns[i - 1].Rest && ns[i - 1].ParentCell == ns[i].ParentCell && (
                        //    ns[i - 1].QuantizedRhythmicValue == RhythmicValue.Sixteenth ||
                        //    ns[i - 1].QuantizedRhythmicValue == RhythmicValue.Eighth ||
                        //    ns[i - 1].QuantizedRhythmicValue == RhythmicValue.DotEighth))
                        //{
                        //    sr = true;
                        //    erb = true;
                        //    srb = ns[i - 1].QuantizedRhythmicValue == RhythmicValue.Sixteenth;
                        //    toflag = false;
                        //}
                    }

                    if (toflag)
                    {
                        Transform flag = new GameObject(nameof(flag)).transform;
                        flag.SetParent(ns[i].TF);
                        var flagsr = flag.gameObject.AddComponent<SpriteRenderer>();
                        flagsr.sprite = Assets.EighthFlag;
                        flag.position = ns[i].TF.position;
                        flag.localScale = Vector3.one;
                        continue;
                    }
                    else if (sr && sf)
                    {
                        ns[i].SR.sprite = Assets.SixteenthBoth;
                    }
                    else if (sr && !sf && !ef)
                    {
                        ns[i].SR.sprite = Assets.SixteenthReverse;
                    }
                    else if (sr && !sf && ef)
                    {
                        ns[i].SR.sprite = Assets.SixteenthReverseEighthForward;
                    }
                    else if (sf && !sr && !er)
                    {
                        ns[i].SR.sprite = Assets.SixteenthForward;
                    }
                    else if (sf && !sr && er)
                    {
                        ns[i].SR.sprite = Assets.SixteenthForwardEighthReverse;
                    }
                    else if (ef && er)
                    {
                        ns[i].SR.sprite = Assets.EighthBoth;
                    }
                    else if (ef && !er)
                    {
                        ns[i].SR.sprite = Assets.EighthForward;
                    }
                    else if (er && !ef)
                    {
                        ns[i].SR.sprite = Assets.EighthReverse;
                    }

                    if (srb) { AddReverse16thBeamBridge(); }
                    if (sfb) { AddForward16thBeamBridge(); }
                    if (erb) { AddReverse8thBeamBridge(); }
                    if (efb) { AddForward8thBeamBridge(); }

                    void AddForward16thBeamBridge()
                    {
                        Transform beamF = new GameObject(nameof(Assets.SixteenthBeam)).transform;
                        beamF.SetParent(ns[i].TF);
                        var beamFSR = beamF.gameObject.AddComponent<SpriteRenderer>();
                        beamFSR.sprite = Assets.SixteenthBeam;
                        beamF.localScale = new Vector3(.5f + (ScaledToFit(ns[i].TF.position.x, ns[i + 1].TF.position.x) * .45f), 1, 1); //new Vector3(1.25f, 1, 1);
                        beamF.position = ns[i].TF.position + (.065f * beamF.localScale.x * Vector3.right);
                    }
                    void AddReverse16thBeamBridge()
                    {
                        Transform beam = new GameObject(nameof(Assets.SixteenthBeamReverse)).transform;
                        beam.SetParent(ns[i].TF);
                        var beamsr = beam.gameObject.AddComponent<SpriteRenderer>();
                        beam.localScale = new Vector3(.5f + (ScaledToFit(ns[i].TF.position.x, ns[i - 1].TF.position.x) * .45f), 1, 1); //new Vector3(1.25f, 1, 1);
                        beamsr.sprite = Assets.SixteenthBeamReverse;
                        beam.position = ns[i].TF.position - (.065f * beam.localScale.x * Vector3.right);
                    }
                    void AddForward8thBeamBridge()
                    {
                        Transform beamF = new GameObject(nameof(Assets.EighthBeam)).transform;
                        beamF.SetParent(ns[i].TF);
                        var beamFSR = beamF.gameObject.AddComponent<SpriteRenderer>();
                        beamFSR.sprite = Assets.EighthBeam;
                        beamF.localScale = new Vector3(.75f + (ScaledToFit(ns[i].TF.position.x, ns[i + 1].TF.position.x) * .5f), 1, 1); //new Vector3(1.25f, 1, 1);
                        beamF.position = ns[i].TF.position + (.065f * beamF.localScale.x * Vector3.right);
                    }
                    void AddReverse8thBeamBridge()
                    {
                        Transform beam = new GameObject(nameof(Assets.EighthBeamReverse)).transform;
                        beam.SetParent(ns[i].TF);
                        var beamsr = beam.gameObject.AddComponent<SpriteRenderer>();
                        beam.localScale = new Vector3(.75f + (ScaledToFit(ns[i].TF.position.x, ns[i - 1].TF.position.x) * .5f), 1, 1); //new Vector3(1.25f, 1, 1);
                        beamsr.sprite = Assets.EighthBeamReverse;
                        beam.position = ns[i].TF.position - (.065f * beam.localScale.x * Vector3.right);
                    }
                    //if (ef)
                    //{
                    //    ns[i].SR.sprite = Assets.EighthNoteForward;

                    //}
                    //if (er)
                    //{
                    //    ns[i].SR.sprite = Assets.EighthNoteReversed;
                    //    Transform beam = new GameObject(nameof(Assets.EighthBeamReverse)).transform;
                    //    beam.SetParent(ns[i].TF);
                    //    var beamsr = beam.gameObject.AddComponent<SpriteRenderer>();
                    //    beam.localScale = new Vector3(ScaledToFit(ns[i].TF.position.x, ns[i - 1].TF.position.x) * 1.1f, 1, 1); //new Vector3(1.25f, 1, 1);
                    //    beamsr.sprite = Assets.EighthBeamReverse;
                    //    beam.position = ns[i].TF.position;
                    //    toflag = false;
                    //}
                    //if (sf)
                    //{
                    //    Transform beam = new GameObject(nameof(Assets.SixteenthBeam)).transform;
                    //    beam.SetParent(ns[i].TF);
                    //    var beamsr = beam.gameObject.AddComponent<SpriteRenderer>();
                    //    beamsr.sprite = Assets.SixteenthBeam;
                    //    beam.position = ns[i].TF.position - (Vector3.right * .035f);
                    //    beam.localScale = new Vector3(ScaledToFit(ns[i].TF.position.x, ns[i + 1].TF.position.x) * 1.15f, 1, 1); //new Vector3(.65f, 1, 1);
                    //}
                    //if (sr)
                    //{
                    //    Transform beam = new GameObject(nameof(Assets.SixteenthBeamReverse)).transform;
                    //    beam.SetParent(ns[i].TF);
                    //    var beamsr = beam.gameObject.AddComponent<SpriteRenderer>();
                    //    beamsr.sprite = Assets.SixteenthBeamReverse;
                    //    beam.position = ns[i].TF.position - (Vector3.right * .035f);
                    //    beam.localScale = new Vector3(ScaledToFit(ns[i].TF.position.x, ns[i - 1].TF.position.x) * 1.15f, 1, 1); //new Vector3(.65f, 1, 1);
                    //}
                }
            }


        }

        static void AssignTies(this MusicSheet ms)
        {
            List<Note> ns = ms.Notes;
            for (int i = 0; i < ns.Count; i++)
            {
                if (ns[i].ParentCell == ms.Measures[2].Cells[0] &&
                    ms.Measures[2].Cells[0].TiedFrom &&
                    ns[i].BeatLocation.BeatAssignment.SubBeatAssignment == SubBeatAssignment.D &&
                    ns[i].BeatLocation.BeatAssignment.Count == Count.One)
                {
                    Transform tf = new GameObject("Tie").transform;
                    tf.SetParent(ns[i].TF);
                    tf.localScale = new Vector3(ScaledToFit(ns[i].TF.position.x, ns[i].TF.position.x + 1.5f) * -.1f, .5f, 1);
                    tf.position = ns[i].TF.position;// + new Vector3(tf.localScale.x * .5f, 0, 0);
                    SpriteRenderer sr = tf.gameObject.AddComponent<SpriteRenderer>();
                    sr.sprite = Assets.Tie;
                }
                if (!ns[i].TiesTo) continue;
                if (ns[i].ParentCell == ms.Measures[1].Cells[^1])
                {
                    Transform tf = new GameObject("Tie").transform;
                    tf.SetParent(ns[i].TF);
                    tf.localScale = new Vector3(ScaledToFit(ns[i].TF.position.x, ns[i].TF.position.x + 2.5f) * .1f, .5f, 1);
                    tf.position = ns[i].TF.position;// + new Vector3(tf.localScale.x * .5f, 0, 0);
                    SpriteRenderer sr = tf.gameObject.AddComponent<SpriteRenderer>();
                    sr.sprite = Assets.Tie;
                }
                else
                {
                    Transform tf = new GameObject("Tie").transform;
                    tf.SetParent(ns[i].TF);
                    tf.localScale = new Vector3(ScaledToFit(ns[i].TF.position.x, ns[i + 1].TF.position.x) * .1f, .5f, 1);
                    tf.position = ns[i].TF.position;// + new Vector3(tf.localScale.x * .5f, 0, 0);
                    SpriteRenderer sr = tf.gameObject.AddComponent<SpriteRenderer>();
                    sr.sprite = Assets.Tie;
                }

            }
        }

        public static SubBeatAssignment[] SubCountsPerMeasure(this MusicSheet ms)
        {
            List<SubBeatAssignment> beats = new();

            if (!ms.RhythmSpecs.HasTriplets)
            {
                switch (ms.RhythmSpecs.SubDivisionTier)
                {
                    case SubDivisionTier.BeatOnly:
                        for (int i = 0; i < (int)ms.RhythmSpecs.Time.Signature.Quantity; i++)
                        {
                            beats.Add(SubBeatAssignment.D);
                        };
                        break;

                    case SubDivisionTier.BeatAndD1:
                        for (int i = 0; i < (int)ms.RhythmSpecs.Time.Signature.Quantity; i++)
                        {
                            beats.Add(SubBeatAssignment.D);
                            beats.Add(SubBeatAssignment.N);
                        };
                        break;

                    case SubDivisionTier.D1Only:
                        for (int i = 0; i < (int)ms.RhythmSpecs.Time.Signature.Quantity; i++)
                        {
                            beats.Add(SubBeatAssignment.D);
                            beats.Add(SubBeatAssignment.N);
                        };
                        break;
                    case SubDivisionTier.D1AndD2:
                        for (int i = 0; i < (int)ms.RhythmSpecs.Time.Signature.Quantity; i++)
                        {
                            beats.Add(SubBeatAssignment.D);
                            beats.Add(SubBeatAssignment.E);
                            beats.Add(SubBeatAssignment.N);
                            beats.Add(SubBeatAssignment.A);
                        };
                        break;
                    case SubDivisionTier.D2Only:
                        for (int i = 0; i < (int)ms.RhythmSpecs.Time.Signature.Quantity; i++)
                        {
                            beats.Add(SubBeatAssignment.D);
                            beats.Add(SubBeatAssignment.E);
                            beats.Add(SubBeatAssignment.N);
                            beats.Add(SubBeatAssignment.A);
                        };
                        break;
                }
            }

            else
            {
                switch (ms.RhythmSpecs.SubDivisionTier)
                {
                    case SubDivisionTier.BeatOnly:
                        for (int i = 0; i < (int)ms.RhythmSpecs.Time.Signature.Quantity; i++)
                        {
                            beats.Add(SubBeatAssignment.D);
                        };
                        break;

                    case SubDivisionTier.BeatAndD1:
                        for (int i = 0; i < (int)ms.RhythmSpecs.Time.Signature.Quantity; i++)
                        {
                            beats.Add(SubBeatAssignment.D);
                            beats.Add(SubBeatAssignment.T);
                            beats.Add(SubBeatAssignment.N);
                            beats.Add(SubBeatAssignment.L);
                        };
                        break;

                    case SubDivisionTier.D1Only:
                        for (int i = 0; i < (int)ms.RhythmSpecs.Time.Signature.Quantity; i++)
                        {
                            beats.Add(SubBeatAssignment.D);
                            beats.Add(SubBeatAssignment.T);
                            beats.Add(SubBeatAssignment.N);
                            beats.Add(SubBeatAssignment.L);
                        };
                        break;
                }
            }

            return beats.ToArray();
        }

        static float ScaledToFit(float pointA, float pointB)
        {
            Debug.Log(pointA + ", " + pointB + ": " + Mathf.Abs(pointB - pointA));
            return Mathf.Abs(pointB - pointA);
        }
    }
}




//foreach (Note n in ms.Notes)
//{
//    Transform t = new GameObject(n.BeatLocation.MeasureNumber + " " +
//                                 n.BeatLocation.Count + " " +
//                                 n.BeatLocation.SubBeatAssignment + " " +
//                                 n.QuantizedRhythmicValue).transform;
//    t.SetParent(ms.Parent);
//    t.position = NotePosition(ms, n.BeatLocation);
//    var sr = t.gameObject.AddComponent<SpriteRenderer>();

//    if (n.Rest)
//    {
//        sr.sprite = n.QuantizedRhythmicValue switch
//        {
//            RhythmicValue.Half => Assets.HalfRest,
//            RhythmicValue.DotHalf => Assets.HalfRest,
//            RhythmicValue.Eighth => Assets.EighthRest,
//            _ => Assets.QuarterRest,
//        };
//    }
//    else
//    {
//        sr.sprite = n.QuantizedRhythmicValue switch
//        {
//            RhythmicValue.Half => Assets.WhiteNote,
//            RhythmicValue.DotHalf => Assets.WhiteNote,
//            RhythmicValue.TripHalf => Assets.White,
//            _ => Assets.BlackNote,
//        };
//    }
//    t.transform.localScale = Vector3.one * 10;

//    if (n.QuantizedRhythmicValue == RhythmicValue.DotEighth ||
//        n.QuantizedRhythmicValue == RhythmicValue.DotQuarter ||
//        n.QuantizedRhythmicValue == RhythmicValue.DotHalf)
//    {
//        Transform dot = new GameObject(nameof(dot)).transform;
//        dot.SetParent(t);
//        var dotsr = dot.gameObject.AddComponent<SpriteRenderer>();
//        dotsr.sprite = Assets.Dot;
//        dot.position = t.position;
//        dot.localScale = Vector3.one;
//    }

//    if (n.QuantizedRhythmicValue == RhythmicValue.Eighth && !n.Rest)
//    {

//        Transform flag = new GameObject(nameof(flag)).transform;
//        flag.SetParent(t);
//        var flagsr = flag.gameObject.AddComponent<SpriteRenderer>();
//        flagsr.sprite = Assets.EighthFlag;
//        flag.position = t.position;
//        flag.localScale = Vector3.one;
//    }
//}


//static Vector3 SubBeatPos(SubBeatAssignment b, Count count)
//{
//    int c = (int)count * 12;
//    float x = -48 / Cam.Io.OrthoX() * .45f;
//    return new Vector3((x * c) + (x * (int)b), 0, 0);
//}

//public static void AssignNoteSprites(this MusicSheet ms)
//{
//    foreach (Note n in ms.Notes)
//    {
//        Transform parent = new GameObject(n.BeatLocation.ToString()).transform;
//        parent.SetParent(ms.Parent);
//        parent.position = NotePosition(n.BeatLocation);

//        switch (n.QuantizedRhythmicValue)
//        {
//            case RhythmicValue.Whole:
//                {
//                    Card note = AddNote(n, parent);
//                    note.transform.position = n.Rest ? parent.position + new Vector3(.4f, .035f) : parent.position;
//                    note.sprite = n.Rest ? Assets.WholeRest : Assets.WhiteNote;

//                    if (n.Tied)
//                    {
//                        AddTie(n, note, parent);
//                    }
//                    break;
//                }

//            case RhythmicValue.DotHalf:
//                {
//                    Card note = AddNote(n, parent);
//                    AddDot(note, parent);

//                    if (n.Rest)
//                    {
//                        note.sprite = Assets.HalfRest;
//                        break;
//                    }
//                    else
//                    {
//                        note.sprite = Assets.WhiteNote;
//                        AddStem(note);
//                    }

//                    if (n.Tied)
//                    {
//                        AddTie(n, note, parent);
//                    }
//                    break;
//                }

//            case RhythmicValue.Half:
//                {
//                    Card note = AddNote(n, parent);

//                    if (n.Rest)
//                    {
//                        note.sprite = Assets.HalfRest;
//                        break;
//                    }
//                    else
//                    {
//                        note.sprite = Assets.WhiteNote;
//                        AddStem(note);
//                    }

//                    if (n.Tied)
//                    {
//                        AddTie(n, note, parent);
//                    }
//                    break;
//                }

//            case RhythmicValue.TripHalf:
//                {
//                    Card note = AddNote(n, parent);

//                    if (n.BeatLocation == BeatLocation.One || n.BeatLocation == BeatLocation.Thr)
//                    {
//                        AddTripBracket(note, new Vector3(.125f, .15f), new Vector2(.3f, 1));
//                        AddTrip3(note, new Vector3(.125f, .15f), Vector2.one * .5f);
//                    }

//                    if (n.Rest)
//                    {
//                        note.sprite = Assets.HalfRest;
//                        break;
//                    }
//                    else
//                    {
//                        note.sprite = Assets.WhiteNote;
//                        AddStem(note);
//                    }

//                    if (n.Tied)
//                    {
//                        AddTie(n, note, parent);
//                    }


//                    break;
//                }

//            case RhythmicValue.DotQuarter:
//                {
//                    Card note = AddNote(n, parent);
//                    note.sprite = n.Rest ? Assets.QuarterRest : Assets.BlackNote;
//                    AddDot(note, parent);

//                    if (n.Tied)
//                    {
//                        AddTie(n, note, parent);
//                    }
//                    break;
//                }

//            case RhythmicValue.Quarter:
//                {
//                    Card note = AddNote(n, parent);
//                    note.sprite = n.Rest ? Assets.QuarterRest : Assets.BlackNote;

//                    if (n.Tied)
//                    {
//                        AddTie(n, note, parent);
//                    }
//                    break;
//                }

//            case RhythmicValue.TripQuarter:
//                {
//                    Card note = AddNote(n, parent);
//                    note.sprite = n.Rest ? Assets.QuarterRest : Assets.BlackNote;

//                    if (n.ParentCell.Quantizement == Quantizement.EighthTrips &&
//                       (n.BeatLocation == BeatLocation.One || n.BeatLocation == BeatLocation.Thr ||
//                        n.BeatLocation == BeatLocation.Two || n.BeatLocation == BeatLocation.For))
//                    {
//                        AddTripBracket(note, new Vector3(.05f, .15f), new Vector2(.3f, 1));
//                        AddTrip3(note, new Vector3(.05f, .15f), Vector2.one * .5f);
//                        break;
//                    }

//                    if (n.BeatLocation == BeatLocation.One || n.BeatLocation == BeatLocation.Thr)
//                    {
//                        AddTripBracket(note, new Vector3(.125f, .15f), new Vector2(.3f, 1));
//                        AddTrip3(note, new Vector3(.125f, .15f), Vector2.one * .5f);
//                    }

//                    if (n.Tied)
//                    {
//                        AddTie(n, note, parent);
//                    }
//                    break;
//                }

//            case RhythmicValue.DotEighth:
//                {
//                    Card note = AddNote(n, parent);
//                    AddDot(note, parent);

//                    if (n.Rest)
//                    {
//                        note.sprite = Assets.EighthRest;
//                        break;
//                    }

//                    note.sprite = Assets.BlackNote;

//                    if ((n.ParentCell.RhythmicShape == CellShape.dhq ||
//                        n.ParentCell.RhythmicShape == CellShape.qdh) &&
//                        n.ParentCell.Rest)
//                    {
//                        AddEighthFlag(note);
//                    }
//                    else if (n.ParentCell.RhythmicShape == CellShape.dhq)
//                    {
//                        AddEighthBeam(note, Vector3.right * .015f, new Vector2(2, 1));
//                    }
//                    else if (n.ParentCell.RhythmicShape == CellShape.qdh)
//                    {
//                        // AddEighthBeamRevemse(note, size: Vector3.one);
//                    }

//                    if (n.Tied)
//                    {
//                        AddTie(n, note, parent);
//                    }
//                    break;
//                }

//            case RhythmicValue.Eighth:
//                {
//                    Card note = AddNote(n, parent);
//                    note.sprite = n.Rest ? Assets.EighthRest : Assets.BlackNote;

//                    switch (n.ParentCell.Quantizement)
//                    {
//                        case Quantizement.Eighth:
//                            {
//                                switch (n.ParentCell.RhythmicShape)
//                                {
//                                    case CellShape.dhq:
//                                        {
//                                            AddEighthFlag(note);
//                                        }
//                                        break;

//                                    case CellShape.hqq:
//                                        {
//                                            if (n.BeatLocation == BeatLocation.Two || n.BeatLocation == BeatLocation.For)
//                                            {
//                                                AddEighthBeam(note, Vector3.zero, Vector3.one);
//                                            }
//                                            else
//                                            {
//                                                AddEighthBeamRevemse(note, size: Vector3.one);
//                                            }
//                                        }
//                                        break;

//                                    case CellShape.qdh:
//                                        {
//                                            if (n.Rest) { break; }
//                                            AddEighthFlag(note);
//                                        }
//                                        break;

//                                    case CellShape.qhq:
//                                        {
//                                            if (n.Rest && (n.BeatLocation == BeatLocation.One
//                                                || n.BeatLocation == BeatLocation.Thr)) { break; }
//                                            AddEighthFlag(note);
//                                        }
//                                        break;

//                                    case CellShape.qqh:
//                                        {
//                                            if (n.BeatLocation == BeatLocation.One || n.BeatLocation == BeatLocation.Thr)
//                                            {
//                                                if (n.Rest) { break; }
//                                                AddEighthBeam(note, Vector3.zero, Vector3.one);
//                                            }
//                                            else if (n.ParentCell.Rest)
//                                            {
//                                                AddEighthFlag(note);
//                                            }
//                                            else
//                                            {
//                                                AddEighthBeamRevemse(note, size: Vector3.one);
//                                            }

//                                        }
//                                        break;

//                                    case CellShape.qqqq:
//                                        {
//                                            if (n.BeatLocation == BeatLocation.One || n.BeatLocation == BeatLocation.Thr)
//                                            {
//                                                if (n.Rest) { break; }
//                                                AddEighthBeam(note, Vector3.zero, Vector3.one);
//                                            }
//                                            else if (n.ParentCell.Rest && (n.BeatLocation == BeatLocation.OneN || n.BeatLocation == BeatLocation.ThrN))
//                                            {
//                                                if (n.Rest) { break; }
//                                                AddEighthBeam(note, Vector3.zero, Vector3.one);
//                                            }
//                                            else if (n.BeatLocation == BeatLocation.TwoN || n.BeatLocation == BeatLocation.ForN)
//                                            {
//                                                AddEighthBeamRevemse(note, size: Vector3.one);
//                                            }
//                                            else
//                                            {
//                                                if (n.Rest) { break; }
//                                                AddEighthBeam(note, Vector3.zero, new Vector2(1.5f, 1));
//                                                AddEighthBeamRevemse(note, size: new Vector2(1.5f, 1));
//                                            }
//                                        }
//                                        break;
//                                }
//                                break;
//                            }

//                        case Quantizement.Sixteenth:
//                            {
//                                if (n.ParentCell.RhythmicShape == CellShape.hh && n.ParentCell.Rest && !n.Rest)
//                                {
//                                    AddEighthFlag(note);
//                                }
//                                else if ((n.BeatLocation == BeatLocation.One || n.BeatLocation == BeatLocation.Two ||
//                                          n.BeatLocation == BeatLocation.Thr || n.BeatLocation == BeatLocation.For) && !n.Rest)
//                                {
//                                    AddEighthBeam(note, Vector2.zero, new Vector2(.9f, 1));
//                                }
//                                else if (n.BeatLocation == BeatLocation.OneE || n.BeatLocation == BeatLocation.TwoE ||
//                                         n.BeatLocation == BeatLocation.ThrE || n.BeatLocation == BeatLocation.ForE)
//                                {
//                                    AddEighthBeam(note, Vector3.zero, new Vector2(.9f, 1));

//                                    if (!n.ParentCell.Rest)
//                                    {
//                                        AddEighthBeamRevemse(note, size: new Vector2(.8f, 1));
//                                    }
//                                }
//                                else if (n.BeatLocation == BeatLocation.OneN || n.BeatLocation == BeatLocation.TwoN ||
//                                         n.BeatLocation == BeatLocation.ThrN || n.BeatLocation == BeatLocation.ForN)
//                                {
//                                    AddEighthBeamRevemse(note, size: new Vector2(.8f, 1));
//                                }
//                                break;
//                            }
//                    };

//                    if (n.Tied)
//                    {
//                        AddTie(n, note, parent);
//                    }
//                    break;
//                }

//            case RhythmicValue.TripEighth:
//                {
//                    Card note = AddNote(n, parent);
//                    note.sprite = n.Rest ? Assets.EighthRest : Assets.BlackNote;

//                    if (n.Tied)
//                    {
//                        AddTie(n, note, parent);
//                    }

//                    if (n.BeatLocation == BeatLocation.One || n.BeatLocation == BeatLocation.Two ||
//                        n.BeatLocation == BeatLocation.Thr || n.BeatLocation == BeatLocation.For)
//                    {
//                        AddTripBracket(note, new Vector3(.05f, .15f), new Vector2(.3f, 1));
//                        AddTrip3(note, new Vector3(.05f, .15f), Vector2.one * .5f);
//                    }

//                    if ((n.ParentCell.RhythmicShape == CellShape.tqh || n.ParentCell.RhythmicShape == CellShape.thq) && !n.Rest)
//                    {
//                        AddEighthFlag(note);
//                    }
//                    else if ((n.BeatLocation == BeatLocation.One || n.BeatLocation == BeatLocation.Two ||
//                              n.BeatLocation == BeatLocation.Thr || n.BeatLocation == BeatLocation.For) & !n.Rest)
//                    {
//                        AddEighthBeam(note, new Vector3(.004f, 0), new Vector3(1.2f, 1));
//                    }
//                    else if ((n.BeatLocation == BeatLocation.OneT || n.BeatLocation == BeatLocation.TwoT ||
//                              n.BeatLocation == BeatLocation.ThrT || n.BeatLocation == BeatLocation.ForT))
//                    {
//                        AddEighthBeam(note, Vector3.zero, Vector3.one);
//                    }
//                    else if ((n.BeatLocation == BeatLocation.OneL || n.BeatLocation == BeatLocation.TwoL ||
//                              n.BeatLocation == BeatLocation.ThrL || n.BeatLocation == BeatLocation.ForL))
//                    {
//                        AddEighthBeamRevemse(note, size: new Vector2(.9f, 1));
//                    }
//                    break;
//                }

//            case RhythmicValue.Sixteenth:
//                {
//                    Card note = AddNote(n, parent);
//                    note.sprite = n.Rest ? Assets.SixteenthRest : Assets.BlackNote;

//                    switch (n.ParentCell.RhythmicShape)
//                    {
//                        case CellShape.dhq:
//                            {
//                                if (n.ParentCell.Rest)
//                                {
//                                    AddSixteenthFlag(note);
//                                }
//                                else
//                                {
//                                    AddEighthBeamRevemse(note, new Vector2(.8f, 1));

//                                    AddSixteenthBeamRevemse(note, Vector3.right * -.005f, new Vector2(.5f, 1));
//                                }
//                            }
//                            break;

//                        case CellShape.qdh:
//                            {
//                                if (!n.Rest)
//                                {
//                                    AddSixteenthBeam(note, -Vector3.right * .005f, new Vector2(.5f, 1));
//                                    AddEighthBeam(note, Vector3.zero, new Vector2(.9f, 1));
//                                }
//                            }
//                            break;

//                        case CellShape.hqq:
//                            {
//                                if (n.BeatLocation == BeatLocation.OneN || n.BeatLocation == BeatLocation.TwoN ||
//                                    n.BeatLocation == BeatLocation.ThrN || n.BeatLocation == BeatLocation.ForN)
//                                {
//                                    if (!n.ParentCell.Rest)
//                                    {
//                                        AddEighthBeamRevemse(note, new Vector2(.85f, 1));
//                                    }
//                                    AddSixteenthBeam(note, -Vector3.right * .005f, new Vector2(.5f, 1));
//                                    AddEighthBeam(note, Vector3.zero, new Vector2(.9f, 1));
//                                }
//                                else
//                                {
//                                    AddEighthBeamRevemse(note, new Vector2(.85f, 1));
//                                    AddSixteenthBeamRevemse(note, -Vector3.right * .0055f, new Vector2(.8f, 1));
//                                }
//                            }
//                            break;

//                        case CellShape.qqh:
//                            {
//                                if ((n.BeatLocation == BeatLocation.One || n.BeatLocation == BeatLocation.Two ||
//                                 n.BeatLocation == BeatLocation.Thr || n.BeatLocation == BeatLocation.For) && !n.Rest)
//                                {
//                                    AddSixteenthBeam(note, -Vector3.right * .005f, new Vector2(.5f, 1));
//                                    AddEighthBeam(note, Vector3.zero, new Vector2(.9f, 1));
//                                }
//                                else if (!n.Rest && !n.ParentCell.Rest)
//                                {
//                                    AddEighthBeamRevemse(note, new Vector2(.85f, 1));
//                                    AddSixteenthBeamRevemse(note, -Vector3.right * .0055f, new Vector2(.8f, 1));
//                                }
//                                if ((n.BeatLocation == BeatLocation.OneE || n.BeatLocation == BeatLocation.TwoE ||
//                                 n.BeatLocation == BeatLocation.ThrE || n.BeatLocation == BeatLocation.ForE) && n.ParentCell.Rest)
//                                {
//                                    AddSixteenthBeam(note, -Vector3.right * .005f, new Vector2(.5f, 1));
//                                }
//                            }
//                            break;

//                        case CellShape.qhq:
//                            {
//                                if ((n.BeatLocation == BeatLocation.One || n.BeatLocation == BeatLocation.Two ||
//                                    n.BeatLocation == BeatLocation.Thr || n.BeatLocation == BeatLocation.For) && !n.Rest)
//                                {
//                                    AddSixteenthBeam(note, -Vector3.right * .005f, new Vector2(.5f, 1));
//                                    AddEighthBeam(note, Vector3.zero, new Vector2(.9f, 1));
//                                }
//                                else if (!n.Rest)
//                                {
//                                    AddEighthBeamRevemse(note, new Vector2(.85f, 1));
//                                    AddSixteenthBeamRevemse(note, -Vector3.right * .0055f, new Vector2(.5f, 1));
//                                }
//                            }
//                            break;

//                        case CellShape.qqqq:
//                            {
//                                if (!n.Rest &&
//                                   (n.BeatLocation != BeatLocation.OneA && n.BeatLocation != BeatLocation.TwoA &&
//                                    n.BeatLocation != BeatLocation.ThrA && n.BeatLocation != BeatLocation.ForA))
//                                {
//                                    AddSixteenthBeam(note, -Vector3.right * .005f, new Vector2(.5f, 1));
//                                    AddEighthBeam(note, Vector3.zero, new Vector2(.9f, 1));
//                                }

//                                if (n.BeatLocation == BeatLocation.OneA || n.BeatLocation == BeatLocation.TwoA ||
//                                    n.BeatLocation == BeatLocation.ThrA || n.BeatLocation == BeatLocation.ForA ||
//                                    n.BeatLocation == BeatLocation.OneN || n.BeatLocation == BeatLocation.TwoN ||
//                                    n.BeatLocation == BeatLocation.ThrN || n.BeatLocation == BeatLocation.ForN)
//                                {
//                                    AddEighthBeamRevemse(note, new Vector2(.85f, 1));
//                                    AddSixteenthBeamRevemse(note, -Vector3.right * .0055f, new Vector2(.5f, 1));
//                                }

//                                if (!n.ParentCell.Rest &&
//                                   (n.BeatLocation == BeatLocation.OneE || n.BeatLocation == BeatLocation.TwoE ||
//                                    n.BeatLocation == BeatLocation.ThrE || n.BeatLocation == BeatLocation.ForE))
//                                {
//                                    AddEighthBeamRevemse(note, new Vector2(.85f, 1));
//                                    AddSixteenthBeamRevemse(note, -Vector3.right * .0055f, new Vector2(.5f, 1));
//                                }

//                                // if (n.BeatLocation != BeatLocation.One && n.BeatLocation != BeatLocation.Two &&
//                                //   n.BeatLocation != BeatLocation.Thr && n.BeatLocation != BeatLocation.For &&

//                                //   !((n.BeatLocation == BeatLocation.OneE || n.BeatLocation == BeatLocation.TwoE ||
//                                //   n.BeatLocation == BeatLocation.ThrE || n.BeatLocation == BeatLocation.ForE)

//                                //    &&
//                                //   n.ParentCell.Rest))
//                                // {
//                                //     Debug.Log(!((n.BeatLocation == BeatLocation.OneE || n.BeatLocation == BeatLocation.TwoE ||
//                                //                  n.BeatLocation == BeatLocation.ThrE || n.BeatLocation == BeatLocation.ForE)));

//                                //     // AddEighthBeamRevemse(note, new Vector2(.5f, 1));
//                                //     AddSixteenthBeamRevemse(note, -Vector3.right * .0055f, new Vector2(.5f, 1));
//                                // }
//                            }
//                            break;
//                    }

//                    if (n.Tied)
//                    {
//                        AddTie(n, note, parent);
//                    }
//                    break;
//                }
//        }

//        if (n.MeasureNumber == MeasureNumber.Three &&
//            n.BeatLocation == BeatLocation.One &&
//            rb.Measures[1][^1].Tied)
//        {
//            Card tie = new GameObject(nameof(tie)).AddComponent<Card>();
//            tie.transform.SetParent(parent);
//            tie.transform.position = parent.position + (Vector3.left * .14f);
//            tie.transform.localScale = new Vector2(2f, 1);
//            tie.sprite = Assets.Tie;
//        }

//        parent.localScale = Vector2.one * 7;
//    }
//}

//private static Card AddTripBracket(Card note, Vector3 pos, Vector2 size)
//{
//    Card tripBracket = new GameObject(nameof(tripBracket)).AddComponent<Card>();
//    tripBracket.transform.SetParent(note.transform);
//    tripBracket.transform.position = note.transform.position + pos;
//    tripBracket.sprite = Assets.TripletBracket;
//    tripBracket.transform.localScale = size;
//    return tripBracket;
//}

//private static Card AddTrip3(Card note, Vector3 pos, Vector2 size)
//{
//    Card trip3 = new GameObject(nameof(trip3)).AddComponent<Card>();
//    trip3.transform.SetParent(note.transform);
//    trip3.transform.position = note.transform.position + pos;
//    trip3.sprite = Assets.Triplet3;
//    trip3.transform.localScale = size;
//    return trip3;
//}

//private static Card AddSixteenthBeam(Card note, Vector3 offset, Vector2 size)
//{
//    Card sixteenthBeam = new GameObject(nameof(sixteenthBeam)).AddComponent<Card>();
//    sixteenthBeam.transform.SetParent(note.transform);
//    sixteenthBeam.transform.position = note.transform.position + offset;
//    sixteenthBeam.transform.localScale = size;
//    sixteenthBeam.sprite = Assets.SixteenthBeam;
//    return sixteenthBeam;
//}

//private static Card AddSixteenthBeamRevemse(Card note, Vector3 offset, Vector2 size)
//{
//    Card doubleBeamRevemse = new GameObject(nameof(doubleBeamRevemse)).AddComponent<Card>();
//    doubleBeamRevemse.transform.SetParent(note.transform);
//    doubleBeamRevemse.transform.position = note.transform.position + offset;
//    doubleBeamRevemse.transform.localScale = size;
//    doubleBeamRevemse.sprite = Assets.SixteenthBeamRevemse;
//    return doubleBeamRevemse;
//}

//private static Card AddSixteenthFlag(Card note)
//{
//    Card sixteenthFlag = new GameObject(nameof(sixteenthFlag)).AddComponent<Card>();
//    sixteenthFlag.transform.SetParent(note.transform);
//    sixteenthFlag.transform.position = note.transform.position; //+offset
//    sixteenthFlag.sprite = Assets.SixteenthFlag;
//    return sixteenthFlag;
//}

//private static Card AddEighthBeamRevemse(Card note, Vector2 size)
//{
//    Card eighthBeamRevemse = new GameObject(nameof(eighthBeamRevemse)).AddComponent<Card>();
//    eighthBeamRevemse.transform.SetParent(note.transform);
//    eighthBeamRevemse.transform.position = note.transform.position + (Vector3.left * .005f);
//    eighthBeamRevemse.sprite = Assets.EighthBeamRevemse;
//    eighthBeamRevemse.transform.localScale = size;
//    return eighthBeamRevemse;
//}

//private static Card AddEighthBeam(Card note, Vector3 offset, Vector2 size)
//{
//    Card eighthBeam = new GameObject(nameof(eighthBeam)).AddComponent<Card>();
//    eighthBeam.transform.SetParent(note.transform);
//    eighthBeam.transform.position = note.transform.position + offset;
//    eighthBeam.transform.localScale = size;
//    eighthBeam.sprite = Assets.EighthBeam;
//    return eighthBeam;
//}

//private static Card AddEighthFlag(Card note)
//{
//    Card eighthFlag = new GameObject(nameof(eighthFlag)).AddComponent<Card>();
//    eighthFlag.transform.SetParent(note.transform);
//    eighthFlag.transform.position = note.transform.position;
//    eighthFlag.sprite = Assets.EighthFlag;
//    return eighthFlag;
//}

//private static Card AddStem(Card note)
//{
//    Card stem = new GameObject(nameof(stem)).AddComponent<Card>();
//    stem.transform.SetParent(note.transform);
//    stem.transform.position = note.transform.position;
//    stem.sprite = Assets.Stem;
//    return stem;
//}

//private static Card AddDot(Card note, Transform parent)
//{
//    Card dot = new GameObject(nameof(dot)).AddComponent<Card>();
//    dot.transform.SetParent(note.transform);
//    dot.transform.position = parent.position + (Vector3.right * -.01f);
//    dot.sprite = Assets.Dot;
//    return dot;
//}

//private static Card AddNote(Note n, Transform parent)
//{
//    Card note = new GameObject(n.QuantizedRhythmicNoteValue.ToString()).AddComponent<Card>();
//    note.transform.SetParent(parent);
//    note.transform.position = parent.position;
//    return note;
//}

//private static Card AddTie(Note n, Card note, Transform parent)
//{
//    Card tie = new GameObject(nameof(tie)).AddComponent<Card>();
//    tie.transform.SetParent(parent);
//    tie.transform.position = GetTieLocation(note, n);
//    tie.transform.localScale = GetTieSize(n);
//    tie.sprite = Assets.Tie;
//    return tie;
//}

//private static Vector3 GetTieLocation(Card sr, Note n) => n.ParentCell.Quantizement switch
//{
//    Quantizement.Quarter => sr.transform.position + (Vector3.right * .02f),

//    _ => n.BeatLocation switch
//    {
//        BeatLocation.For => sr.transform.position + (Vector3.right * .015f),//good
//        BeatLocation.ForE => sr.transform.position + (Vector3.right * .015f),
//        BeatLocation.ForN => sr.transform.position + (Vector3.right * .01f),
//        BeatLocation.ForA => sr.transform.position + (Vector3.right * .0125f),
//        BeatLocation.ForT => sr.transform.position + (Vector3.right * .01625f),
//        BeatLocation.ForL => sr.transform.position + (Vector3.right * .01125f),
//        BeatLocation.Thr => sr.transform.position + (Vector3.right * .015f),
//        BeatLocation.ThrE => sr.transform.position + (Vector3.right * .0125f),
//        BeatLocation.ThrN => sr.transform.position + (Vector3.right * .01f),//good
//        BeatLocation.ThrA => sr.transform.position + Vector3.right * .0075f,
//        BeatLocation.ThrT => sr.transform.position + (Vector3.right * .01125f),
//        BeatLocation.ThrL => sr.transform.position + (Vector3.right * .0125f),
//        BeatLocation.One => sr.transform.position + (Vector3.right * .01f),
//        BeatLocation.OneE => sr.transform.position + (Vector3.right * .01f),
//        BeatLocation.OneT => sr.transform.position + (Vector3.right * .01f),
//        BeatLocation.OneN => sr.transform.position + (Vector3.right * .01f),//good
//        BeatLocation.OneL => sr.transform.position + (Vector3.right * .0125f),
//        BeatLocation.OneA => sr.transform.position + (Vector3.right * .01f),
//        BeatLocation.Two => sr.transform.position + (Vector3.right * .01f),//good
//        BeatLocation.TwoE => sr.transform.position + (Vector3.right * .01f),
//        BeatLocation.TwoT => sr.transform.position + (Vector3.right * .01f),
//        BeatLocation.TwoN => sr.transform.position,//good
//        BeatLocation.TwoL => sr.transform.position + (Vector3.right * .0125f),
//        BeatLocation.TwoA => sr.transform.position,
//        _ => sr.transform.position,
//    },
//};

//private static Vector3 GetTieSize(Note n) => n.ParentCell.Quantizement switch
//{
//    Quantizement.Quarter => n.BeatLocation.MeasureNumber switch
//    {
//        MeasureNumber.Two => (ScaledToFit(
//            NotePosition(n.BeatLocation),
//            NotePosition(BeatLocation.ForA)) + Vector2.right) * new Vector3(2, 1),
//        _ => ScaledToFit(
//            NotePosition(n.MeasureNumber, n.BeatLocation),
//            NotePosition(n.MeasureNumber + 1, BeatLocation.One)) * new Vector3(2, 1),
//    },

//    Quantizement.QuarterTrips => (n.MeasureNumber, n.BeatLocation) switch
//    {
//        (_, BeatLocation.OneL) => ScaledToFit(
//        NotePosition(n.MeasureNumber, n.BeatLocation),
//        NotePosition(n.MeasureNumber, BeatLocation.Thr)) * new Vector3(2, 1),

//        (_, BeatLocation.TwoT) => ScaledToFit(
//        NotePosition(n.MeasureNumber, n.BeatLocation),
//        NotePosition(n.MeasureNumber, BeatLocation.Thr)) * new Vector3(2, 1),

//        (_, BeatLocation.TwoL) => ScaledToFit(
//        NotePosition(n.MeasureNumber, n.BeatLocation),
//        NotePosition(n.MeasureNumber, BeatLocation.Thr)) * new Vector3(2, 1),

//        (MeasureNumber.Two, _) => (ScaledToFit(
//            NotePosition(n.MeasureNumber, n.BeatLocation),
//            NotePosition(MeasureNumber.Two, BeatLocation.ForA)) + Vector2.right) * new Vector3(2, 1),

//        _ => ScaledToFit(
//           NotePosition(n.MeasureNumber, n.BeatLocation),
//           NotePosition(n.MeasureNumber + 1, BeatLocation.One)) * new Vector3(2, 1),
//    },

//    Quantizement.EighthTrips => (n.MeasureNumber, n.BeatLocation) switch
//    {
//        (MeasureNumber.Two, BeatLocation.For) => (ScaledToFit(
//             NotePosition(n.MeasureNumber, n.BeatLocation),
//             NotePosition(MeasureNumber.Two, BeatLocation.ForA)) + Vector2.right) * new Vector3(2, 1),

//        (MeasureNumber.Two, BeatLocation.ForT) => (ScaledToFit(
//             NotePosition(n.MeasureNumber, n.BeatLocation),
//             NotePosition(MeasureNumber.Two, BeatLocation.ForA)) + Vector2.right) * new Vector3(2, 1),

//        (MeasureNumber.Two, BeatLocation.ForL) => (ScaledToFit(
//             NotePosition(n.MeasureNumber, n.BeatLocation),
//             NotePosition(MeasureNumber.Two, BeatLocation.ForA)) + Vector2.right) * new Vector3(2, 1),

//        _ when n.BeatLocation < BeatLocation.Two => ScaledToFit(
//        NotePosition(n.MeasureNumber, n.BeatLocation),
//        NotePosition(n.MeasureNumber, BeatLocation.Two)) * new Vector3(2, 1),

//        _ when n.BeatLocation < BeatLocation.Thr => ScaledToFit(
//       NotePosition(n.MeasureNumber, n.BeatLocation),
//       NotePosition(n.MeasureNumber, BeatLocation.Thr)) * new Vector3(2, 1),

//        _ when n.BeatLocation < BeatLocation.For => ScaledToFit(
//       NotePosition(n.MeasureNumber, n.BeatLocation),
//       NotePosition(n.MeasureNumber, BeatLocation.For)) * new Vector3(2, 1),

//        _ => ScaledToFit(
//       NotePosition(n.MeasureNumber, n.BeatLocation),
//       NotePosition(n.MeasureNumber + 1, BeatLocation.One)) * new Vector3(2, 1),
//    },

//    Quantizement.Eighth => (n.MeasureNumber, n.BeatLocation) switch
//    {
//        (MeasureNumber.Two, BeatLocation.Thr) => (ScaledToFit(
//             NotePosition(n.MeasureNumber, n.BeatLocation),
//             NotePosition(MeasureNumber.Two, BeatLocation.ForA)) + Vector2.right) * new Vector3(2, 1),

//        (MeasureNumber.Two, BeatLocation.ThrN) => (ScaledToFit(
//             NotePosition(n.MeasureNumber, n.BeatLocation),
//             NotePosition(MeasureNumber.Two, BeatLocation.ForA)) + Vector2.right) * new Vector3(2, 1),

//        (MeasureNumber.Two, BeatLocation.For) => (ScaledToFit(
//             NotePosition(n.MeasureNumber, n.BeatLocation),
//             NotePosition(MeasureNumber.Two, BeatLocation.ForA)) + Vector2.right) * new Vector3(2, 1),

//        (MeasureNumber.Two, BeatLocation.ForN) => (ScaledToFit(
//             NotePosition(n.MeasureNumber, n.BeatLocation),
//             NotePosition(MeasureNumber.Two, BeatLocation.ForA)) + Vector2.right) * new Vector3(2, 1),

//        _ when n.BeatLocation < BeatLocation.Thr => ScaledToFit(
//       NotePosition(n.MeasureNumber, n.BeatLocation),
//       NotePosition(n.MeasureNumber, BeatLocation.Thr)) * new Vector3(2, 1),

//        _ => ScaledToFit(
//       NotePosition(n.MeasureNumber, n.BeatLocation),
//       NotePosition(n.MeasureNumber + 1, BeatLocation.One)) * new Vector3(2, 1),
//    },

//    Quantizement.Sixteenth => (n.MeasureNumber, n.BeatLocation) switch
//    {
//        (MeasureNumber.Two, BeatLocation.For) => (ScaledToFit(
//             NotePosition(n.MeasureNumber, n.BeatLocation),
//             NotePosition(MeasureNumber.Two, BeatLocation.ForA)) + Vector2.right) * new Vector3(2, 1),

//        (MeasureNumber.Two, BeatLocation.ForE) => (ScaledToFit(
//             NotePosition(n.MeasureNumber, n.BeatLocation),
//             NotePosition(MeasureNumber.Two, BeatLocation.ForA)) + Vector2.right) * new Vector3(2, 1),

//        (MeasureNumber.Two, BeatLocation.ForN) => (ScaledToFit(
//             NotePosition(n.MeasureNumber, n.BeatLocation),
//             NotePosition(MeasureNumber.Two, BeatLocation.ForA)) + Vector2.right) * new Vector3(2, 1),

//        (MeasureNumber.Two, BeatLocation.ForA) => (ScaledToFit(
//             NotePosition(n.MeasureNumber, n.BeatLocation),
//             NotePosition(MeasureNumber.Two, BeatLocation.ForA)) + Vector2.right) * new Vector3(2, 1),

//        _ when n.BeatLocation < BeatLocation.Two => ScaledToFit(
//        NotePosition(n.MeasureNumber, n.BeatLocation),
//        NotePosition(n.MeasureNumber, BeatLocation.Two)) * new Vector3(2, 1),

//        _ when n.BeatLocation < BeatLocation.Thr => ScaledToFit(
//       NotePosition(n.MeasureNumber, n.BeatLocation),
//       NotePosition(n.MeasureNumber, BeatLocation.Thr)) * new Vector3(2, 1),

//        _ when n.BeatLocation < BeatLocation.For => ScaledToFit(
//       NotePosition(n.MeasureNumber, n.BeatLocation),
//       NotePosition(n.MeasureNumber, BeatLocation.For)) * new Vector3(2, 1),

//        _ => ScaledToFit(
//       NotePosition(n.MeasureNumber, n.BeatLocation),
//       NotePosition(n.MeasureNumber + 1, BeatLocation.One)) * new Vector3(2, 1),
//    },

//    _ => Vector2.one,
//};


//static Vector2 MeasureOne(BeatLocation bl)
//{

//    return new Vector2(
//           (((int)bl) * ((((Cam.Io.Camera.orthographicSize * 2)) - 1) / 132))
//           - ((Cam.Io.Camera.orthographicSize)),
//           2.25f) + Vector2.left * .050f;
//    //return new Vector2(
//    //       (((int)bl) * ((((MyCam.Io.Cam.orthographicSize * 2 * MyCam.Io.Cam.aspect)) - 1) / 132))
//    //       - ((MyCam.Io.Cam.orthographicSize * MyCam.Io.Cam.aspect) - (MyCam.Io.Cam.aspect * .5f)),
//    //       2.25f) + Vector2.left * .35f;
//}
//static Vector2 MeasureTwo(BeatLocation bl)
//{
//    return new Vector2(
//          (((int)bl + 64) * ((((Cam.Io.Camera.orthographicSize * 2)) - 1) / 132))
//          - ((Cam.Io.Camera.orthographicSize)),
//          2.255f) + Vector2.right * 1.050f;
//    //return new Vector2(
//    //       (((int)bl + 64) * ((((MyCam.Io.Cam.orthographicSize * 2 * MyCam.Io.Cam.aspect)) - 1) / 132))
//    //       - ((MyCam.Io.Cam.orthographicSize * MyCam.Io.Cam.aspect) - (MyCam.Io.Cam.aspect * .5f)),
//    //       2.255f) + Vector2.left * .35f;
//}
//static Vector2 MeasureThree(BeatLocation bl)
//{
//    return new Vector2(
//           (((int)bl) * ((((Cam.Io.Camera.orthographicSize * 2)) - 1) / 132))
//           - ((Cam.Io.Camera.orthographicSize)),
//          -1.25f) + Vector2.left * .050f;
//    //return new Vector2(
//    //       (((int)bl) * ((((MyCam.Io.Cam.orthographicSize * 2 * MyCam.Io.Cam.aspect)) - 1) / 132))
//    //       - ((MyCam.Io.Cam.orthographicSize * MyCam.Io.Cam.aspect) - (MyCam.Io.Cam.aspect * .5f)),
//    //      -1.25f) + Vector2.left * .35f;

//}
//static Vector2 MeasureFour(BeatLocation bl)
//{
//    return new Vector2(
//           (((int)bl + 64) * ((((Cam.Io.Camera.orthographicSize * 2)) - 1) / 132))
//           - ((Cam.Io.Camera.orthographicSize)),
//         -1.255f) + Vector2.right * 1.050f;
//    //return new Vector2(
//    //       (((int)bl + 64) * ((((MyCam.Io.Cam.orthographicSize * 2 * MyCam.Io.Cam.aspect)) - 1) / 132))
//    //       - ((MyCam.Io.Cam.orthographicSize * MyCam.Io.Cam.aspect) - (MyCam.Io.Cam.aspect * .5f)),
//    //     -1.255f) + Vector2.left * .35f;
//}



//    string GetString(int i)
//    {
//        return ms.RhythmSpecs.SubDivisionTier switch
//        {

//            SubDivisionTier.QuartersOnly => BeatIndexToNoteLocation(i).Item2 switch
//            {
//                Count.One => "1",
//                Count.Two => "2",
//                Count.Thr => "3",
//                Count.For => "4",
//                _ => ""
//            },
//            SubDivisionTier.QuartersAndEighths => (BeatIndexToNoteLocation(i).Item2 switch
//            {
//                BeatLocation.One => "1",
//                BeatLocation.Two => "2",
//                BeatLocation.Thr => "3",
//                BeatLocation.For => "4",
//                BeatLocation.OneN => "+",
//                BeatLocation.TwoN => "+",
//                BeatLocation.ThrN => "+",
//                BeatLocation.ForN => "+",
//                _ => ""
//            },
//            SubDivisionTier.EighthsOnly => BeatIndexToNoteLocation(i).Item2 switch
//            {
//                BeatLocation.One => "1",
//                BeatLocation.Two => "2",
//                BeatLocation.Thr => "3",
//                BeatLocation.For => "4",
//                BeatLocation.OneN => "+",
//                BeatLocation.TwoN => "+",
//                BeatLocation.ThrN => "+",
//                BeatLocation.ForN => "+",
//                _ => ""
//            },
//            SubDivisionTier.EighthsAndSixteenths => BeatIndexToNoteLocation(i).Item2 switch
//            {
//                BeatLocation.One => "1",
//                BeatLocation.Two => "2",
//                BeatLocation.Thr => "3",
//                BeatLocation.For => "4",
//                BeatLocation.OneN => "+",
//                BeatLocation.TwoN => "+",
//                BeatLocation.ThrN => "+",
//                BeatLocation.ForN => "+",
//                BeatLocation.OneE => "e",
//                BeatLocation.TwoE => "e",
//                BeatLocation.ThrE => "e",
//                BeatLocation.ForE => "e",
//                BeatLocation.OneA => "a",
//                BeatLocation.TwoA => "a",
//                BeatLocation.ThrA => "a",
//                BeatLocation.ForA => "a",
//                _ => ""
//            },

//            SubDivisionTier.SixteenthsOnly => BeatIndexToNoteLocation(i).Item2 switch
//            {
//                BeatLocation.One => "1",
//                BeatLocation.Two => "2",
//                BeatLocation.Thr => "3",
//                BeatLocation.For => "4",
//                BeatLocation.OneN => "+",
//                BeatLocation.TwoN => "+",
//                BeatLocation.ThrN => "+",
//                BeatLocation.ForN => "+",
//                BeatLocation.OneE => "e",
//                BeatLocation.TwoE => "e",
//                BeatLocation.ThrE => "e",
//                BeatLocation.ForE => "e",
//                BeatLocation.OneA => "a",
//                BeatLocation.TwoA => "a",
//                BeatLocation.ThrA => "a",
//                BeatLocation.ForA => "a",
//                _ => ""
//            },
//        };
//        return BeatIndexToNoteLocation(i).Item2 switch
//        {
//            BeatLocation.One => "1",
//            BeatLocation.Two => "2",
//            BeatLocation.Thr => "3",
//            BeatLocation.For => "4",
//            BeatLocation.OneN => "+",
//            BeatLocation.TwoN => "+",
//            BeatLocation.ThrN => "+",
//            BeatLocation.ForN => "+",
//            BeatLocation.OneE => "",
//            BeatLocation.TwoE => "",
//            BeatLocation.ThrE => "",
//            BeatLocation.ForE => "",
//            BeatLocation.OneA => "",
//            BeatLocation.TwoA => "",
//            BeatLocation.ThrA => "",
//            BeatLocation.ForA => "",
//            BeatLocation.OneT => "",
//            BeatLocation.TwoT => "",
//            BeatLocation.ThrT => "",
//            BeatLocation.ForT => "",
//            BeatLocation.OneL => "",
//            BeatLocation.TwoL => "",
//            BeatLocation.ThrL => "",
//            BeatLocation.ForL => "",
//            _ => "s",
//        };
//    //}
//}



//public static (MeasureNumber, Count, SubBeatAssignment) BeatIndexToNoteLocation(int beatIndex)
//{
//    return beatIndex switch
//    {
//        00 + 00 => (MeasureNumber.One, Count.One, SubBeatAssignment.D),
//        01 + 00 => (MeasureNumber.One, Count.One, SubBeatAssignment.E),
//        02 + 00 => (MeasureNumber.One, Count.One, SubBeatAssignment.N),
//        03 + 00 => (MeasureNumber.One, Count.One, SubBeatAssignment.A),
//        04 + 00 => (MeasureNumber.One, Count.Two, SubBeatAssignment.D),
//        05 + 00 => (MeasureNumber.One, Count.Two, SubBeatAssignment.E),
//        06 + 00 => (MeasureNumber.One, Count.Two, SubBeatAssignment.N),
//        07 + 00 => (MeasureNumber.One, Count.Two, SubBeatAssignment.A),
//        08 + 00 => (MeasureNumber.One, Count.Thr, SubBeatAssignment.D),
//        09 + 00 => (MeasureNumber.One, Count.Thr, SubBeatAssignment.E),
//        10 + 00 => (MeasureNumber.One, Count.Thr, SubBeatAssignment.N),
//        11 + 00 => (MeasureNumber.One, Count.Thr, SubBeatAssignment.A),
//        12 + 00 => (MeasureNumber.One, Count.For, SubBeatAssignment.D),
//        13 + 00 => (MeasureNumber.One, Count.For, SubBeatAssignment.E),
//        14 + 00 => (MeasureNumber.One, Count.For, SubBeatAssignment.N),
//        15 + 00 => (MeasureNumber.One, Count.For, SubBeatAssignment.A),
//        00 + 16 => (MeasureNumber.Two, Count.One, SubBeatAssignment.D),
//        01 + 16 => (MeasureNumber.Two, Count.One, SubBeatAssignment.E),
//        02 + 16 => (MeasureNumber.Two, Count.One, SubBeatAssignment.N),
//        03 + 16 => (MeasureNumber.Two, Count.One, SubBeatAssignment.A),
//        04 + 16 => (MeasureNumber.Two, Count.Two, SubBeatAssignment.D),
//        05 + 16 => (MeasureNumber.Two, Count.Two, SubBeatAssignment.E),
//        06 + 16 => (MeasureNumber.Two, Count.Two, SubBeatAssignment.N),
//        07 + 16 => (MeasureNumber.Two, Count.Two, SubBeatAssignment.A),
//        08 + 16 => (MeasureNumber.Two, Count.Thr, SubBeatAssignment.D),
//        09 + 16 => (MeasureNumber.Two, Count.Thr, SubBeatAssignment.E),
//        10 + 16 => (MeasureNumber.Two, Count.Thr, SubBeatAssignment.N),
//        11 + 16 => (MeasureNumber.Two, Count.Thr, SubBeatAssignment.A),
//        12 + 16 => (MeasureNumber.Two, Count.For, SubBeatAssignment.D),
//        13 + 16 => (MeasureNumber.Two, Count.For, SubBeatAssignment.E),
//        14 + 16 => (MeasureNumber.Two, Count.For, SubBeatAssignment.N),
//        15 + 16 => (MeasureNumber.Two, Count.For, SubBeatAssignment.A),
//        00 + 32 => (MeasureNumber.Thr, Count.One, SubBeatAssignment.D),
//        01 + 32 => (MeasureNumber.Thr, Count.One, SubBeatAssignment.E),
//        02 + 32 => (MeasureNumber.Thr, Count.One, SubBeatAssignment.N),
//        03 + 32 => (MeasureNumber.Thr, Count.One, SubBeatAssignment.A),
//        04 + 32 => (MeasureNumber.Thr, Count.Two, SubBeatAssignment.D),
//        05 + 32 => (MeasureNumber.Thr, Count.Two, SubBeatAssignment.E),
//        06 + 32 => (MeasureNumber.Thr, Count.Two, SubBeatAssignment.N),
//        07 + 32 => (MeasureNumber.Thr, Count.Two, SubBeatAssignment.A),
//        08 + 32 => (MeasureNumber.Thr, Count.Thr, SubBeatAssignment.D),
//        09 + 32 => (MeasureNumber.Thr, Count.Thr, SubBeatAssignment.E),
//        10 + 32 => (MeasureNumber.Thr, Count.Thr, SubBeatAssignment.N),
//        11 + 32 => (MeasureNumber.Thr, Count.Thr, SubBeatAssignment.A),
//        12 + 32 => (MeasureNumber.Thr, Count.For, SubBeatAssignment.D),
//        13 + 32 => (MeasureNumber.Thr, Count.For, SubBeatAssignment.E),
//        14 + 32 => (MeasureNumber.Thr, Count.For, SubBeatAssignment.N),
//        15 + 32 => (MeasureNumber.Thr, Count.For, SubBeatAssignment.A),
//        00 + 48 => (MeasureNumber.For, Count.One, SubBeatAssignment.D),
//        01 + 48 => (MeasureNumber.For, Count.One, SubBeatAssignment.E),
//        02 + 48 => (MeasureNumber.For, Count.One, SubBeatAssignment.N),
//        03 + 48 => (MeasureNumber.For, Count.One, SubBeatAssignment.A),
//        04 + 48 => (MeasureNumber.For, Count.Two, SubBeatAssignment.D),
//        05 + 48 => (MeasureNumber.For, Count.Two, SubBeatAssignment.E),
//        06 + 48 => (MeasureNumber.For, Count.Two, SubBeatAssignment.N),
//        07 + 48 => (MeasureNumber.For, Count.Two, SubBeatAssignment.A),
//        08 + 48 => (MeasureNumber.For, Count.Thr, SubBeatAssignment.D),
//        09 + 48 => (MeasureNumber.For, Count.Thr, SubBeatAssignment.E),
//        10 + 48 => (MeasureNumber.For, Count.Thr, SubBeatAssignment.N),
//        11 + 48 => (MeasureNumber.For, Count.Thr, SubBeatAssignment.A),
//        12 + 48 => (MeasureNumber.For, Count.For, SubBeatAssignment.D),
//        13 + 48 => (MeasureNumber.For, Count.For, SubBeatAssignment.E),
//        14 + 48 => (MeasureNumber.For, Count.For, SubBeatAssignment.N),
//        15 + 48 => (MeasureNumber.For, Count.For, SubBeatAssignment.A),
//        _ => (MeasureNumber.One, Count.One, SubBeatAssignment.D)
//    };
//}
//}

//this was under subcounts per measure
//if (ms.RhythmSpecs.RhythmOptions.Contains(RhythmOption.TripsOnly))
//{
//    switch (ms.RhythmSpecs.SubDivisionTier)
//    {
//        case SubDivisionTier.BeatOnly:
//            for (int i = 0; i < (int)ms.RhythmSpecs.Time.Signature.Quantity; i++)
//            {
//                beats.Add(SubBeatAssignment.D);
//            };
//            break;

//        case SubDivisionTier.BeatAndD1:
//            for (int i = 0; i < (int)ms.RhythmSpecs.Time.Signature.Quantity; i++)
//            {
//                beats.Add(SubBeatAssignment.D);
//                beats.Add(SubBeatAssignment.T);
//                beats.Add(SubBeatAssignment.L);
//            };
//            break;

//        case SubDivisionTier.D1Only:
//            for (int i = 0; i < (int)ms.RhythmSpecs.Time.Signature.Quantity; i++)
//            {
//                beats.Add(SubBeatAssignment.D);
//                beats.Add(SubBeatAssignment.T);
//                beats.Add(SubBeatAssignment.L);
//            };
//            break;
//    }
//}
