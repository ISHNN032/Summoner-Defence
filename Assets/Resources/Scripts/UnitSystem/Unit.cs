using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [SerializeField] protected float health = 10;
    [SerializeField] protected float speed = 0.3f;
    protected Direction direction;

    [SerializeField] protected float disableTime = 1;
    protected WaitForSeconds wait;

    protected Rigidbody2D _rigidbody;
    protected BoxCollider2D _collider;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();

        wait = new WaitForSeconds(disableTime);
    }
    protected virtual void Start()
    {
        StartCoroutine("CheckOverlap");
        //hitbox를 따로 두는 것 대신 오버랩 박스를 이용
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        //_rigidbody.AddForce(Vector3.right + Vector3.up * 10, ForceMode2D.Impulse);
        Debug.Log(string.Format("{0} : {1}", this.name, collision.name));
    }

    protected void Move(Direction direct)
    {
        StopCoroutine("MoveCoroutine");
        direction = direct;
        StartCoroutine("MoveCoroutine");
    }
    protected void Stop()
    {
        StopCoroutine("MoveCoroutine");
    }

    protected IEnumerator MoveCoroutine()
    {
        while (true)
        {
            if (Time.timeScale == 0) yield return null;

            this.transform.Translate(Vector3.Lerp(Vector3.zero, new Vector3((float)direction * speed, 0, 0), 0.1f));
            yield return null;
        }
    }

    //HitBox역할을 하는 코루틴이다
    protected IEnumerator CheckOverlap()
    {
        while (true)
        {
            if (Time.timeScale == 0) yield return null;

            var overlap = Physics2D.OverlapBox(transform.position, _collider.size, 0, 1 << 10);
            //오버랩이 존재하며, 본인과 다른 태그의 히트박스일때
            if (overlap != null && !this.tag.Equals(overlap.tag))
            {
                Debug.Log(string.Format("{0} :: {1}", this.name, overlap.name));
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
            if (Time.timeScale == 0) yield return null;

            var overlap = Physics2D.OverlapBox(transform.position, _collider.size, 0, 1 << 10);
            //오버랩이 존재하며, 본인과 다른 태그의 히트박스일때
            if (overlap != null && !this.tag.Equals(overlap.tag))
            {
                Debug.Log(string.Format("{0} :: {1}", this.name, overlap.name));
                yield return wait;
                //레이어10 (hitbox)에 해당하는 것이 충돌되었을때 지정된 초 동안 검사중지
            }
            yield return null;
        }
    }
}
