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
            Card TimeSignature = ms.Card.CreateChild(nameof(TimeSignature), ms.Card.Canvas.transform, ms.Card.Canvas)
                   .SetTextString((int)ms.RhythmSpecs.Time.Signature.Quantity + "\n" + (int)ms.RhythmSpecs.Time.Signature.Quality)
                   .SetTextAlignment(TMPro.TextAlignmentOptions.Center)
                   .SetTMPPosition(MeasurePos(MeasureNumber.One) + (.56f * Cam.OrthoX * Vector3.left))
                   .SetFontScale(.76f, .76f)
                   .AutoSizeFont(true)
                   .AutoSizeTextContainer(true);
        }

        private static void SetUpStaff(this MusicSheet ms)
        {
            Card Measure1Staff = ms.Card.CreateChild(nameof(Measure1Staff), ms.Card.Canvas.transform, ms.Card.Canvas)
                 .SetImageSprite(SheetMusicAssets.StaffDoubleLeft)
                 .SetImageSize(new Vector3(Cam.OrthoX * .895f, Cam.OrthoX * .5f, 1))
                 .SetImagePosition(MeasurePos(MeasureNumber.One));

            switch (ms.Measures.Length)
            {
                case 2:
                    Card Measure2Staff = ms.Card.CreateChild(nameof(Measure2Staff), ms.Card.Canvas.transform, ms.Card.Canvas)
                        .SetImageSprite(SheetMusicAssets.StaffDoubleRight)
                        .SetImageSize(new Vector3(Cam.OrthoX * .895f, Cam.OrthoX * .5f, 1))
                        .SetImagePosition(MeasurePos(MeasureNumber.Two));
                    break;

                case 4:
                    ms.Card.CreateChild(nameof(Measure2Staff), ms.Card.Canvas.transform, ms.Card.Canvas)
                        .SetImageSprite(SheetMusicAssets.Staff)
                        .SetImageSize(new Vector3(Cam.OrthoX * .895f, Cam.OrthoX * .5f, 1))
                        .SetImagePosition(MeasurePos(MeasureNumber.Two));
                    Card Measure3Staff = ms.Card.CreateChild(nameof(Measure3Staff), ms.Card.Canvas.transform, ms.Card.Canvas)
                        .SetImageSprite(SheetMusicAssets.Staff)
                        .SetImageSize(new Vector3(Cam.OrthoX * .895f, Cam.OrthoX * .5f, 1))
                        .SetImagePosition(MeasurePos(MeasureNumber.Thr));
                    Card Measure4Staff = ms.Card.CreateChild(nameof(Measure4Staff), ms.Card.Canvas.transform, ms.Card.Canvas)
                        .SetImageSprite(SheetMusicAssets.StaffDoubleRight)
                        .SetImageSize(new Vector3(Cam.OrthoX * .895f, Cam.OrthoX * .5f, 1))
                        .SetImagePosition(MeasurePos(MeasureNumber.For));
                    break;
            };
        }

        private static void SetUpCounts(this MusicSheet ms, SubBeatAssignment[] beats)
        {
            for (int i = 0; i < ms.Measures.Length; i++)
            {
                int c = 0;
                int count = 0;
                for (int ii = 0; ii < beats.Length; ii++)
                {
                    if (beats[c] is D) count++;
                    Card card = ms.Card.CreateChild(nameof(Count) + ": " + count + BeatToString(beats[c]), ms.Card.Canvas.transform, ms.Card.Canvas)
                          .SetTextString(beats[c] is D ? count.ToString() : BeatToString(beats[c]))
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

                    c++;
                }
            }

            static string BeatToString(SubBeatAssignment b)
            {
                return b switch
                {
                    N => "+",
                    E => "e",
                    T => "t",
                    L => "l",
                    A => "a",
                    D => "D",
                    _ => "?"
                };
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
            _ when m == MeasureNumber.One => (Cam.OrthoX * .515f * Vector2.left) + (Vector2.up * 3),
            _ when m == MeasureNumber.Two => (Cam.OrthoX * .515f * Vector2.right) + (Vector2.up * 3),
            _ when m == MeasureNumber.Thr => (Cam.OrthoX * .515f * Vector2.left) + (Vector2.down * 1),
            _ when m == MeasureNumber.For => (Cam.OrthoX * .515f * Vector2.right) + (Vector2.down * 1),
            _ => Vector2.zero
        };

        static Vector3 CountPos(Count counts, Count c, SubBeatAssignment b)
        {
            float pointA = -Cam.OrthoX * .45f;
            float pointB = pointA * -1;
            float interval = (pointB - pointA) / ((int)counts * 12);
            float x = (((int)c - 1) * 12) + (int)b;
            return new Vector3(pointA + (x * interval), 0, 0);
        }

        static void AssignNoteSprites(this MusicSheet ms)
        {
            Note[] notes = ms.Notes;

            AddNoteHeads();
            AddBeamsOrFlags();

            void AddNoteHeads()
            {
                string GetName(int i)
                {
                    return
                    notes[i].BeatLocation.MeasureNumber.Name + " " +
                            notes[i].BeatLocation.BeatAssignment.Count.Name + " " +
                            notes[i].BeatLocation.BeatAssignment.SubBeatAssignment.Name + " " +
                            notes[i].QuantizedRhythmicValue;
                }

                for (int i = 0; i < notes.Length; i++)
                {
                    notes[i].Card = ms.Card.CreateChild(GetName(i), ms.Card.Canvas.transform, ms.Card.Canvas)
                         .SetImagePosition(NotePosition(ms, notes[i].BeatLocation))
                         .SetImageSize(new Vector3(Cam.OrthoX * .895f, Cam.OrthoX * .5f, 1));

                    if (notes[i].Rest)
                    {
                        notes[i].Card.SetImageSprite(notes[i].QuantizedRhythmicValue switch
                        {
                            RhythmicValue.Half => SheetMusicAssets.HalfRest,
                            RhythmicValue.TripHalf => SheetMusicAssets.HalfRest,
                            RhythmicValue.DotHalf => SheetMusicAssets.HalfRest,
                            RhythmicValue.Eighth => SheetMusicAssets.EighthRest,
                            RhythmicValue.DotEighth => SheetMusicAssets.EighthRest,
                            RhythmicValue.TripEighth => SheetMusicAssets.EighthRest,
                            RhythmicValue.Whole => SheetMusicAssets.WholeRest,
                            RhythmicValue.DotWhole => SheetMusicAssets.WholeRest,
                            RhythmicValue.Sixteenth => SheetMusicAssets.SixteenthRest,
                            _ => SheetMusicAssets.QuarterRest,
                        });
                    }
                    else
                    {
                        notes[i].Card.SetImageSprite(notes[i].QuantizedRhythmicValue switch
                        {
                            RhythmicValue.Half => SheetMusicAssets.WhiteNote,
                            RhythmicValue.DotHalf => SheetMusicAssets.WhiteNote,
                            RhythmicValue.TripHalf => SheetMusicAssets.WhiteNote,
                            RhythmicValue.Whole => SheetMusicAssets.WhiteNote,
                            RhythmicValue.DotWhole => SheetMusicAssets.WhiteNote,
                            _ => SheetMusicAssets.BlackNote,
                        });
                    }

                    if (notes[i].QuantizedRhythmicValue == RhythmicValue.DotEighth ||
                        notes[i].QuantizedRhythmicValue == RhythmicValue.DotQuarter ||
                        notes[i].QuantizedRhythmicValue == RhythmicValue.DotHalf ||
                        notes[i].QuantizedRhythmicValue == RhythmicValue.DotWhole)
                    {
                        Card dot = notes[i].Card.CreateChild(nameof(dot), notes[i].Card.Image.transform, notes[i].Card.Canvas)
                            .SetImageSizeUnscaled(notes[i].Card.Image.rectTransform.sizeDelta)
                            .SetImagePosition(Vector3.zero)
                            .SetImageSprite(SheetMusicAssets.Dot);
                    }

                    if ((notes[i].QuantizedRhythmicValue == RhythmicValue.Half ||
                         notes[i].QuantizedRhythmicValue == RhythmicValue.DotHalf ||
                         notes[i].QuantizedRhythmicValue == RhythmicValue.TripHalf) &&
                        !notes[i].Rest)
                    {
                        Card stem = notes[i].Card.CreateChild(nameof(stem), notes[i].Card.Image.transform, notes[i].Card.Canvas)
                            .SetImageSizeUnscaled(notes[i].Card.Image.rectTransform.sizeDelta)
                            .SetImagePosition(Vector3.zero)
                            .SetImageSprite(SheetMusicAssets.Stem);
                    }
                }

            }

            void AddBeamsOrFlags()
            {
                for (int i = 0; i < notes.Length; i++)
                {
                    if ((notes[i].QuantizedRhythmicValue != RhythmicValue.DotEighth &&
                         notes[i].QuantizedRhythmicValue != RhythmicValue.Eighth &&
                         notes[i].QuantizedRhythmicValue != RhythmicValue.TripEighth &&
                         notes[i].QuantizedRhythmicValue != RhythmicValue.Sixteenth) ||
                         notes[i].Rest) continue;

                    bool toflag = true;
                    bool ef = false, er = false, sf = false, sr = false;
                    bool sfb = false, srb = false, efb = false, erb = false;

                    if (i < notes.Length - 1 && notes[i + 1].ParentCell == notes[i].ParentCell &&
                       (notes[i + 1].QuantizedRhythmicValue == RhythmicValue.Sixteenth ||
                        notes[i + 1].QuantizedRhythmicValue == RhythmicValue.Eighth ||
                        notes[i + 1].QuantizedRhythmicValue == RhythmicValue.DotEighth ||
                        notes[i + 1].QuantizedRhythmicValue == RhythmicValue.TripEighth))
                    {
                        ef = true;
                        efb = true;
                        toflag = false;
                    }

                    if (i > 0 && !notes[i - 1].Rest && notes[i - 1].ParentCell == notes[i].ParentCell &&
                       (notes[i - 1].QuantizedRhythmicValue == RhythmicValue.Sixteenth ||
                        notes[i - 1].QuantizedRhythmicValue == RhythmicValue.Eighth ||
                        notes[i - 1].QuantizedRhythmicValue == RhythmicValue.DotEighth ||
                        notes[i - 1].QuantizedRhythmicValue == RhythmicValue.TripEighth))
                    {
                        er = true;
                        erb = true;
                        toflag = false;
                    }

                    if (notes[i].QuantizedRhythmicValue == RhythmicValue.Sixteenth && !notes[i].Rest)
                    {
                        switch (notes[i].ParentCell.Shape)
                        {
                            case CellShape.LS:
                                if (!notes[i - 1].Rest)
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
                                sf = efb = i < notes.Length - 1 && notes[i + 1].ParentCell == notes[i].ParentCell;
                                sr = erb = i > 0 && notes[i - 1].ParentCell == notes[i].ParentCell && !notes[i - 1].Rest;
                                break;

                            case CellShape.SSL:
                                toflag = false;
                                sf = sfb = i < notes.Length - 1 && notes[i + 1].ParentCell == notes[i].ParentCell && notes[i + 1].QuantizedRhythmicValue == RhythmicValue.Sixteenth;
                                sr = srb = erb = i > 0 && notes[i - 1].ParentCell == notes[i].ParentCell && notes[i - 1].QuantizedRhythmicValue == RhythmicValue.Sixteenth;// && !ns[i - 1].Rest;
                                efb = true;
                                break;

                            case CellShape.LSS:
                                toflag = false;
                                sf = sfb = efb = i < notes.Length - 1 && notes[i + 1].ParentCell == notes[i].ParentCell && notes[i + 1].QuantizedRhythmicValue == RhythmicValue.Sixteenth;
                                sr = srb = i > 0 && !notes[i - 1].Rest && notes[i - 1].ParentCell == notes[i].ParentCell && notes[i - 1].QuantizedRhythmicValue == RhythmicValue.Sixteenth; //&& !ns[i - 1].Rest;
                                erb = !notes[i - 1].Rest;
                                break;

                            case CellShape.SSSS:
                                toflag = false;
                                sf = sfb = efb = i < notes.Length - 1 && notes[i + 1].ParentCell == notes[i].ParentCell && notes[i + 1].QuantizedRhythmicValue == RhythmicValue.Sixteenth;
                                sr = srb = erb = i > 0 && !notes[i - 1].Rest && notes[i - 1].ParentCell == notes[i].ParentCell && notes[i - 1].QuantizedRhythmicValue == RhythmicValue.Sixteenth;
                                break;
                        }

                    }

                    if (toflag)
                    {
                        Card flag = notes[i].Card.CreateChild(nameof(flag), notes[i].Card.Image.transform, notes[i].Card.Canvas)
                            .SetImagePosition(Vector3.zero)
                            .SetImageSizeUnscaled(notes[i].Card.Image.rectTransform.sizeDelta)
                            .SetImageSprite(SheetMusicAssets.EighthFlag);
                        continue;
                    }
                    else if (sr && sf)
                    {
                        notes[i].Card.Image.sprite = SheetMusicAssets.SixteenthBoth;
                    }
                    else if (sr && !sf && !ef)
                    {
                        notes[i].Card.Image.sprite = SheetMusicAssets.SixteenthReverse;
                    }
                    else if (sr && !sf && ef)
                    {
                        notes[i].Card.Image.sprite = SheetMusicAssets.SixteenthReverseEighthForward;
                    }
                    else if (sf && !sr && !er)
                    {
                        notes[i].Card.Image.sprite = SheetMusicAssets.SixteenthForward;
                    }
                    else if (sf && !sr && er)
                    {
                        notes[i].Card.Image.sprite = SheetMusicAssets.SixteenthForwardEighthReverse;
                    }
                    else if (ef && er)
                    {
                        notes[i].Card.Image.sprite = SheetMusicAssets.EighthBoth;
                    }
                    else if (ef && !er)
                    {
                        notes[i].Card.Image.sprite = SheetMusicAssets.EighthForward;
                    }
                    else if (er && !ef)
                    {
                        notes[i].Card.Image.sprite = SheetMusicAssets.EighthReverse;
                    }

                    if (srb) { AddReverse16thBeamBridge(); }
                    if (sfb) { AddForward16thBeamBridge(); }
                    if (erb) { AddReverse8thBeamBridge(); }
                    if (efb) { AddForward8thBeamBridge(); }

                    void AddForward16thBeamBridge()
                    {
                        Card beam16BridgeF = notes[i].Card.CreateChild(nameof(beam16BridgeF), notes[i].Card.Image.transform, notes[i].Card.Canvas)
                            .SetImageSprite(SheetMusicAssets.SixteenthBeam)
                            .SetImageSizeUnscaled(new Vector3(notes[i].Card.Image.rectTransform.sizeDelta.x * (ScaledToFit(NotePosition(ms, notes[i].BeatLocation).x, NotePosition(ms, notes[i + 1].BeatLocation).x)), notes[i].Card.Image.rectTransform.sizeDelta.y, 1))
                            .SetImagePosition(Vector3.right * (ScaledToFit(NotePosition(ms, notes[i].BeatLocation).x, NotePosition(ms, notes[i + 1].BeatLocation).x) * .225f));
                    }
                    void AddReverse16thBeamBridge()
                    {
                        Card beam16BridgeR = notes[i].Card.CreateChild(nameof(beam16BridgeR), notes[i].Card.Image.transform, notes[i].Card.Canvas)
                            .SetImageSprite(SheetMusicAssets.SixteenthBeamReverse)
                            .SetImageSizeUnscaled(new Vector3(notes[i].Card.Image.rectTransform.sizeDelta.x * (ScaledToFit(NotePosition(ms, notes[i].BeatLocation).x, NotePosition(ms, notes[i - 1].BeatLocation).x)), notes[i].Card.Image.rectTransform.sizeDelta.y, 1))
                            .SetImagePosition(Vector3.left * (ScaledToFit(NotePosition(ms, notes[i].BeatLocation).x, NotePosition(ms, notes[i - 1].BeatLocation).x) * .1f));
                    }
                    void AddForward8thBeamBridge()
                    {
                        Card beam8BridgeF = notes[i].Card.CreateChild(nameof(beam8BridgeF), notes[i].Card.Image.transform, notes[i].Card.Canvas)
                            .SetImageSprite(SheetMusicAssets.EighthBeam)
                            .SetImageSizeUnscaled(new Vector3(notes[i].Card.Image.rectTransform.sizeDelta.x * (ScaledToFit(NotePosition(ms, notes[i].BeatLocation).x, NotePosition(ms, notes[i + 1].BeatLocation).x)), notes[i].Card.Image.rectTransform.sizeDelta.y, 1))
                            .SetImagePosition(Vector3.right * (ScaledToFit(NotePosition(ms, notes[i].BeatLocation).x, NotePosition(ms, notes[i + 1].BeatLocation).x) * .225f));
                    }
                    void AddReverse8thBeamBridge()
                    {
                        Card beam8BridgeR = notes[i].Card.CreateChild(nameof(beam8BridgeR), notes[i].Card.Image.transform, notes[i].Card.Canvas)
                            .SetImageSprite(SheetMusicAssets.EighthBeamReverse)
                            .SetImageSizeUnscaled(new Vector3(notes[i].Card.Image.rectTransform.sizeDelta.x * (ScaledToFit(NotePosition(ms, notes[i].BeatLocation).x, NotePosition(ms, notes[i - 1].BeatLocation).x)), notes[i].Card.Image.rectTransform.sizeDelta.y, 1))
                            .SetImagePosition(Vector3.left * (ScaledToFit(NotePosition(ms, notes[i].BeatLocation).x, NotePosition(ms, notes[i - 1].BeatLocation).x) * 0.1f));
                    }

                }
            }
        }

        static void AssignTies(this MusicSheet ms)
        {
            Note[] notes = ms.Notes;
            for (int i = 0; i < notes.Length; i++)
            {
                if (notes[i].ParentCell == ms.Measures[2].Cells[0] &&
                    ms.Measures[2].Cells[0].TiedFrom &&
                    notes[i].BeatLocation.BeatAssignment.SubBeatAssignment is D &&
                    notes[i].BeatLocation.BeatAssignment.Count == Count.One)
                {
                    //reverse tie
                    Card tie = notes[i].Card.CreateChild(nameof(tie), notes[i].Card.Image.transform, notes[i].Card.Canvas)
                        .SetImageSprite(SheetMusicAssets.Tie)
                        .SetImageSize(8.95f * new Vector3(ScaledToFit(NotePosition(ms, notes[i].BeatLocation).x, NotePosition(ms, notes[i].BeatLocation).x + 10f) * .1f, .35f, 1))
                        //.SetImageSizeUnscaled(new Vector3(notes[i].Card.Image.rectTransform.sizeDelta.x * (ScaledToFit(NotePosition(ms, notes[i].BeatLocation).x, NotePosition(ms, notes[i + 1].BeatLocation).x)), notes[i].Card.Image.rectTransform.sizeDelta.y, 1))
                        .SetImagePosition(Vector3.zero)
                        .OffsetImagePosition(new Vector3(-.5f, 0, 0));



                    //Transform tf = new GameObject("Tie").transform;
                    //tf.SetParent(notes[i].TF);
                    //tf.localScale = new Vector3(ScaledToFit(notes[i].TF.position.x, notes[i].TF.position.x + 1.5f) * -.1f, .5f, 1);
                    //tf.position = notes[i].TF.position;// + new Vector3(tf.localScale.x * .5f, 0, 0);
                    //SpriteRenderer sr = tf.gameObject.AddComponent<SpriteRenderer>();
                    //sr.sprite = SheetMusicAssets.Tie;
                }

                if (!notes[i].TiesTo) continue;

                if (notes[i].ParentCell == ms.Measures[1].Cells[^1])
                {
                    Card tie = notes[i].Card.CreateChild(nameof(tie), notes[i].Card.Image.transform, notes[i].Card.Canvas)
                        .SetImageSprite(SheetMusicAssets.Tie)
                        .SetImageSize(8.95f * new Vector3(ScaledToFit(NotePosition(ms, notes[i].BeatLocation).x, NotePosition(ms, notes[i].BeatLocation).x + 10f) * .1f, .35f, 1))
                        .SetImagePosition(Vector3.zero)
                        .OffsetImagePosition(new Vector3(.5f, 0, 0));

                    //Transform tf = new GameObject("Tie").transform;
                    //tf.SetParent(notes[i].TF);
                    //tf.localScale = new Vector3(ScaledToFit(notes[i].TF.position.x, notes[i].TF.position.x + 10f) * .1f, .5f, 1);
                    //tf.position = notes[i].TF.position;// + new Vector3(tf.localScale.x * .5f, 0, 0);
                    //SpriteRenderer sr = tf.gameObject.AddComponent<SpriteRenderer>();
                    //sr.sprite = SheetMusicAssets.Tie;
                }
                else
                {
                    Card tie = notes[i].Card.CreateChild(nameof(tie), notes[i].Card.Image.transform, notes[i].Card.Canvas)
                        .SetImageSprite(SheetMusicAssets.Tie)
                        .SetImageSize(8.95f * new Vector3(ScaledToFit(NotePosition(ms, notes[i].BeatLocation).x, NotePosition(ms, notes[i + 1].BeatLocation).x) * .1f, .35f, 1))
                        .SetImagePosition(Vector3.zero)
                        .OffsetImagePosition(new Vector3(.5f, 0, 0));

                    //Transform tf = new GameObject("Tie").transform;
                    //tf.SetParent(notes[i].TF);
                    //tf.localScale = new Vector3(ScaledToFit(notes[i].TF.position.x, notes[i + 1].TF.position.x) * .1f, .5f, 1);
                    //tf.position = notes[i].TF.position;// + new Vector3(tf.localScale.x * .5f, 0, 0);
                    //SpriteRenderer sr = tf.gameObject.AddComponent<SpriteRenderer>();
                    //sr.sprite = SheetMusicAssets.Tie;
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
                            beats.Add(new D());
                        };
                        break;

                    case SubDivisionTier.BeatAndD1:
                        for (int i = 0; i < (int)ms.RhythmSpecs.Time.Signature.Quantity; i++)
                        {
                            beats.Add(new D());
                            beats.Add(new N());
                        };
                        break;

                    case SubDivisionTier.D1Only:
                        for (int i = 0; i < (int)ms.RhythmSpecs.Time.Signature.Quantity; i++)
                        {
                            beats.Add(new D());
                            beats.Add(new N());
                        };
                        break;

                    case SubDivisionTier.D1AndD2:
                        for (int i = 0; i < (int)ms.RhythmSpecs.Time.Signature.Quantity; i++)
                        {
                            beats.Add(new D());
                            beats.Add(new E());
                            beats.Add(new N());
                            beats.Add(new A());
                        };
                        break;

                    case SubDivisionTier.D2Only:
                        for (int i = 0; i < (int)ms.RhythmSpecs.Time.Signature.Quantity; i++)
                        {
                            beats.Add(new D());
                            beats.Add(new E());
                            beats.Add(new N());
                            beats.Add(new A());
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
                            beats.Add(new D());
                        };
                        break;

                    case SubDivisionTier.BeatAndD1:
                        for (int i = 0; i < (int)ms.RhythmSpecs.Time.Signature.Quantity; i++)
                        {
                            beats.Add(new D());
                            beats.Add(new T());
                            beats.Add(new N());
                            beats.Add(new L());
                        };
                        break;

                    case SubDivisionTier.D1Only:
                        for (int i = 0; i < (int)ms.RhythmSpecs.Time.Signature.Quantity; i++)
                        {
                            beats.Add(new D());
                            beats.Add(new T());
                            beats.Add(new N());
                            beats.Add(new L());
                        };
                        break;
                }
            }

            return beats.ToArray();
        }

        static float ScaledToFit(float pointA, float pointB)
        {
            return Mathf.Abs(pointB - pointA);
        }
    }
}