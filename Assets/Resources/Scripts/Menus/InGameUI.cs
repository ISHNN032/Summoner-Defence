using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour {
    //드레그 형식을 가지는 MoveButton 들은 개별 스크립트를 가지고 있음.
    public Button PauseButton;

    public GameObject SkillButtonLayout;
    public Button[] SkillButtons;

    private void Awake()
    {
        PauseButton.onClick.AddListener(() => {
            MenuManager.Instance.OpenMenu<PauseMenu>();
        });
        SkillButtons = SkillButtonLayout.GetComponentsInChildren<Button>();
    }
}
