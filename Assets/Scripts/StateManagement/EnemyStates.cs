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
            enemy.targetDir = (enemy.playerPos - enemy.currentPos).normalized;
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
            enemy.moveDir = (enemy.playerPos - enemy.currentPos).normalized;
            base.ProcessMovement();
        }
        protected override void ProcessOutput()
        {
            base.ProcessOutput();
            if (enemy.cooldownTimer <= 0 && Vector2.Distance(enemy.currentPos, enemy.playerPos) <= enemy.atkRange)
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
            enemy.rb.velocity = -currentMoveSpeed * enemy.targetDir;
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
            enemy.rb.velocity = currentMoveSpeed * enemy.targetDir;
        }
        public override void OnExit()
        {
            base.OnExit();
            enemy.cooldownTimer = enemy.atkCooldown;
        }
    }
}
