using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player
{
    public Player(Transform parent)
    {
        Parent = parent;
        _ = GO;
    }
    private readonly Transform Parent;
    private SphereCollider sphereCollider;

    private GameObject _go;
    public GameObject GO => _go ? _go : _go = SetUpPlayer();

    private Rigidbody rb;
    public Rigidbody RB => rb;

    private Card _bark;
    public Card Bark => _bark ??= new Card(nameof(Bark), GO.transform)
        .SetFontScale(1.3f, 1.3f)
        .SetTextString("...")
        .SetOutlineColor(Color.black)
        .SetOutlineWidth(.15f)
        .AutoSizeFont(true)
        .AllowWordWrap(false)
        .SetTMPPosition(1, 1);


    private GameObject SetUpPlayer()
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        go.name = nameof(Player);
        sphereCollider = go.GetComponent<SphereCollider>();
        go.transform.SetParent(Parent);
        int hBoard = (int)(33 * .5f);
        go.transform.position = new Vector3(hBoard, 1, hBoard);
        go.transform.localScale = Vector3.one * .8f;

        MeshRenderer mr = go.GetComponent<MeshRenderer>();
        //mr.material.SetFloat("_Glossiness", 0);
        //mr.material = Assets.PlayerMat;

        rb = go.AddComponent<Rigidbody>();
        RB.useGravity = false;
        RB.angularDrag = 100;
        RB.drag = 100;
        RB.isKinematic = false;
        RB.freezeRotation = true;
        //RB.interpolation = RigidbodyInterpolation.Interpolate;
        RB.constraints =
            RigidbodyConstraints.FreezePositionY |
            RigidbodyConstraints.FreezeRotationX |
            RigidbodyConstraints.FreezeRotationY |
            RigidbodyConstraints.FreezeRotationZ;

        return go;
    }

}
