using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {
    public static PlayerCamera Instance { get; private set; }

    public GameObject OverL_Obj;
    public GameObject OverR_Obj;
    public Vector3 OverL;
    public Vector3 OverR;

    [SerializeField] private Vector3 OriginOffset = new Vector3(2, 0, -10);
    [SerializeField] private float fallowSpeed = 0.02f;

    private Vector3 offset;
    private Direction p_direction;

    public void Awake()
    {
        Instance = this;
        offset = OriginOffset;
        OverL = OverL_Obj.transform.position;
        OverR = OverR_Obj.transform.position;
    }
    private void OnDestroy()
    {
        Instance = null;
    }

    private void FixedUpdate()
    {
        if (OverL.x > Camera.main.ViewportToWorldPoint(Vector3.zero).x)
        {
            if (p_direction == Direction.Left) return;
        }
        else if (OverR.x < Camera.main.ViewportToWorldPoint(Vector3.right).x)
        {
            if (p_direction == Direction.Right) return;
        }

        this.transform.position = Vector3.Lerp(this.transform.position,
            new Vector3(PlayerController.Instance.transform.position.x,0,0) + offset, fallowSpeed);
    }

    public void MoveCamera(Direction direction)
    {
        p_direction = direction;
        switch (direction) {
            case Direction.Left: offset.x = OriginOffset.x * -1; break;
            case Direction.Right: offset.x = OriginOffset.x; break;
        }
    }
}
