using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Summon<T> : Summon where T : Summon<T>{

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        Move(Direction.Right);
    }

    protected virtual void Update()
    {
        Control();
    }

    protected override void Control()
    {
        //현재키보드 인풋으로 되어 있으나, 이후 드래그 구현필요
        if (Input.anyKey)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Move(Direction.Left);
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                Stop();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                Move(Direction.Right);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                mode = Mode.Capture;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                mode = Mode.Escort;
                Escort();
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                mode = Mode.Exterminate;
            }
        }
    }
}

public abstract class Summon : Unit
{
    protected abstract void Control();
}