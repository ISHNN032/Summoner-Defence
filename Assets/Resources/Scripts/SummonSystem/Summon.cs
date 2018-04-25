using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Summon<T> : Summon where T : Summon<T>{
    [SerializeField] private float speed = 1;
    private Direction direction;

    protected virtual void Awake()
    {
        direction = Direction.Right;
    }

    protected virtual void Start()
    {
        Move();
    }

    protected virtual void Update()
    {
        Control();
    }

    public override void Control()
    {
        //현재키보드 인풋으로 되어 있으나, 이후 드래그 구현필요
        if (Input.anyKey)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                direction = Direction.Left;
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                direction = Direction.Right;
            }
        }
    }

    public override void Move()
    {
        StartCoroutine("MoveCoroutine");
    }
    IEnumerator MoveCoroutine()
    {
        while (true)
        {
            if(Time.timeScale == 0) yield return null;
            else
            {
                Debug.Log("c");
                this.transform.Translate(Vector3.Lerp(Vector3.zero, new Vector3((float)direction * speed, 0, 0), 0.1f));
                yield return null;
            }
        }
    }
}

public abstract class Summon : MonoBehaviour
{
    public abstract void Control();
    public abstract void Move();
    //public abstract void Attack();
}