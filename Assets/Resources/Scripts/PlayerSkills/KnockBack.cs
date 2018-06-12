using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour {
    private void Start()
    {
        float player_front_x = PlayerController.Instance.transform.position.x
            + (float)PlayerController.Instance.current_direction;

        this.transform.position = new Vector2(player_front_x, PlayerController.Instance.transform.position.y);
        StartCoroutine("Active");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Mob"))
        {
            Rigidbody2D rid = collision.GetComponent<Rigidbody2D>();
            rid.AddForce(new Vector2((float)PlayerController.Instance.current_direction * 3,0), ForceMode2D.Impulse);
        }
    }

    IEnumerator Active()
    {
        yield return new WaitForSeconds(0.4f);
        Destroy(this.gameObject);
    }
}
