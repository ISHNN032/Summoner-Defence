using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner : MonoBehaviour {
    public static Summoner Instance { get; private set; }

    private List<Summon> SummonList = new List<Summon>();
    public ArmorSpilt ArmorSpiltPrefab;

    private void Awake()
    {
        Instance = this;
    }
    private void OnDestroy()
    {
        Instance = null;
    }

    public void SummonUnit<T>() where T : Summon
    {
        var prefab = GetSummonPrefab<T>();
        var instance = Instantiate<Summon>(prefab);
        instance.transform.position = this.transform.position + Vector3.right;
        SummonList.Add(instance);
    }

    public T GetSummonPrefab<T>() where T : Summon
    {
        if(typeof(T) == typeof(ArmorSpilt))
        {
            return ArmorSpiltPrefab as T;
        }
        throw new MissingReferenceException();
    }
}