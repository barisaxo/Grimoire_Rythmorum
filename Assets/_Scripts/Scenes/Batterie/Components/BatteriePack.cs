using UnityEngine;
public class BatteriePack
{
    // public Bard2D.BattleHUD.BattleHUD BHUD;
    public BatteriePack()
    {
        var health = Random.Range(16, 24);
        NMEHealth = (health, health);
    }
    public Sea.Scene SeaScene;
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

    public GameObject NME;
    public ParticleSystem NMEFire;
    public GameObject Ship;
    public ParticleSystem ShipFire;

    public BatterieResultType ResultType;
    public BatteriePack SetResultType(BatterieResultType type) { ResultType = type; return this; }
}

public enum BatterieResultType { Won, Lost, NMEscaped, NMESurrender, Surrender, Fled, Spam }