using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { Left = -1, Right = 1 }
public class PlayerController : MonoBehaviour {
    public static PlayerController Instance { get; private set; }

    [SerializeField] private float speed = 1f;
    BoxCollider2D hitbox;

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
        StartCoroutine("Move", direction);
    }

    public void StopPlayer()
    {
        StopCoroutine("Move");
    }
    
    public void Summon<T>()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    private IEnumerator Move(Direction direction)
    {
        PlayerCamera.Instance.MoveCamera(direction);
        while (true)
        {
            this.transform.Translate(Vector2.Lerp(Vector2.zero, new Vector2((float)direction * speed, 0), 0.1f));
            yield return null;
        }
    }
}