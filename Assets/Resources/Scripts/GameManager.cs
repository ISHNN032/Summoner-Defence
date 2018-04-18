using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private static GameManager sInstance = null;

    public static GameManager Instance
    {
        get
        {
            if (sInstance == null)
            {
                sInstance = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (sInstance == null)
                {
                    Debug.Log("Not Found" + sInstance.ToString());
                    return null;
                }
            }
            return sInstance;
        }
    }

    private void Awake()
    {
        if (sInstance == null)
            sInstance = this;

        else if (sInstance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(this.gameObject);
    }

    private void OnDestroy()
    {
        sInstance = null;
    }
}
