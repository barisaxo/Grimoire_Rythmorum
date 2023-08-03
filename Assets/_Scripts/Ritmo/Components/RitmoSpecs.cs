using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ritmo
{

    public class RitmoSpecs
    {
        //public Bard2D.BattleHUD.BattleHUD BHUD;

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

        public BatteryResultType ResultType;
        public RitmoSpecs SetResultType(BatteryResultType type) { ResultType = type; return this; }
    }

    public enum BatteryResultType { Won, Lost, NMEscaped, NMESurrender, Surrender, Fled, Spam }



}