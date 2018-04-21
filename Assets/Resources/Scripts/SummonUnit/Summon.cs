using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Summon<T> : Summon where T : Summon<T>{
    protected virtual void Awake()
    {

    }
}

public abstract class Summon : MonoBehaviour
{

}
