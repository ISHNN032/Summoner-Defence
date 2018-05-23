using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { Left = -1, Right = 1 }
public enum Mode { Capture = 0, Escort, Exterminate }

public abstract class Unit : MonoBehaviour
{
    [SerializeField] protected float health = 10;
    [SerializeField] protected float speed = 0.3f;
    protected Direction direction;
    protected Direction look_direction;
    protected Mode mode;

    [SerializeField] protected float disableTime = 1;
    protected WaitForSeconds wait;

    protected Rigidbody2D _rigidbody;
    protected BoxCollider2D _collider;

    [SerializeField] GameObject hitbox;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();

        wait = new WaitForSeconds(disableTime);
    }
    protected virtual void Start()
    {
        mode = Mode.Capture;
        StartCoroutine("CheckOverlap");
        StartCoroutine("FindEnemy");
        //hitbox를 따로 두는 것 대신 오버랩 박스를 이용
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        //_rigidbody.AddForce(Vector3.right + Vector3.up * 10, ForceMode2D.Impulse);
        Debug.Log(string.Format("{0} : {1}", this.name, collision.name));
    }

    protected virtual void Attack()
    {
        GameObject attack = GameObject.Instantiate(hitbox,this.transform);
        attack.transform.Translate((float)look_direction * 0.5f,0,0);
    }

    protected void Move(Direction direct)
    {
        StopCoroutine("EscortCoroutine");
        StopCoroutine("Combat");
        StopCoroutine("Chase");

        StopCoroutine("MoveCoroutine");
        direction = direct;
        StartCoroutine("MoveCoroutine");
    }

    protected void Stop()
    {
        StopCoroutine("MoveCoroutine");
        StopCoroutine("EscortCoroutine");
        StopCoroutine("Combat");
        StopCoroutine("Chase");
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
        StartCoroutine("EFindEnemy");
    }

    //*/코루틴에서 실행해주는 내부적인 기능함수>>

    private void DirectionToPlayer()
    {
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
            StopCoroutine("MoveCoroutine");
        }
        else if (distance >= -2 && distance < 0)
        {
            look_direction = Direction.Left;
            StopCoroutine("MoveCoroutine");
        }
    }

    //*/코루틴에서 실행해주는 내부적인 기능함수<<


    protected IEnumerator EscortCoroutine()
    {
        while (true)
        {
            DirectionToPlayer();
            yield return null;
        }
    }

    protected IEnumerator MoveCoroutine()
    {
        while (true)
        {
            if (Time.timeScale == 0) {
                yield return null;
                continue;
            }
            this.transform.Translate(Vector2.Lerp(Vector2.zero, new Vector2((float)direction * speed, 0), 0.1f));
            look_direction = direction;
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

            var overlap = Physics2D.OverlapBox(transform.position, _collider.size, 0, 1 << 10);

            //오버랩이 존재하며, 본인과 다른 태그의 히트박스일때
            if (overlap != null && !this.tag.Equals(overlap.tag))
            {
                float distance = this.transform.position.x - overlap.transform.parent.position.x;

                Debug.Log(string.Format("{0} :: {1} - {2}", this.name, overlap.name));
                _rigidbody.AddForce(new Vector2(distance * 3,0), ForceMode2D.Impulse);
                yield return wait;
                //레이어10 (hitbox)에 해당하는 것이 충돌되었을때 지정된 초 동안 검사중지
            }
            yield return null;
        }
    }
    
    //적을 찾는 코루틴이다
    protected IEnumerator FindEnemy()
    {
        while (true)
        {
            if (Time.timeScale == 0)
            {
                yield return null;
                continue;
            }

            Debug.DrawLine(transform.position, (Vector2)transform.position + new Vector2((float)look_direction, 0) * 2, Color.cyan);
            var hits = Physics2D.RaycastAll(transform.position, new Vector2((float)look_direction, 0), 2, 1 << 8);
            for(int i=0; i< hits.Length; ++i)
            {
                var hit = hits[i];
                if (hit && !this.tag.Equals(hit.transform.tag))
                {
                    Debug.Log(string.Format("{0} Found The Enemy {1}", this.name, hit.transform.name));
                    StartCoroutine("Combat",hit.transform.gameObject);
                    StartCoroutine("Chase", hit.transform.gameObject);
                    yield return wait;
                }
            }
            yield return null;
        }
    }

    protected IEnumerator EFindEnemy()
    {
        while (true)
        {
            if (Time.timeScale == 0)
            {
                yield return null;
                continue;
            }

            Debug.DrawLine((Vector2)transform.position + Vector2.left, (Vector2)transform.position + Vector2.right * 2, Color.cyan);

            var hits = Physics2D.RaycastAll((Vector2)transform.position - Vector2.left * 2f , Vector2.right * 2f, 4, 1 << 8);
            for (int i = 0; i < hits.Length; ++i)
            {
                var hit = hits[i];
                if (hit && !this.tag.Equals(hit.transform.tag))
                {
                    Debug.Log(string.Format("{0} Found The Enemy {1}", this.name, hit.transform.name));
                    StartCoroutine("Combat", hit.transform.gameObject);
                    yield return wait;
                }
            }
            yield return null;
        }
    }

    protected IEnumerator Combat(GameObject enemy)
    {
        while (enemy != null)
        {
            Attack();
            yield return new WaitForSeconds(1);
        }
    }

    protected IEnumerator Chase(GameObject enemy)
    {
        StopCoroutine("FindEnemy");
        while (enemy != null)
        {
            if (Time.timeScale == 0)
            {
                yield return null;
                continue;
            }

            float distance = this.transform.position.x - enemy.transform.position.x;

            Debug.DrawLine(transform.position, (Vector2)transform.position + Vector2.up + Vector2.left, Color.red);

            if (distance > 1.2f)
            {
                direction = Direction.Left;
            }
            else if (distance < -1.2f)
            {
                direction = Direction.Right;
            }
            else if (distance <= 1.2 && distance > 0)
            {
                look_direction = Direction.Left;
                StopCoroutine("MoveCoroutine");
            }
            else if (distance >= -1.2 && distance < 0)
            {
                look_direction = Direction.Right;
                StopCoroutine("MoveCoroutine");
            }
            yield return null;
        }
    }
}
