﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour {
    public Button PauseButton;

    public GameObject SkillButtonLayout;
    public Button[] SkillButtons;

    private void Awake()
    {
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
        Summoner.Instance.SummonUnit<ArmorSpilt>();
    }
}
