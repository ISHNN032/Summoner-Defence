using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {
    private Rigidbody2D rigi;

    private void Awake()
    {
        rigi = PlayerController.Instance.gameObject.GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        rigi.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        this.gameObject.SetActive(false);
    }
}
