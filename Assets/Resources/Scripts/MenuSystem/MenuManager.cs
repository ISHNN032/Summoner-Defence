using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {
    public static MenuManager Instance { get; private set; }

    public PauseMenu PauseMenuPreFeb;

    private Stack<Menu> menuStack = new Stack<Menu>();

    private void Awake()
    {
        Instance = this;
    }
    private void OnDestroy()
    {
        Instance = null;
    }

    public void OpenMenu<T>() where T : Menu
    {
        var prefeb = GetPreFeb<T>();
        var instance = Instantiate<Menu>(prefeb, transform);

        if(menuStack.Count > 0)
            menuStack.Peek().gameObject.SetActive(false);

        menuStack.Push(instance);
    }

    public void CloseMenu()
    {
        var instance = menuStack.Peek();
        Destroy(instance.gameObject);

        if (menuStack.Count > 0)
            menuStack.Pop().gameObject.SetActive(true);
    }

    public T GetPreFeb<T>() where T : Menu
    {
        if(typeof(T) == typeof(PauseMenu))
        {
            return PauseMenuPreFeb as T;
        }
        throw new MissingReferenceException();
    }
}
