using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {
    public static MenuManager Instance { get; private set; }

    public InGameUI InGameUIPreFab;
    public PauseMenu PauseMenuPreFab;
    public DialogUI DialogUIPreFab;

    private Stack<Menu> menuStack = new Stack<Menu>();

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    private void OnDestroy()
    {
        Instance = null;
    }

    public void OpenMenu<T>() where T : Menu
    {
        var prefab = GetPreFab<T>();
        var instance = Instantiate<Menu>(prefab, this.transform);

        if(menuStack.Count > 0)
            menuStack.Peek().gameObject.SetActive(false);

        menuStack.Push(instance);
    }

    public void CloseMenu()
    {
        var instance = menuStack.Peek();
        Destroy(instance.gameObject);
        
        menuStack.Pop();

        if (menuStack.Count > 0)
            menuStack.Peek().gameObject.SetActive(true);
    }

    public T GetPreFab<T>() where T : Menu
    {
        if (typeof(T) == typeof(InGameUI))
        {
            return InGameUIPreFab as T;
        }
        if (typeof(T) == typeof(PauseMenu))
        {
            return PauseMenuPreFab as T;
        }
        if(typeof(T) == typeof(DialogUI))
        {
            return DialogUIPreFab as T;
        }
        throw new MissingReferenceException();
    }
}
