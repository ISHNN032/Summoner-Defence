using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Summon<T> : Summon where T : Summon<T>{
    protected virtual void Awake()
    {
        //this.gameObject.SetActive(false);
    }

    /*
    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(1);
        this.gameObject.SetActive(true);
    }
    */
}

public abstract class Summon : MonoBehaviour
{

}
