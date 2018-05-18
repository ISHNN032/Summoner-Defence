using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour {

	void Start () {
        StartCoroutine("Active");
	}
    IEnumerator Active()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(this.gameObject);
    }
}
