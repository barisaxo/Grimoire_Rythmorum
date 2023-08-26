namespace MusicTheory.Rhythms
{
    public struct Measure
    {
        //public RhythmSpecs Specs;
        public RhythmCell[] Cells;

        //public Measure SetSpecs(RhythmSpecs specs) { Specs = specs; return this; }
        public Measure SetCells(RhythmCell[] cells) { Cells = cells; return this; }
    }
}
