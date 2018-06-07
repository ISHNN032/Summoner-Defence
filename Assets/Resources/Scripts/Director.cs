using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director : MonoBehaviour {

    public void EnableMenu()
    {
        MenuManager.Instance.OpenMenu<DialogUI>();
        DialogUI.Instance.SetDIalog("Main_01");
    }
    public void LoadGameUI()
    {
        MenuManager.Instance.OpenMenu<InGameUI>();
    }
    public void ChangeDialog(string DialogName)
    {
        Debug.Log("Changed" + DialogName);
        MenuManager.Instance.OpenMenu<DialogUI>();
        DialogUI.Instance.SetDIalog(DialogName);
    }
}
