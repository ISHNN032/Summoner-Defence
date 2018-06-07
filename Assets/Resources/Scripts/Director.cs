using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director : MonoBehaviour {
    [SerializeField] private GameObject MenuManager;

    public void EnableMenu()
    {
        MenuManager.SetActive(true);
    }
}
