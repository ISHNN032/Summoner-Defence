using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InGameUI : Menu<InGameUI> {
    public Button PauseButton;

    public GameObject SkillButtonLayout;
    public Button[] SkillButtons;
    public GameObject[] Skills = new GameObject[5];

    protected override void Awake()
    {
        base.Awake();
        PauseButton.onClick.AddListener(() => {
            MenuManager.Instance.OpenMenu<PauseMenu>();
        });
        SkillButtons = SkillButtonLayout.GetComponentsInChildren<Button>();
        for (int i=0; i<SkillButtons.Length; ++i)
        {
            int index = i;
            SkillButtons[i].onClick.AddListener(()=> Summon(index));
        }
    }

    public void Summon(int index)
    {
        Debug.Log("Summon #" + index);
        if(index == 0)
            Summoner.Instance.SummonUnit<ArmorSpilt>();
        else
        {
            Instantiate(Skills[index], PlayerController.Instance.transform);
        }
    }

    public override void OnBackPressed()
    {
        MenuManager.Instance.OpenMenu<PauseMenu>();
    }
}
