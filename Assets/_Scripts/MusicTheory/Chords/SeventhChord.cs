using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MusicTheory.SeventhChords
{
    public interface ISeventhChord : IMusicalElement
    {
        SeventhChordEnum Enum { get; }
        int IMusicalElement.Id => Enum.Id;
        int IMusicalElement.SN => Enum.SN;
        string IMusicalElement.Name => Enum.Name;
        string Description => Enum.Description;
    }

    [Serializable] public readonly struct MajorSeventh : ISeventhChord { public readonly SeventhChordEnum Enum => SeventhChordEnum.MajorSeventh; }
    [Serializable] public readonly struct MinorSeventh : ISeventhChord { public readonly SeventhChordEnum Enum => SeventhChordEnum.MinorSeventh; }
    [Serializable] public readonly struct DominantSeventh : ISeventhChord { public readonly SeventhChordEnum Enum => SeventhChordEnum.DominantSeventh; }
    [Serializable] public readonly struct DominantSeventhSus : ISeventhChord { public readonly SeventhChordEnum Enum => SeventhChordEnum.DominantSeventhSus; }
    [Serializable] public readonly struct MinorMajorSeventh : ISeventhChord { public readonly SeventhChordEnum Enum => SeventhChordEnum.MinorMajorSeventh; }
    [Serializable] public readonly struct HalfDiminishedSeventh : ISeventhChord { public readonly SeventhChordEnum Enum => SeventhChordEnum.HalfDiminishedSeventh; }
    [Serializable] public readonly struct DiminishedSeventh : ISeventhChord { public readonly SeventhChordEnum Enum => SeventhChordEnum.DiminishedSeventh; }

    public class SeventhChordEnum : Enumeration
    {
        public SeventhChordEnum() : base(0, "") { }
        public SeventhChordEnum(int id, string name, string desc) : base(id, name) { Description = desc; }
        public readonly string Description;

        public static SeventhChordEnum MajorSeventh = new(0, "∆7", nameof(MajorSeventh)) { };
        public static SeventhChordEnum MinorSeventh = new(1, "-7", nameof(MinorSeventh)) { };
        public static SeventhChordEnum DominantSeventh = new(2, "7", nameof(DominantSeventh)) { };
        public static SeventhChordEnum DominantSeventhSus = new(3, "7(sus)", nameof(DominantSeventhSus)) { };
        public static SeventhChordEnum MinorMajorSeventh = new(4, "-∆7", nameof(MinorMajorSeventh)) { };
        public static SeventhChordEnum HalfDiminishedSeventh = new(5, "ø7", nameof(HalfDiminishedSeventh)) { };
        public static SeventhChordEnum DiminishedSeventh = new(6, "º7", nameof(DiminishedSeventh)) { };


    }
}
namespace MusicTheory.SeventhChords.Arithmetic
{
    public static class SeventhChordArithmetic
    {
        public static Intervals.IInterval[] ChordTonesAsIntervals(this ISeventhChord chord)
        {
            Intervals.IInterval[] temp = new Intervals.IInterval[3];

            temp[0] = chord switch
            {
                MajorSeventh or DominantSeventh => new Intervals.M3(),
                MinorSeventh or MinorMajorSeventh or DiminishedSeventh or HalfDiminishedSeventh => new Intervals.mi3(),
                DominantSeventhSus => new Intervals.P4(),
                _ => throw new System.ArgumentOutOfRangeException(chord.Description)
            };

            temp[1] = chord switch
            {
                MajorSeventh or DominantSeventh or MinorSeventh or MinorMajorSeventh or DominantSeventhSus => new Intervals.P5(),
                DiminishedSeventh or HalfDiminishedSeventh => new Intervals.d5(),
                _ => throw new System.ArgumentOutOfRangeException(chord.Description)
            };

            temp[2] = chord switch
            {
                MajorSeventh or MinorMajorSeventh => new Intervals.M7(),
                MinorSeventh or DominantSeventh or DominantSeventhSus or HalfDiminishedSeventh => new Intervals.mi7(),
                DiminishedSeventh => new Intervals.d7(),
                _ => throw new System.ArgumentOutOfRangeException(chord.Description)
            };
            return temp;
        }

        public static ISeventhChord GetSeventhChord(this SeventhChordEnum e) => e switch
        {
            _ when e == SeventhChordEnum.MajorSeventh => new MajorSeventh(),
            _ when e == SeventhChordEnum.MinorSeventh => new MinorSeventh(),
            _ when e == SeventhChordEnum.MinorMajorSeventh => new MinorMajorSeventh(),
            _ when e == SeventhChordEnum.DominantSeventh => new DominantSeventh(),
            _ when e == SeventhChordEnum.DominantSeventhSus => new DominantSeventhSus(),
            _ when e == SeventhChordEnum.HalfDiminishedSeventh => new HalfDiminishedSeventh(),
            _ when e == SeventhChordEnum.DiminishedSeventh => new DiminishedSeventh(),
            _ => throw new System.ArgumentOutOfRangeException(e.Id + " : " + e.ToString())
        };

    }
}
