using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : Menu<PauseMenu>
{
    public Button ResumeButon;

    protected override void Awake()
    {
        Time.timeScale = 0;
        ResumeButon.onClick.AddListener(() => {
            Time.timeScale = 1;
            MenuManager.Instance.CloseMenu();
        });
    }

    public override void OnBackPressed()
    {
        Time.timeScale = 1;
        MenuManager.Instance.CloseMenu();
    }
}
