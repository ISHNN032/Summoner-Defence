using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogUI : Menu<DialogUI> {
    [SerializeField] private Text d_name;
    [SerializeField] private Text d_script;

    List<Dialog> dialogs = new List<Dialog>();

    int count = 0;

    protected override void Awake()
    {
        base.Awake();
        Time.timeScale = 0;
    }

    public void SetDIalog(string dialog_name)
    {
        dialogs = DialogManager.ReadDialog(dialog_name);
        d_name.text = dialogs[0].name;
        d_script.text = dialogs[0].script;
    }
    
    public void OnCliked()
    {
        if (++count == dialogs.Capacity)
        {
            count = 0;
            Time.timeScale = 1;
            MenuManager.Instance.CloseMenu();
        }
        d_name.text = dialogs[count].name;
        d_script.text = dialogs[count].script;
    }

    public override void OnBackPressed()
    {

    }
}
