
namespace Batterie
{
    public class BatterieSpecs
    {
        public (int cur, int max) NMEHealth;
        public int VolleysFired;
        public bool Escaping;

        public int GoodHits;
        public int GoodHolds;
        public int GoodRests;
        public int ErroneousAttacks;
        public int MissedRests;
        public int MissedHits;
        public int MissedHolds;
        public int TotalErrors => ErroneousAttacks + MissedRests + MissedHits + MissedHolds;
        public bool Spammed => ErroneousAttacks > GoodHits + GoodHolds + GoodRests + MissedRests + MissedHits + MissedHolds;

        public BatterieResultType ResultType;
        public BatterieSpecs SetResultType(BatterieResultType type) { ResultType = type; return this; }
    }

    public enum BatterieResultType { Won, Lost, NMEscaped, NMESurrender, Surrender, Fled, Spam }
}