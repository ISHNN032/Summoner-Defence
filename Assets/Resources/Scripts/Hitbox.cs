using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour {
    public float attack_power { get; private set; }

    private void Awake()
    {
        this.attack_power = this.GetComponentInParent<Unit>().attack_power;
    }

    void Start () {
        StartCoroutine("Active");
	}
    IEnumerator Active()
    {
        yield return new WaitForSeconds(0.4f);
        Destroy(this.gameObject);
    }
}
