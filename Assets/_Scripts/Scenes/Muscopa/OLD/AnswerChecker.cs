using UnityEngine;
using MusicTheory.Chords;

namespace OLDMuscopa
{
    static class AnswerChecker
    {

        public static void UpdateAnswerTexts(this MuscopaScene scene)
        {
            scene.CurrentLevel.SetTextString(
            //TODO
            //     Puzzle_Data.CurrentLevel switch
            // {
            //     Level.c251 => "I, II-, V",
            //     Level.c145 => "I, IV, V",
            //     Level.c1456 => "I, IV, V, VI-",
            //     Level.c3625 => "II-, III-, V, VI-",
            //     Level.cAll => "I, II-, III-, IV, V, VI-, VIIø",
            //     _ => "??",
            // }
            ""
            );

            // Muscopa.Answer1ChordName_Text.text = Muscopa.PuzzleChord[0].ToString();
            // Muscopa.Answer2ChordName_Text.text = Muscopa.AnsweredPiles.Contains(CardPile.Two) ?
            //     Muscopa.PuzzleChord[1].ToString() : "?";
            // Muscopa.Answer3ChordName_Text.text = Muscopa.AnsweredPiles.Contains(CardPile.Three) ?
            //      Muscopa.PuzzleChord[2].ToString() : "?";
            // Muscopa.Answer4ChordName_Text.text = Muscopa.AnsweredPiles.Contains(CardPile.Four) ?
            //     Muscopa.PuzzleChord[3].ToString() : "?";
        }

        public static bool CheckAnswer(this MuscopaScene scene, Card card) => scene.PuzzleChords[(int)scene.CurrentAnswerPile] switch
        {
            // Chord.I when card.Function == Function.Tonic => true,
            // Chord.II when card.Function == Function.Recessive => true,
            // Chord.III when card.Function == Function.Tonic => true,
            // Chord.IV when card.Function == Function.Recessive => true,
            // Chord.V when card.Function == Function.Dominant => true,
            // Chord.VI when card.Function == Function.Tonic => true,
            // Chord.VII when card.Function == Function.Dominant => true,
            _ => false,
        };

        public static bool PuzzleIsSolved(this MuscopaScene scene) =>
             scene.AnsweredPiles.Contains(CardPile.Two) &&
             scene.AnsweredPiles.Contains(CardPile.Three) &&
             scene.AnsweredPiles.Contains(CardPile.Four);
    }
}