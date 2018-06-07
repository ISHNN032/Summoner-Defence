using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { Left = -1, Right = 1 }
public enum Mode { Capture = 0, Escort, Exterminate }

public abstract class Unit : MonoBehaviour
{
    [SerializeField] protected float health = 10;
    [SerializeField] protected GameObject hp_bar;
    [SerializeField] public readonly float attack_power = 3;

    [SerializeField] protected float speed = 0.3f;
    protected Direction direction;
    protected Direction look_direction;
    protected Mode mode;

    [SerializeField] protected Direction startdirection = 0;

    [SerializeField] protected float find_distance = 2f;

    [SerializeField] protected float disableTime = 1;
    protected WaitForSeconds wait;

    protected Rigidbody2D _rigidbody;
    protected BoxCollider2D _collider;

    protected SpriteRenderer sprite;

    [SerializeField] GameObject hitbox;
    [SerializeField] LayerMask enemy_layer;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();

        wait = new WaitForSeconds(disableTime);
    }

    protected virtual void Start()
    {
        mode = Mode.Capture;

        StartCoroutine("CheckOverlap");
        StartCoroutine("FindCoroutine");
    }

    protected void Move(Direction direct)
    {
        StopCoroutine("MoveCoroutine");
        StopCoroutine("CombatCoroutine");
        StopCoroutine("EscortCoroutine");
        
        direction = direct;
        look_direction = direct;

        switch (direct)
        {
            case Direction.Left: sprite.flipX = true; break;
            case Direction.Right: sprite.flipX = false; break;
        }

        StartCoroutine("MoveCoroutine");
    }

    protected void Stop()
    {
        StopCoroutine("MoveCoroutine");
        StopCoroutine("EscortCoroutine");
    }

    protected void Capture()
    {

    }

    protected void Escort()
    {
        StartCoroutine("EscortCoroutine");
    }

    protected void Exterminate()
    {
        StartCoroutine("FindEnemy");
    }

    protected IEnumerator MoveCoroutine()
    {
        while (true)
        {
            if (Time.timeScale == 0)
            {
                yield return null;
                continue;
            }
            this.transform.Translate(Vector2.Lerp(Vector2.zero, new Vector2((float)direction * speed, 0), 0.1f));
            //look_direction = direction;
            yield return null;
        }
    }

    protected IEnumerator FindCoroutine()
    {
        while (true)
        {
            if (Time.timeScale == 0)
            {
                yield return null;
                continue;
            }

            Vector2 Raystart = transform.position;
            Raystart.x += (float)look_direction * 0.5f;

            Debug.DrawRay(Raystart, new Vector2((float)look_direction, 0) * find_distance, Color.cyan);

            var hit = Physics2D.Raycast(Raystart, new Vector2((float)look_direction,0) , find_distance, enemy_layer);
            if (hit && !this.tag.Equals(hit.transform.tag))
            {
                StartCoroutine("CombatCoroutine", hit.transform.gameObject);
                yield return wait;
            }
            yield return null;
        }
    }
    
    protected IEnumerator CombatCoroutine(GameObject enemy)
    {
        StopCoroutine("FindCoroutine");
        float distance;

        Direction p_direct = direction;

        while (enemy != null)
        {
            //적과 일정 거리 이상 가까워지면 멈춘 후 공격
            distance = this.transform.position.x - enemy.transform.position.x;

            if (Mathf.Abs(distance) <= 1.2f)
            {
                direction = 0;

                if (distance > 0)
                {
                    look_direction = Direction.Left;
                    sprite.flipX = true;
                }
                else
                {
                    look_direction = Direction.Right;
                    sprite.flipX = false;
                }

                GameObject attack = GameObject.Instantiate(hitbox, this.transform);
                attack.transform.Translate((float)look_direction * 0.5f, 0, 0);
                yield return new WaitForSeconds(1);
            }
            else
            {
                if (distance > 0)
                {
                    direction = Direction.Left;
                }
                else
                {
                    direction = Direction.Right;
                }
                yield return null;
            }
        }
        StartCoroutine("FindCoroutine");
        direction = p_direct;
    }
    
    protected IEnumerator EscortCoroutine()
    {
        while (true)
        {
            //플레이어에게 일정 거리를 두고 따라온다.
            Debug.DrawLine(transform.position, (Vector2)transform.position + Vector2.up, Color.yellow);
            float distance = this.transform.position.x - PlayerController.Instance.transform.position.x;

            if (distance > 2)
            {
                direction = Direction.Left;
            }
            else if (distance < -2)
            {
                direction = Direction.Right;
            }
            else if (distance <= 2 && distance > 0)
            {
                look_direction = Direction.Right;
                direction = 0;
            }
            else if (distance >= -2 && distance < 0)
            {
                look_direction = Direction.Left;
                direction = 0;
            }
            
            yield return null;
        }
    }

    //HitBox역할을 하는 코루틴이다
    protected IEnumerator CheckOverlap()
    {
        while (true)
        {
            if (Time.timeScale == 0)
            {
                yield return null;
                continue;
            }

            var overlap = Physics2D.OverlapBox(transform.position, _collider.size, 0, 1 << 11);

            //오버랩이 존재하며, 본인과 다른 태그의 히트박스일때
            if (overlap != null && !this.tag.Equals(overlap.tag))
            {
                health -= overlap.GetComponent<Hitbox>().attack_power;
                hp_bar.transform.localScale = new Vector3(health * 0.2f, 0.2f, 1);
                float distance = this.transform.position.x - overlap.transform.parent.position.x;
                _rigidbody.AddForce(new Vector2(distance * 3, 0), ForceMode2D.Impulse);

                if (health <= 0) Destroy(this.gameObject);
                yield return wait;
                //레이어11 (hitbox)에 해당하는 것이 충돌되었을때 지정된 초 동안 검사중지
            }
            yield return null;
        }
    }
}
