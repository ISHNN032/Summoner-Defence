using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour {

    protected virtual void Start () {
        Spawn();
	}

    public void Spawn()
    {
        StartCoroutine(SpawnCoroutine<SampleMob>(6, new WaitForSeconds(0.2f), new WaitForSeconds(2)));
    }
    //SpawnConrountune을 제네릭<>부분을 넣어 실행시킨다.

    protected IEnumerator SpawnCoroutine<T>(int number, WaitForSeconds startTime, WaitForSeconds delay) where T : Mob
    {
        var prefab = PrefabLoader.Instance.GetMobPrefab<T>();
        yield return startTime;
        while (number-- > 0)
        {
            Mob mob = Instantiate<Mob>(prefab);
            mob.transform.position = new Vector2(25,0);
            yield return delay;
        }
    }
}
