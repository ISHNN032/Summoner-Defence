using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {
    public Attack instance;

    private void Awake()
    {
        instance = this;
    }
    
    void Start () {
        StartCoroutine("AttackCoroutine");
	}

    IEnumerator AttackCoroutine()
    {

        yield return null;
    }
}
