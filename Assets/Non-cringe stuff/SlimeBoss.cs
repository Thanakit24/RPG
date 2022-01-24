using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBoss : BaseEnemy
{
    public float scale;
    protected override void Update()
    {
        base.Update();
        if (currentHealth <= maxHealth * 0.5f)
        {
            Despawn();
            SlimeBoss split = Instantiate(this);
            split.maxHealth = Mathf.FloorToInt(maxHealth * 0.5f);
        }
    }
}
