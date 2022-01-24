using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyStates
{
    public class BaseEnemyState : BaseState
    {
        public BaseEnemy enemy;
        public BaseEnemyState(BaseEnemy daddy) : base(daddy)
        {
            enemy = daddy;
        }

        public override void Update()
        {
            base.Update();
            PrepareAtk();
        }
        public void PrepareAtk()
        {
            enemy.aimDir = (enemy.target - (Vector2)enemy.transform.position).normalized;
            if (enemy.cooldownTimer >= 0)
                enemy.cooldownTimer -= Time.deltaTime;
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            enemy.rb.velocity = enemy.moveSpeed * enemy.moveDir;
        }
    }

    public class ChasePlayer : BaseEnemyState
    {
        public ChasePlayer(BaseEnemy daddy) : base(daddy) { }
        public override void Update()
        {
            base.Update();
            enemy.moveDir = (enemy.target - (Vector2)enemy.transform.position).normalized;
            if (enemy.cooldownTimer <= 0 && Vector2.SqrMagnitude(enemy.target - (Vector2)enemy.transform.position) <= enemy.atkRange)
                enemy.ChangeState(new AtkWindup(enemy));
        }
        public override void OnExit()
        {
            base.OnExit();
            enemy.rb.velocity = Vector2.zero;
        }
    }

    public class AtkWindup : BaseEnemyState
    {
        private float currentMoveSpeed;
        public AtkWindup(BaseEnemy daddy) : base(daddy)
        {
            age = enemy.windupDur;
            isTimed = true;
        }
        public override void Update()
        {
            base.Update();
            currentMoveSpeed = Mathf.Lerp(enemy.windupSpeed, 0, 1 - age / enemy.windupDur);
        }
        public override void FixedUpdate()
        {
            enemy.rb.velocity = -currentMoveSpeed * enemy.aimDir;
        }
    }

    public class MeleeAtk : BaseEnemyState
    {
        private float currentMoveSpeed;
        public MeleeAtk(BaseEnemy daddy) : base(daddy)
        {
            age = enemy.atkDur;
            isTimed = true;
        }
        public override void Update()
        {
            base.Update();
            currentMoveSpeed = Mathf.Lerp(enemy.initialDashSpeed, enemy.endDashSpeed, 1 - age / enemy.atkDur);
        }
        public override void FixedUpdate()
        {
            enemy.rb.velocity = currentMoveSpeed * enemy.aimDir;
        }
        public override void OnExit()
        {
            enemy.cooldownTimer = enemy.atkCooldown;
            base.OnExit();
        }
    }
}
