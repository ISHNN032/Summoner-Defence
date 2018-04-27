﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mob<T> : Mob where T : Mob<T>
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected virtual void Start()
    {
        Move(Direction.Left);
    }
}

public abstract class Mob : Unit {

}