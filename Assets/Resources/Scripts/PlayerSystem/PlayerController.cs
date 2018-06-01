using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public static PlayerController Instance { get; private set; }

    [SerializeField] private float speed = 1f;
    SpriteRenderer sprite;

    private void Awake()
    {
        Instance = this;
        sprite = GetComponent<SpriteRenderer>();
    }
    private void OnDestroy()
    {
        Instance = null;
    }

    private void Update()
    {

    }

    public void MovePlayer(Direction direction)
    {
        StartCoroutine("Move", direction);
        switch (direction)
        {
            case Direction.Left: sprite.flipX = true; break;
            case Direction.Right: sprite.flipX = false; break;
        }
    }

    public void StopPlayer()
    {
        StopCoroutine("Move");
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