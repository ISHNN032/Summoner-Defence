using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorSpilt : Summon<ArmorSpilt> {
    protected override void Awake()
    {
        base.Awake();
        Debug.Log(this.gameObject.name + " /Armor Script");
    }
}
