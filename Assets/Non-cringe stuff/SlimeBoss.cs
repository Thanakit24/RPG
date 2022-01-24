using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Slime
{
    public class SlimeBoss : BaseEnemy
    {
        public float scale;
        protected override void Update()
        {
            base.Update();
            if (currentHealth <= maxHealth * 0.5f)
            {
                Despawn();
                SlimeBoss split1 = Instantiate(this);
                split1.maxHealth = Mathf.FloorToInt(maxHealth * 0.5f);
                SlimeBoss split2 = Instantiate(this);
                split2.maxHealth = Mathf.FloorToInt(maxHealth * 0.5f);
            }
        }
    }
}
