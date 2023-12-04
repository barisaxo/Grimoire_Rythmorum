using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow
{
    public bool LockYPos;
    public bool LockYRot;
    public bool LockXPos;
    public bool LockXRot;
    public bool LockZPos;
    public bool LockZRot;
    public bool LockWRot;

    public Vector3 LocOffset;
    public Quaternion RotOffset;

    readonly Transform Subject;

    public CameraFollow(Transform subject)
    {
        Subject = subject;

        LocOffset = Subject.transform.position - Cam.Io.Camera.transform.position;

        RotOffset = new Quaternion(
            Subject.transform.rotation.x - Cam.Io.Camera.transform.rotation.x,
            Subject.transform.rotation.y - Cam.Io.Camera.transform.rotation.y,
            Subject.transform.rotation.z - Cam.Io.Camera.transform.rotation.z,
            Subject.transform.rotation.w - Cam.Io.Camera.transform.rotation.w);

        MonoHelper.OnUpdate += Follow;
    }

    public void SelfDestruct()
    {
        MonoHelper.OnUpdate -= Follow;
    }

    public void Pause()
    {
        MonoHelper.OnUpdate -= Follow;
    }

    public void Resume()
    {
        MonoHelper.OnUpdate += Follow;
    }

    void Follow()
    {
        var v3 = new Vector3(
              LockXPos ? Cam.Io.Camera.transform.position.x : Subject.position.x,
              LockYPos ? Cam.Io.Camera.transform.position.y : Subject.position.y,
              LockZPos ? Cam.Io.Camera.transform.position.z : Subject.position.z);
        Cam.Io.Camera.transform.position = v3 - (Subject.transform.forward * 2);
        Cam.Io.Camera.transform.LookAt(Subject.transform, Vector3.up);
        Cam.Io.Camera.transform.Rotate(new Vector3(-10, 0, 0));
    }


    public const float lookSpeed = 1f;
    public void MoveCamera(float y)
    {
        Cam.Io.Camera.transform.LookAt(Subject);
        Cam.Io.Camera.transform.Translate(new Vector3(0, Time.deltaTime * lookSpeed * y, 0), Subject);
        Cam.Io.Camera.transform.localPosition =
            new Vector3(Cam.Io.Camera.transform.position.x,
                        Mathf.Clamp(Cam.Io.Camera.transform.localPosition.y, .5f, 3f),
                        Cam.Io.Camera.transform.position.z);
        Cam.Io.Camera.transform.Rotate(new Vector3(-10, 0, 0));
    }



}