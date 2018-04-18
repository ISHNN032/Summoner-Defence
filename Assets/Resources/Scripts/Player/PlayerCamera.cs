using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {
    public static PlayerCamera Instance { get; private set; }
    private Vector3 offset;

    public void Awake()
    {
        Instance = this;
        offset = this.transform.position;
    }
    private void OnDestroy()
    {
        Instance = null;
    }

    public void MoveCamera(Move direction)
    {
        switch (direction) {
            case Move.Left: offset.x = -4; break;
            case Move.Right: offset.x = 4; break;
        }
        this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(PlayerController.Instance.transform.position.x + offset.x, offset.y, offset.z), 0.02f);
    }
}
