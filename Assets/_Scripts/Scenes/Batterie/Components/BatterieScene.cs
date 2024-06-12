using System.Collections;
using System;
using UnityEngine;
using SheetMusic;
using Batterie;
using MusicTheory.Rhythms;
using MusicTheory;
using Muscopa;

public class BatterieScene
{
    public BatterieScene(ShipStats.ShipStats nmeShipStats, GameObject nmeGO, PlayerShip playerShip, Action tick, RhythmSpecs specs, string nmeName)
    {
        NMEGO = nmeGO;
        NMEShipStats = nmeShipStats;
        PlayerShip = playerShip;

        NMEName = nmeName;
        int nmeHealth = (int)(NMEShipStats.HullStrength * UnityEngine.Random.Range(0.8f, 1.1f));
        NMEHealth = (nmeHealth, nmeHealth);

        Pack = new(specs);
        Tick = tick;
        BatterieAudio = Audio.AudioManager.Io.Batterie;
    }

    public BatterieScene(ShipStats.ShipStats nmeShipStats, GameObject nmeGO, PlayerShip playerShip, Action tick, BatteriePack pack, string nmeName)
    {
        NMEGO = nmeGO;
        NMEName = nmeName;
        NMEShipStats = nmeShipStats;
        PlayerShip = playerShip;

        int nmeHealth = (int)(NMEShipStats.HullStrength * UnityEngine.Random.Range(0.8f, 1.1f));
        NMEHealth = (nmeHealth, nmeHealth);

        Pack = pack;
        Tick = tick;
        BatterieAudio = Audio.AudioManager.Io.Batterie;
    }
    // public BatterieScene(Sea.NPCShip npcShip, PlayerShip playerShip, BatteriePack pack)
    // {
    //     NPCShip = npcShip;
    //     PlayerShip = playerShip;

    //     int nmeHealth = (int)(NPCShip.ShipStats.HullStrength * UnityEngine.Random.Range(0.8f, 1.1f));
    //     NMEHealth = (nmeHealth, nmeHealth);

    //     Pack = pack;
    //     // Tick = tick;
    //     BatterieAudio = Audio.AudioManager.Io.Batterie;
    // }
    public void Initialize()
    {
        Debug.Log(Data.Manager.Io.ActiveShip.GetLevel(new Data.MaxHitPoints()));

        // Sea.WorldMapScene.Io.Ship.ShipStats.HullStrength,
        BatterieHUD ??= new BatterieHUD(
            Data.Manager.Io.ActiveShip.GetLevel(new Data.MaxHitPoints()),
            Data.Manager.Io.ActiveShip.GetLevel(new Data.CurrentHitPoints()),
            (int)NMEShipStats.HullStrength,
            NMEName);

        Debug.Log("(int)NPCShip.ShipStats.HullStrength: " + NMEShipStats.HullStats.Hull.Description);
        _ = Background;
        _ = Ship;
        BatterieFeedback = new();
        Pack.Initialize(HandleHit, BatterieFeedback, Tick);
        CountOffFeedBack = new(Pack.MusicSheet.RhythmSpecs.Time.GetCounts());
        BatterieFeedback.UpdateLoop();
        CountOffFeedBack.UpdateLoop();

        DamageDealt = Data.Manager.Io.ActiveShip.ShipStats.VolleyDamage;
        // Cam.Io.Camera.transform.SetPositionAndRotation(
        //     new UnityEngine.Vector3(Cam.Io.Camera.transform.position.x, 15, Cam.Io.Camera.transform.position.z),
        //     Quaternion.identity);
    }

    public void SelfDestruct()
    {
        GameObject.Destroy(ShipFire.gameObject);
        GameObject.Destroy(NMEFire.gameObject);
        Background.SelfDestruct();
        BatterieHUD.SelfDestruct();
    }


    public BatterieHUD BatterieHUD;
    public Action Tick;
    readonly Audio.Batterie_AudioSystem BatterieAudio;
    public BatteriePack Pack;
    // public Sea.NPCShip NPCShip;
    public readonly string NMEName;
    public ShipStats.ShipStats NMEShipStats;
    public PlayerShip PlayerShip;
    public (int cur, int max) NMEHealth;
    public int DamageDealt;
    public int Cap;
    public int PlayerDamage;
    public int VolleysFired;
    public bool Escaping;

    public BatterieFeedback BatterieFeedback;
    public CountOffFeedback CountOffFeedBack;

    public GameObject NMEGO;
    public ParticleSystem NMEFire;

    private GameObject _ship;
    public GameObject Ship => _ship != null ? _ship : _ship = SetUpShip();
    public ParticleSystem ShipFire;

    private GameObject SetUpShip()
    {
        var ship = Sea.WorldMapScene.Io.Ship.GO;
        // NMEGO = Sea.WorldMapScene.Io.NearestNPC.SceneObject.GO;

        ship.transform.position = Cam.Io.Camera.transform.position + ((Cam.Io.Camera.transform.forward * 4) - (Cam.Io.Camera.transform.right * 2) - (Cam.Io.Camera.transform.up * 2));
        NMEGO.transform.position = Cam.Io.Camera.transform.position + ((Cam.Io.Camera.transform.forward * 4) + (Cam.Io.Camera.transform.right * 2) - (Cam.Io.Camera.transform.up * 2));

        // ship.transform.rotation = Cam.Io.Camera.transform.rotation;
        // NME.transform.rotation = Cam.Io.Camera.transform.rotation;

        // ship.transform.LookAt(Cam.Io.Camera.transform);
        // NME.transform.LookAt(Cam.Io.Camera.transform);


        NMEFire = Assets.CannonFire;
        NMEFire.transform.position = NMEGO.transform.position;
        NMEFire.transform.LookAt(ship.transform);
        NMEFire.transform.Translate(Vector3.forward * .25f);

        ShipFire = Assets.CannonFire;
        ShipFire.transform.position = ship.transform.position;
        ShipFire.transform.LookAt(NMEGO.transform);
        ShipFire.transform.Translate(Vector3.forward * .25f);

        return ship;
    }

    Card _background;
    Card Background => _background ??= new Card(nameof(Background), null)
        .SetImagePosition(Vector2.zero)
        .SetImageSize(new Vector2(Cam.Io.UICamera.scaledPixelWidth * .8f, Cam.Io.UICamera.scaledPixelHeight * .8f))
        .SetCanvasSortingOrder(0)
        .SetImageColor(new Color(0, 0, 0, .25f));



    private void HandleHit(Batterie.Hit hit)
    {
        Cap++;
        switch (hit)
        {
            case Hit.Hit:
                // score++;
                Pack.GoodHits++;
                BatterieAudio.Hit();
                ShipFire.Play();
                // BatterieHUD.NMECurrent = NMEHealth.cur -= Data.Manager.Io.ActiveShip.ShipStats.HitDamage;
                // UnityEngine.Debug.Log("Damage: " + Data.Manager.Io.ActiveShip.ShipStats.HitDamage);
                break;
            case Hit.Miss:
                // score--;
                DamageDealt -= Data.Manager.Io.ActiveShip.ShipStats.HitDamage;
                Pack.MissedHits++;
                BatterieAudio.Miss();
                break;
            case Hit.BadHit:
                // score--;
                DamageDealt -= Data.Manager.Io.ActiveShip.ShipStats.HitDamage;
                Pack.ErroneousAttacks++;
                Audio.AudioManager.Io.Batterie.MissStick();
                // NMEFire.Play();
                break;
            case Hit.Break:
                break;
        }
    }

}
