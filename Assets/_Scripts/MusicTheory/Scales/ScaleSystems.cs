
using MusicTheory.Scales;
using MusicTheory.Notes.Arithmetic;
using MusicTheory.Intervals.Arithmetic;
using MusicTheory.ScaleDegrees.Arithmetic;

namespace MusicTheory.Scales.Arithmetic
{
    public static class ScaleSystems
    {
        //public static Steps.Step[] ShiftSteps(this Scales.Scale scale, Modes.ModeDegreeEnum mode)
        //{
        //    Steps.Step[] newSteps = new Steps.Step[scale.Steps.Length];

        //    for (int i = (int)mode; i < (int)mode + newSteps.Length; i++)
        //    {
        //        newSteps[i] = scale.Steps[i < newSteps.Length ? i : i - newSteps.Length];
        //    }

        //    return newSteps;
        //}

        public static int GetIndex(this IScale scale, Modes.IMode mode)
        {
            for (int i = 0; i < scale.Modes.Length; i++)
                if (mode.Equals(scale.Modes[i])) { return i; }
            return 0;
        }

        public static int GetIndex(this IScale scale, ScaleDegrees.IScaleDegree degree)
        {
            for (int i = 0; i < scale.ScaleDegrees.Length; i++)
                if (degree.Equals(scale.ScaleDegrees[i])) { return i; }
            return 0;
        }

        public static Notes.INote Root(this IScale scale, ScaleDegrees.IScaleDegree currentScaleDegree, Notes.INote keyOf)
        {
            return keyOf.GetNoteAbove(currentScaleDegree.AsInterval());
        }

        public static Notes.INote Third(this IScale scale, ScaleDegrees.IScaleDegree currentScaleDegree, Notes.INote keyOf)
        {
            int x = 0;

            for (int i = 0; i < scale.ScaleDegrees.Length; i++)
                if (currentScaleDegree.Equals(scale.ScaleDegrees[i])) { x = i; break; }

            return keyOf.GetNoteAbove(currentScaleDegree.AsInterval())
                .GetNoteAbove(currentScaleDegree.GetInterval(scale.ScaleDegrees[(x + 2) % scale.ScaleDegrees.Length]));
        }

        public static Notes.INote Fifth(this IScale scale, ScaleDegrees.IScaleDegree currentScaleDegree, Notes.INote keyOf)
        {
            int x = 0;

            for (int i = 0; i < scale.ScaleDegrees.Length; i++)
                if (currentScaleDegree.Equals(scale.ScaleDegrees[i])) { x = i; break; }

            return keyOf.GetNoteAbove(currentScaleDegree.AsInterval())
                .GetNoteAbove(currentScaleDegree
                .GetInterval(scale.ScaleDegrees[(x + 4) % scale.ScaleDegrees.Length]));
        }

        public static Notes.INote Seventh(this IScale scale, ScaleDegrees.IScaleDegree currentScaleDegree, Notes.INote keyOf)
        {
            int x = 0;

            for (int i = 0; i < scale.ScaleDegrees.Length; i++)
                if (currentScaleDegree.Equals(scale.ScaleDegrees[i])) { x = i; break; }

            return keyOf.GetNoteAbove(currentScaleDegree.AsInterval())
                .GetNoteAbove(currentScaleDegree.GetInterval(scale.ScaleDegrees[(x + 6) % scale.ScaleDegrees.Length]));
        }

        public static Notes.INote Ninth(this IScale scale, ScaleDegrees.IScaleDegree currentScaleDegree, Notes.INote keyOf)
        {
            int x = 0;

            for (int i = 0; i < scale.ScaleDegrees.Length; i++)
                if (currentScaleDegree.Equals(scale.ScaleDegrees[i])) { x = i; break; }

            return keyOf.GetNoteAbove(currentScaleDegree.AsInterval())
                .GetNoteAbove(currentScaleDegree.GetInterval(scale.ScaleDegrees[(x + 1) % scale.ScaleDegrees.Length]));
        }

        public static Notes.INote Eleventh(this IScale scale, ScaleDegrees.IScaleDegree currentScaleDegree, Notes.INote keyOf)
        {
            int x = 0;

            for (int i = 0; i < scale.ScaleDegrees.Length; i++)
                if (currentScaleDegree.Equals(scale.ScaleDegrees[i])) { x = i; break; }

            return keyOf.GetNoteAbove(currentScaleDegree.AsInterval())
                .GetNoteAbove(currentScaleDegree
                .GetInterval(scale.ScaleDegrees[(x + 3) % scale.ScaleDegrees.Length]));
        }

        public static Notes.INote Thirteenth(this IScale scale, ScaleDegrees.IScaleDegree currentScaleDegree, Notes.INote keyOf)
        {
            int x = 0;

            for (int i = 0; i < scale.ScaleDegrees.Length; i++)
                if (currentScaleDegree.Equals(scale.ScaleDegrees[i])) { x = i; break; }

            return keyOf.GetNoteAbove(currentScaleDegree.AsInterval())
                .GetNoteAbove(currentScaleDegree
                .GetInterval(scale.ScaleDegrees[(x + 5) % scale.ScaleDegrees.Length]));
        }
    }

}