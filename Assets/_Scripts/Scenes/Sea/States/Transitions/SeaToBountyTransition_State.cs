using System;
using MusicTheory.Rhythms;

public class SeaToBountyTransition_State : State
{
    public SeaToBountyTransition_State(
        ShipStats.ShipStats nmeShipStats,
        UnityEngine.GameObject nmeGO,
        Sea.Region region,
        Sea.Cell cell)
    {
        NMEShipStats = nmeShipStats;
        Fade = false;
        NMEGO = nmeGO;
        Cell = cell;
        Region = region;
        Quest = DataManager.Quests.GetQuest(new Data.Two.Bounty()) as Quests.BountyQuest;
    }

    readonly Quests.BountyQuest Quest;
    readonly Sea.Region Region;
    readonly Sea.Cell Cell;
    readonly UnityEngine.GameObject NMEGO;
    readonly ShipStats.ShipStats NMEShipStats;

    protected override void PrepareState(Action callback)
    {
        Audio.Ambience.Pause();
        Audio.BGMusic.Pause();
        Audio.SFX.PlayOneShot(Assets.AlertHalfDim);
        Sea.WorldMapScene.Io.HUD.Disable();
        Sea.WorldMapScene.Io.Ship.ConfirmPopup.GO.SetActive(false);
        Sea.WorldMapScene.Io.Ship.AttackPopup.GO.SetActive(false);
        Sea.WorldMapScene.Io.MiniMap.Card.GO.SetActive(false);
        Sea.WorldMapScene.Io.Ship.SeaPos = Sea.WorldMapScene.Io.Ship.GO.transform.position;
        Sea.WorldMapScene.Io.Ship.SeaRot = Sea.WorldMapScene.Io.Ship.GO.transform.rotation;
        Sea.WorldMapScene.Io.Board.Swells.DisableSwells();
        base.PrepareState(callback);
    }

    protected override void EngageState()
    {
        Region.Cells.Remove(Cell);
        SetState(new BountyBatterie_State(NMEShipStats, NMEGO, Sea.WorldMapScene.Io.Ship, Quest));
    }
}