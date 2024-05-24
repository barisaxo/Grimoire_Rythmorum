using System;

[Serializable]
public class PuzzleSpec
{
    public readonly PuzzleType PuzzleType;
    public readonly Type GamutType;

    public PuzzleSpec(PuzzleType type, IPuzzle gamut)
    {
        PuzzleType = type;
        GamutType = gamut.GetType();
    }

    public override int GetHashCode() => System.HashCode.Combine(PuzzleType, GamutType);
    public override bool Equals(object obj)
    {
        PuzzleSpec spec = (PuzzleSpec)obj;
        bool tf = obj is PuzzleSpec e && e.PuzzleType == PuzzleType && e.GamutType == GamutType;
        //UnityEngine.Debug.Log(obj.GetType().ToString() + " " + spec.PuzzleType + " " + spec.GamutType + " Is Equal?? " + tf);
        return tf;
    }
}

[Serializable]
public class PuzzleStat
{
    public readonly PuzzleSpec Specs;
    public int ErroneousEntries;
    public int WrongAnswers;
    public int HintsUsed;
    public int Skipped;
    public int Solved;
    public int Failed;

    public PuzzleStat(PuzzleSpec specs, int hints, int wrong, int fail, int solve, int skipped, int errors)
    {
        Specs = specs; HintsUsed = hints; WrongAnswers = wrong; Failed = fail;
        Solved = solve; Skipped = skipped; ErroneousEntries = errors;
    }

    public void AddStats(PuzzleStat stat)
    {
        if (!stat.Specs.Equals(Specs)) throw new ArgumentOutOfRangeException();
        WrongAnswers += stat.WrongAnswers;
        HintsUsed += stat.HintsUsed;
        Failed += stat.Failed;
        Solved += stat.Solved;
        Skipped += stat.Skipped;
    }
}

[System.Serializable]
public enum PuzzleDifficulty { Free, Easy, Hard, Challenge }
[System.Serializable]
public enum PuzzleType { Aural, Theory }
[System.Serializable]
public enum AnswerData { Wrong, Skipped, Solved, Hinted }