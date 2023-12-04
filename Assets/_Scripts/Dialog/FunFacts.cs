using System.Collections.Generic;
using UnityEngine;

public static class FunFacts
{
    private static List<string> _funFact;
    private static List<string> FunFact =>
        _funFact == null || _funFact.Count == 0 ? _funFact = GetFunFacts : _funFact;

    public static string GetFunFact(this FunFact _)
    {
        int r = Random.Range(0, FunFact.Count);
        string funFact = FunFact[r];
        FunFact.RemoveAt(r);
        return funFact;
    }

    public static List<string> GetFunFacts => new List<string>()
    {
        "Follow the tritone!",

        "How about a courtesy accidental, huh?",

        "Have you heard of Rhythm Cells? " +
        "Supposedly if one can master all 12 rhythm cells " +
        "they'll have an unbeatable Batterie, and dominate the 7 seas.",

        "Ties should only be used to connect the last note of one rhythm cell to the first note of the next.",

        "Never tie to a rest.",

        "Rests should only be used as the first note of a rhythm cell.",

        "Rests and ties mean that the beat is <i>syncopated.",

        "Subdivisions don't change any <i>rhythmic shape</i>, but the way we count that rhythm. That's why there are only 12 rhythm cells.",

        "The old Bards were really smart to have mastered this now forgotten Musica so long ago.",

        "Listen to the bass line!",

        "There's an old saying called 'See Count Three' that really helps when reading rhythms.",

        "Any <i>diatonic key</i> contains all 7 notes (ABCDEFG) exactly once, and never mixes sharps with flats.",

        "Bards used circles to visualize <i>key centers</i> and make great patterns with Musica.",

        "The word Dominant means 'the most important.'\nDominant chords are the most important because they contain a Tritone. That alone is enough information identify what key the music is in.",

        "There's an order to the sharps(#) FCGDAEB and flats(b) BEADGCF.\nThey are actually the same, but reverse of each other.",

        "''Dude, suckin at somethin, is first step towards being kinda good at somethin.'' - A very smart, magic dog. "
    };
}

public enum FunFact { }