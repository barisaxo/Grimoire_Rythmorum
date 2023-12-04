using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AetherMovement
{
    public const float moveSpeed = 5f;
    public const float lookSpeed = 3f;
    public const float rotSpeed = 60f;

    public static void MovePlayer(this Player player, Vector2 dir)
    {
        Vector3 dir3D = new(dir.x, 0, dir.y);
        player.GO.transform.Translate(moveSpeed * Time.fixedDeltaTime * dir3D);
    }

    public static void MoveCamera(this Player player, float y)
    {
        Cam.Io.Camera.transform.LookAt(player.GO.transform);
        Cam.Io.Camera.transform.Translate(new Vector3(0, Time.fixedDeltaTime * lookSpeed * y), 0);
        Cam.Io.Camera.transform.localPosition =
            new Vector3(Cam.Io.Camera.transform.localPosition.x,
                        Mathf.Clamp(Cam.Io.Camera.transform.localPosition.y, 1, 8),
                        Cam.Io.Camera.transform.localPosition.z);
    }

    public static void RotatePlayer(this Player player, float x) =>
        player.GO.transform.Rotate(Time.fixedDeltaTime * x * rotSpeed * Vector3.up);

}