using System;
using System.Collections;
using SheetMusic;
using MusicTheory.Rhythms;
using Batterie;
using UnityEngine;

public class NewBatterie_State : State
{
    // private NewBatterie_State(BatteriePack pack)
    // {
    //     Pack = pack;
    // }
    private NewBatterie_State() { }

    BatteriePack Pack;
    RhythmSpecs specs;

    GameObject Ship;
    GameObject NME;

    protected override void PrepareState(Action callback)
    {
        specs = new MusicTheory.Rhythms.RhythmSpecs()
        {
            Time = new MusicTheory.Rhythms.FourFour(),
            NumberOfMeasures = 4,
            SubDivisionTier = MusicTheory.Rhythms.SubDivisionTier.D1Only,
            HasTies = UnityEngine.Random.value > .5f,
            HasRests = UnityEngine.Random.value > .5f,
            HasTriplets = false,
            Tempo = 90
        };

        Ship = Assets.Sloop.gameObject;
        NME = Sea.WorldMapScene.Io.NearestNPC.SceneObject.Instantiator.ToInstantiate;

        Ship.transform.SetParent(Cam.Io.Camera.transform);
        NME.transform.SetParent(Cam.Io.Camera.transform);

        Cam.Io.Camera.transform.SetPositionAndRotation(new UnityEngine.Vector3(Cam.Io.Camera.transform.position.x, 15, Cam.Io.Camera.transform.position.z), Quaternion.identity);
        Ship.transform.LookAt(Cam.Io.Camera.transform);
        NME.transform.LookAt(Cam.Io.Camera.transform);

        // SetStateDirectly(new BatteryAndCadenceTestState(specs, Pack));


        base.PrepareState(callback);
    }

    protected override void EngageState()
    {
        // SetStateDirectly(new BatteryAndCadenceTestState(specs, Pack));
    }

    // IEnumerator PanCamera()
    // {
    //     Pan = new Vector3(Pan.x.SignedMod(360), Pan.y.SignedMod(360), Pan.z.SignedMod(360));

    //     // while (!Cam.Io.Camera.transform.rotation.eulerAngles.x.IsPOM(1, Pan.x) ||
    //     //        !Cam.Io.Camera.transform.rotation.eulerAngles.y.IsPOM(1, Pan.y) ||
    //     //        !Cam.Io.Camera.transform.rotation.eulerAngles.z.IsPOM(1, Pan.z) ||
    //     //        !Cam.Io.Camera.transform.position.x.IsPOM(1, Strafe.x) ||
    //     //        !Cam.Io.Camera.transform.position.y.IsPOM(1, Strafe.y) ||
    //     //        !Cam.Io.Camera.transform.position.z.IsPOM(1, Strafe.z))
    //     // {
    //     //     Cam.Io.Camera.transform.rotation = Quaternion.Slerp(Cam.Io.Camera.transform.rotation, Quaternion.Euler(Pan), UnityEngine.Time.deltaTime * 2.3f);
    //     //     Cam.Io.Camera.transform.position = Vector3.Lerp(Cam.Io.Camera.transform.position, Strafe, UnityEngine.Time.deltaTime * 2.3f);
    //     //     yield return null;
    //     // }

    //     // Cam.Io.Camera.transform.position = Strafe;
    //     // Cam.Io.Camera.transform.rotation = Quaternion.Euler(Pan);

    //     // ShipPos = Cam.Io.Camera.transform.position + ((Cam.Io.Camera.transform.forward * 4) - (Cam.Io.Camera.transform.right * 2) - (Cam.Io.Camera.transform.up));
    //     // NMEPos = Cam.Io.Camera.transform.position + ((Cam.Io.Camera.transform.forward * 4) + (Cam.Io.Camera.transform.right * 2) - (Cam.Io.Camera.transform.up));

    //     // while (
    //     //        !Ship.transform.position.x.IsPOM(1, ShipPos.x) ||
    //     //        !Ship.transform.position.y.IsPOM(1, ShipPos.y) ||
    //     //        !Ship.transform.position.z.IsPOM(1, ShipPos.z) ||
    //     //        !NME.transform.position.x.IsPOM(1, NMEPos.x) ||
    //     //        !NME.transform.position.y.IsPOM(1, NMEPos.y) ||
    //     //        !NME.transform.position.z.IsPOM(1, NMEPos.z))
    //     // {
    //     //     Ship.transform.position = Vector3.Lerp(Ship.transform.position, ShipPos, UnityEngine.Time.deltaTime * 2.3f);
    //     //     NME.transform.position = Vector3.Lerp(NME.transform.position, NMEPos, UnityEngine.Time.deltaTime * 2.3f);
    //     //     yield return null;
    //     // }

    // }


}
