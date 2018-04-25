using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { Left = -1, Right = 1 }
public class PlayerController : MonoBehaviour {
    public static PlayerController Instance { get; private set; }

    [SerializeField] private float speed = 1f;

    private void Awake()
    {
        Instance = this;
    }
    private void OnDestroy()
    {
        Instance = null;
    }

    public void MovePlayer(Direction direction)
    {
        this.transform.Translate(Vector3.Lerp(Vector3.zero, new Vector3((float)direction * speed, 0, 0), 0.1f));
        PlayerCamera.Instance.MoveCamera(direction);
    }
    
    public void Summon<T>()
    {

    }
}