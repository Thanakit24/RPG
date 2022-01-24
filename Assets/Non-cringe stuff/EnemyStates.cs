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
            ProcessOutput();
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            ProcessMovement();
        }
        protected virtual void ProcessOutput()
        {
            enemy.aimDir = (enemy.target - (Vector2)enemy.transform.position).normalized;
            if (enemy.cooldownTimer >= 0)
                enemy.cooldownTimer -= Time.deltaTime;
        }
        protected virtual void ProcessMovement()
        {
            enemy.rb.velocity = enemy.moveSpeed * enemy.moveDir;
        }
    }

    public class ChasePlayer : BaseEnemyState
    {
        public ChasePlayer(BaseEnemy daddy) : base(daddy) { }
        protected override void ProcessMovement()
        {
            enemy.moveDir = (enemy.target - (Vector2)enemy.transform.position).normalized;
            base.ProcessMovement();
        }
        protected override void ProcessOutput()
        {
            base.ProcessOutput();
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
        public override void OnEnter()
        {
            base.OnEnter();
            enemy.bufferedState = new MeleeAtk(enemy);
        }
        protected override void ProcessOutput()
        {
            currentMoveSpeed = Mathf.Lerp(enemy.windupSpeed, 0, 1 - age / enemy.windupDur);
        }
        protected override void ProcessMovement()
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
        protected override void ProcessOutput()
        {
            currentMoveSpeed = Mathf.Lerp(enemy.initialDashSpeed, enemy.endDashSpeed, 1 - age / enemy.atkDur);
        }
        protected override void ProcessMovement()
        {
            enemy.rb.velocity = currentMoveSpeed * enemy.aimDir;
        }
        public override void OnExit()
        {
            base.OnExit();
            enemy.cooldownTimer = enemy.atkCooldown;
        }
    }
}
