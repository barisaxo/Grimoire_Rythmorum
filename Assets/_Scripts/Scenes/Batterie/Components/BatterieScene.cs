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
    public BatterieScene(Sea.NPCShip npcShip, PlayerShip playerShip, Action tick, Audio.Batterie_AudioSystem batterieAudio)
    {
        NPCShip = npcShip;
        PlayerShip = playerShip;

        int nmeHealth = (int)(NPCShip.ShipStats.HullStrength * UnityEngine.Random.Range(0.8f, 1.1f));
        NMEHealth = (nmeHealth, nmeHealth);

        Pack = new();
        Tick = tick;
        BatterieAudio = batterieAudio;
    }

    public void Initialize()
    {
        BatterieHUD ??= new BatterieHUD(
            Sea.WorldMapScene.Io.Ship.ShipStats.HullStrength,
            DataManager.Io.CharData.GetLevel(Data.Player.CharacterData.DataItem.CurrentHP),
            (int)NPCShip.ShipStats.HullStrength);
        Debug.Log("(int)NPCShip.ShipStats.HullStrength: " + NPCShip.ShipStats.HullStats.HullData.Description);
        _ = Background;
        _ = Ship;
        BatterieFeedback = new();
        Pack.Initialize(HandleHit, BatterieFeedback, Tick);
        CountOffFeedBack = new(Pack.MusicSheet.RhythmSpecs.Time.GetCounts());
        BatterieFeedback.UpdateLoop();
        CountOffFeedBack.UpdateLoop();

        // Cam.Io.Camera.transform.SetPositionAndRotation(
        //     new UnityEngine.Vector3(Cam.Io.Camera.transform.position.x, 15, Cam.Io.Camera.transform.position.z),
        //     Quaternion.identity);
    }

    public void SelfDestruct()
    {
        Background.SelfDestruct();
        BatterieHUD.SelfDestruct();
    }


    public BatterieHUD BatterieHUD;
    public Action Tick;
    readonly Audio.Batterie_AudioSystem BatterieAudio;
    public BatteriePack Pack;
    public Sea.NPCShip NPCShip;
    public PlayerShip PlayerShip;
    public (int cur, int max) NMEHealth;
    public int NMEDamage;
    public int Cap;
    public int PlayerDamage;
    public int VolleysFired;
    public bool Escaping;

    public BatterieFeedback BatterieFeedback;
    public CountOffFeedback CountOffFeedBack;

    public GameObject NME;
    public ParticleSystem NMEFire;

    private GameObject _ship;
    public GameObject Ship => _ship != null ? _ship : _ship = SetUpShip();
    public ParticleSystem ShipFire;

    private GameObject SetUpShip()
    {
        var ship = Sea.WorldMapScene.Io.Ship.GO;
        NME = Sea.WorldMapScene.Io.NearestNPC.SceneObject.GO;

        ship.transform.position = Cam.Io.Camera.transform.position + ((Cam.Io.Camera.transform.forward * 4) - (Cam.Io.Camera.transform.right * 2) - (Cam.Io.Camera.transform.up * 2));
        NME.transform.position = Cam.Io.Camera.transform.position + ((Cam.Io.Camera.transform.forward * 4) + (Cam.Io.Camera.transform.right * 2) - (Cam.Io.Camera.transform.up * 2));

        ship.transform.LookAt(Cam.Io.Camera.transform);
        NME.transform.LookAt(Cam.Io.Camera.transform);

        NMEFire = Assets.CannonFire;
        NMEFire.transform.position = NME.transform.position;
        NMEFire.transform.LookAt(ship.transform);
        NMEFire.transform.Translate(Vector3.forward * .25f);

        ShipFire = Assets.CannonFire;
        ShipFire.transform.position = ship.transform.position;
        ShipFire.transform.LookAt(NME.transform);
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
                BatterieHUD.NMECurrent = NMEHealth.cur -= DataManager.Io.ShipData.ShipStats.HitDamage;
                UnityEngine.Debug.Log("Damage: " + DataManager.Io.ShipData.ShipStats.HitDamage);
                break;
            case Hit.Miss:
                // score--;
                Pack.MissedHits++;
                BatterieAudio.Miss();
                break;
            case Hit.BadHit:
                // score--;
                Pack.ErroneousAttacks++;
                Audio.AudioManager.Io.Batterie.MissStick();
                NMEFire.Play();
                break;
            case Hit.Break:
                break;
        }
    }

}
