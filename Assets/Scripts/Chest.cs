using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    protected override void OnCollect()
    {
        base.OnCollect();
        Debug.Log("Grand power");
    }
}
