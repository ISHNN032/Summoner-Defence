using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabLoader : MonoBehaviour {
    //이름을 몹 리로더 라던가 바꾸는 게 좋지 않을까?
    public static PrefabLoader Instance;

    public SampleMob sampleMobPrefab;

    private void Awake()
    {
        Instance = this;
    }

    public T GetMobPrefab<T>() where T : Mob
    {
        if (typeof(T) == typeof(SampleMob))
        {
            return sampleMobPrefab as T;
        }
        throw new MissingReferenceException();
    }
}
