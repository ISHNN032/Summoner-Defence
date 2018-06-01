using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum GameMode { Exterminate, Siege, Protect }

public class GameManager : MonoBehaviour {
    private static GameManager sInstance = null;

    public GameMode Current_GameMode { get; private set; }

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
        Application.targetFrameRate = 60;

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

    public void ChangeScene(int scene_num)
    {
        SceneManager.LoadScene(scene_num);
    }

    public void Initialize_Game(GameMode mode)
    {
        switch (mode) {
            case GameMode.Exterminate:
                {

                    break;
                }
            case GameMode.Siege:
                {

                    break;
                }
            case GameMode.Protect:
                {

                    break;
                }
        }
    }

}
