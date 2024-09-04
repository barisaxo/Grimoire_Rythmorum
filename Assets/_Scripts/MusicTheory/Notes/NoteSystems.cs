using System;
using MusicTheory.Scales.Arithmetic;
using MusicTheory.Intervals.Arithmetic;
using MusicTheory.Intervals;
using MusicTheory.RomanNumerals.Chromatic.Arithmetic;
using MusicTheory.RomanNumerals.Diatonic;

namespace MusicTheory.Notes.Arithmetic
{
    public static class NoteArithmetic
    {
        public static INote[] GetNotes(this INote note, Scales.IScale scale, Modes.IMode mode)
        {
            INote[] notes = new INote[scale.ScaleDegrees.Length];
            notes[0] = note;

            for (int i = 1; i < notes.Length; i++)
                notes[i] = notes[i - 1].GetNoteAbove(scale.Steps[(i - 1 + scale.GetIndex(mode)) % notes.Length].AsInterval());

            return notes;
        }

        public static Triads.ITriad GetTriadQuality(this INote root, INote third, INote fifth)
        {
            return (root.GetInterval(third), root.GetInterval(fifth)) switch
            {
                (Intervals.mi3, Intervals.P5) => new Triads.Minor(),
                (Intervals.A2, Intervals.P5) => new Triads.Minor(),

                (Intervals.M3, Intervals.P5) => new Triads.Major(),
                (Intervals.d4, Intervals.P5) => new Triads.Major(),

                (Intervals.M3, Intervals.A5) => new Triads.Augmented(),
                (Intervals.M3, Intervals.mi6) => new Triads.Augmented(),
                (Intervals.d4, Intervals.mi6) => new Triads.Augmented(),
                (Intervals.d4, Intervals.A5) => new Triads.Augmented(),

                (Intervals.mi3, Intervals.d5) => new Triads.Diminished(),
                (Intervals.mi3, Intervals.A4) => new Triads.Diminished(),
                (Intervals.A2, Intervals.d5) => new Triads.Diminished(),
                (Intervals.A2, Intervals.A4) => new Triads.Diminished(),

                //(Intervals.M2, Intervals.M3) => new Triads.Secundal(),//d3 + d3
                //(Intervals.M2, Intervals.d4) => new Triads.Secundal(),//d3 + d3
                //(Intervals.M2, Intervals.P4) => new Triads.Secundal(),//d3 + mi3

                //(Intervals.mi3, Intervals.P4) => new Triads.Quartal(),//mi3 + d3
                //(Intervals.M3, Intervals.M6) => new Triads.Quartal(),//M3 + A3
                //(Intervals.M3, Intervals.d7) => new Triads.Quartal(),//M3 + A3
                //(Intervals.d4, Intervals.M6) => new Triads.Quartal(),//M3 + M3
                //(Intervals.d4, Intervals.d7) => new Triads.Quartal(),//M3 + M3
                //(Intervals.P4, Intervals.d7) => new Triads.Quartal(),//A3 + M3
                //(Intervals.P4, Intervals.M6) => new Triads.Quartal(),//A3 + M3
                //(Intervals.P4, Intervals.mi7) => new Triads.Quartal(),//A3 + A3

                _ => throw new ArgumentOutOfRangeException(root.Name + ", " + root.GetInterval(third) + ", " + third.Name + ", " + root.GetInterval(fifth) + ", " + fifth.Name)
            };
        }

        public static INote KeepFlatOrNatural(this INote note)
        {
            if (note.Enum.Accidental is Notes.Sharp)
                foreach (var noteEnum in Enumeration.All<NoteEnum>())
                    if (noteEnum.Letter.Id.Equals((note.Enum.Letter.Id + 1) % 12) && note.Id == noteEnum.Id)
                        return noteEnum.GetNote();
            return note;
        }

        public static INote GetNoteAbove(this INote note, Intervals.IInterval interval)
        {
            if (interval is Intervals.P1 or Intervals.P8) return note;

            int id = (note.Id + interval.Id).Smod(NoteEnum.Count);

            ILetter letter = note.GetLetterAbove(interval);
            INote newNote = Enumeration.FindId<NoteEnum>(id).GetNote();

            foreach (var e in Enumeration.All<NoteEnum>())
            {
                if (e.Id.Equals(id) &&
                // e.Letter.Equals(letter)
                MathF.Abs(e.Letter.Id - letter.Id) <=
                MathF.Abs(newNote.Enum.Letter.Id - letter.Id))
                {
                    UnityEngine.Debug.Log("e letter: " + e.Letter.Name + " letter: " + letter.Name);

                    newNote = e.GetNote();

                }
            }

            UnityEngine.Debug.Log(
                        "note id: " + note.Id +
                        ", interval quantity id: " + interval.Enum.Quantity.Id +
                        ", smoded: " + id +
                        ", letter: " + letter.Name +
                        ", new note: " + newNote.Name);

            return newNote;
        }


        public static INote GetNoteAbove(this INote note, RomanNumerals.Diatonic.IRomanNumeral rn)
        {
            if (rn is RomanNumerals.Diatonic.I) return note;

            int id = (note.Id + rn.Id).Smod(NoteEnum.Count);

            IInterval interval = note.GetInterval(rn);

            // note.GetNoteAbove(interval);
            // foreach (var e in Enumeration.All<NoteEnum>())
            // {
            //     if (e.Id.Equals(id) &&
            //     // e.Letter.Equals(letter)
            //     MathF.Abs(e.Letter.Id - interval.Id) <=
            //     MathF.Abs(newNote.Enum.Letter.Id - interval.Id))
            //     {
            //         UnityEngine.Debug.Log("e letter: " + e.Letter.Name + " letter: " + interval.Name);

            //         newNote = e.GetNote();
            //     }
            // }

            return note.GetNoteAbove(interval); ;
        }



        public static Intervals.IInterval GetInterval(this INote left, INote right)
        {
            Intervals.IInterval newInterval = new Intervals.P8();
            Intervals.IQuantity quantity = left.GetQuantity(right);

            int id = (right.Id + 12 - left.Id) % 12;

            foreach (var interval in Enumeration.All<Intervals.IntervalEnum>())
            {
                if (interval.Id.Equals(id) &&
                    MathF.Abs(interval.Quantity.Id - quantity.Id) <
                    MathF.Abs(newInterval.Quantity.Id - quantity.Id))
                    newInterval = interval.GetInterval();
            }

            return newInterval;
        }

        public static Intervals.IInterval GetInterval(this INote left, RomanNumerals.Diatonic.IRomanNumeral right)
        {
            return right switch
            {
                I => new P1(),
                II => new M2(),
                III => new M3(),
                IV => new P4(),
                V => new P5(),
                VI => new M6(),
                VII => new M7(),
                _ => throw new System.Exception(right.Name)
            };
        }

        public static INote GetRootNote(this RomanNumerals.Diatonic.IRomanNumeral diatonicChord, INote keyCenter) =>
            GetRootNote(diatonicChord.ToChromaticRoman(), keyCenter);

        public static INote GetRootNote(this RomanNumerals.Chromatic.IRomanNumeral romanNumeral, INote keyCenter)
        {
            return keyCenter.NotePlusInterval(romanNumeral.Id).GetEnharmonicInKeyCenter(keyCenter);
            //todo I think this is backwards

            // return romanNumeral switch
            // {
            //     RomanNumerals.Chromatic.I => KeyCenter,
            //     RomanNumerals.Chromatic.bII => KeyPlusInterval(1),
            //     RomanNumerals.Chromatic.II => KeyPlusInterval(2),
            //     RomanNumerals.Chromatic.bIII => KeyPlusInterval(3),
            //     RomanNumerals.Chromatic.III => KeyPlusInterval(4),
            //     RomanNumerals.Chromatic.IV => KeyPlusInterval(5),
            //     RomanNumerals.Chromatic.bV => KeyPlusInterval(6),
            //     RomanNumerals.Chromatic.V => KeyPlusInterval(7),
            //     RomanNumerals.Chromatic.bVI => KeyPlusInterval(8),
            //     RomanNumerals.Chromatic.VI => KeyPlusInterval(9),
            //     RomanNumerals.Chromatic.bVII => KeyPlusInterval(10),
            //     RomanNumerals.Chromatic.VII => KeyPlusInterval(11),
            //     _ => KeyCenter,
            // };

        }

        public static int DistFromInterval(this INote note, int interval) =>
            (note.Id + interval).Smod(NoteEnum.Count);


        public static INote NotePlusInterval(this INote note, int interval) =>
            (note.Id + interval) > NoteEnum.Count - 1 ?
                Enumeration.FindId<NoteEnum>(note.Id + interval - NoteEnum.Count).GetNote() :
                Enumeration.FindId<NoteEnum>(note.Id + interval).GetNote();


        public static INote FindEnharmonic(this INote rootKey, Intervals.IInterval interval)
        {
            ILetter letter = rootKey.GetNoteAbove(interval).Enum.Letter;
            foreach (var e in NoteEnum.All<NoteEnum>())
                if (e.Letter.Equals(letter) && e.Accidental.Equals(rootKey.Enum.Accidental))
                    return e.GetNote();

            throw new ArgumentOutOfRangeException(rootKey.Name + ", " + interval.ToString());
        }


        public static INote GetNote(this NoteEnum note) => note switch
        {
            _ when note == NoteEnum.C => new C(),
            _ when note == NoteEnum.Cs => new Cs(),
            _ when note == NoteEnum.Db => new Db(),
            _ when note == NoteEnum.D => new D(),
            _ when note == NoteEnum.Ds => new Ds(),
            _ when note == NoteEnum.Eb => new Eb(),
            _ when note == NoteEnum.E => new E(),
            _ when note == NoteEnum.Es => new Es(),
            _ when note == NoteEnum.Fb => new Fb(),
            _ when note == NoteEnum.F => new F(),
            _ when note == NoteEnum.Fs => new Fs(),
            _ when note == NoteEnum.Gb => new Gb(),
            _ when note == NoteEnum.G => new G(),
            _ when note == NoteEnum.Gs => new Gs(),
            _ when note == NoteEnum.Ab => new Ab(),
            _ when note == NoteEnum.A => new A(),
            _ when note == NoteEnum.As => new As(),
            _ when note == NoteEnum.Bb => new Bb(),
            _ when note == NoteEnum.B => new B(),
            _ when note == NoteEnum.Bs => new Bs(),
            _ when note == NoteEnum.Cb => new Cb(),
            _ => throw new ArgumentOutOfRangeException(note.Id.ToString())
        };

        public static INote GetEnharmonicInKeyCenter(this INote note, INote keyCenter)
        {
            return note switch
            {
                Fb => keyCenter switch
                {
                    Cb => note,
                    _ => new E()
                },
                Cb => keyCenter switch
                {
                    Cb or Gb => note,
                    _ => new B()
                },
                Es => keyCenter switch
                {
                    Cs or Fs => note,
                    _ => new F()
                },
                Bs => keyCenter switch
                {
                    Cs => note,
                    _ => new C()
                },
                _ => note
            };
        }

    }

}