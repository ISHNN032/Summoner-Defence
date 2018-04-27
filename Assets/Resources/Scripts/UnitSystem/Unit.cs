using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [SerializeField] protected float health = 10;
    [SerializeField] protected float speed = 1;
    protected Direction direction;

    protected virtual void Awake()
    {

    }

    protected void Move(Direction direct)
    {
        StopCoroutine("MoveCoroutine");
        direction = Direction.Left;
        StartCoroutine("MoveCoroutine");
    }
    protected void Stop()
    {
        StopCoroutine("MoveCoroutine");
    }

    IEnumerator MoveCoroutine()
    {
        while (true)
        {
            if (Time.timeScale == 0) yield return null;
            else
            {
                this.transform.Translate(Vector3.Lerp(Vector3.zero, new Vector3((float)direction * speed, 0, 0), 0.1f));
                yield return null;
            }
        }
    }
}
