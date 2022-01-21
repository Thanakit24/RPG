using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class BasePlayerState : BaseState
    {
        protected Player player;
        public BasePlayerState(Player daddy) : base(daddy)
        {
            player = daddy;
        }
        public override void Update()
        {
            base.Update();
            player.moveDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
            player.aimDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.transform.position;
        }
        protected void ProcessInputs()
        {
            if (Input.GetButtonDown("Dash"))
                daddy.ChangeState(new Dash(player));
            if (Input.GetButtonDown("LightAtk"))
                daddy.ChangeState(new LightAtk(player));
            if (Input.GetButtonDown("HeavyAtk"))
                daddy.ChangeState(new HeavyAtk(player));
        }
        protected void BufferInputs()
        {
            if (Input.GetButtonDown("Dash"))
                player.bufferedState = new Dash(player);
            if (Input.GetButtonDown("LightAtk"))
                player.bufferedState = new LightAtk(player);
            if (Input.GetButtonDown("HeavyAtk"))
                player.bufferedState = new HeavyAtk(player);
        }
    }

    public class Idle : BasePlayerState
    {
        public Idle(Player daddy) : base(daddy) { }
        public override void Update()
        {
            base.Update();
            ProcessInputs();
            if (player.moveDir == Vector2.zero)
            {
                player.anim.SetFloat(Player.MoveKey, 0);
            }
            else
            {
                player.anim.SetFloat(Player.MoveKey, 1);
                player.lastDir = player.moveDir;
            }
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            player.rb.velocity = player.moveSpeed * player.moveDir;
        }
    }

    public class Dash : BasePlayerState
    {
        private float currentDashSpeed;
        public Dash(Player daddy) : base(daddy)
        {
            age = player.dashDur;
            isTimed = true;
        }
        public override void OnEnter()
        {
            base.OnEnter();
            player.anim.SetBool(Player.DashKey, true);
        }
        public override void Update()
        {
            base.Update();
            //player.bufferedStates.Enqueue(new Dash(player));
            if (Input.GetButtonDown("Dash") && age <= player.dashDur * 0.5f)
                player.bufferedState = new Dash(player);
            if (Input.GetButtonDown("LightAtk"))
                player.bufferedState = new LightAtk(player);
            if (Input.GetButtonDown("HeavyAtk"))
                player.bufferedState = new RollAtk(player);
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            currentDashSpeed = Mathf.Lerp(player.initialDashSpeed, player.endDashSpeed, 1 - age / player.dashDur);
            player.rb.velocity = currentDashSpeed * player.lastDir;
        }
        public override void OnExit()
        {
            base.OnExit();
            player.rb.velocity = Vector2.zero;
            player.anim.SetBool(Player.DashKey, false);
            if (player.moveDir != Vector2.zero)
                player.lastDir = player.moveDir;
        }
    }

    public class LightAtk : BasePlayerState
    {
        public LightAtk(Player daddy) : base(daddy)
        {
            age = player.attSeqTimes[player.atkSeq];
            isTimed = true;
        }
        public override void OnEnter()
        {
            base.OnEnter();
            player.rb.velocity = Vector2.zero;
            player.anim.SetInteger(Player.AtkSeqKey, player.atkSeq);
            player.anim.SetBool(Player.LightAtkKey, true);
            player.atkSeq = (player.atkSeq + 1) % player.attSeqTimes.Length;

            player.lastDir = player.aimDir;
            float angle = Mathf.Atan2(player.aimDir.y, player.aimDir.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));
            player.weapon.rotation = rotation;
        }
        public override void Update()
        {
            base.Update();
            BufferInputs();
        }
        public override void OnExit()
        {
            base.OnExit();
            player.rb.velocity = Vector2.zero;
            if (!(player.bufferedState is LightAtk))
                player.atkSeq = 0;

            player.anim.SetBool(Player.LightAtkKey, false);
            if (player.moveDir != Vector2.zero)
                player.lastDir = player.moveDir;
        }
    }

    public class HeavyAtk : BasePlayerState
    {
        private float chargeDur = 0f;
        public HeavyAtk(Player daddy) : base(daddy)
        {
            age = player.atkDur;
        }
        public override void OnEnter()
        {
            base.OnEnter();
            player.anim.SetInteger(Player.AtkChargeKey, 0);
            player.anim.SetBool(Player.HeavyAtkKey, true);
        }
        public override void Update()
        {
            base.Update();
            if (Input.GetButton("HeavyAtk"))
            {
                chargeDur += Time.deltaTime;
            }
            else
            {
                if (chargeDur < player.chargeDurMax)
                {
                    //do normal charged attack
                    player.anim.SetInteger(Player.AtkChargeKey, 1);
                }
                else
                {
                    //do max charged attack
                    player.anim.SetInteger(Player.AtkChargeKey, 2);
                }
                isTimed = true;
                BufferInputs();
            }
        }
        public override void OnExit()
        {
            base.OnExit();
            player.anim.SetBool(Player.HeavyAtkKey, false);
            player.anim.SetInteger(Player.AtkChargeKey, 0);
        }
    }

    public class RollAtk : BasePlayerState
    {
        public RollAtk(Player daddy) : base(daddy)
        {
            age = player.atkDur;
            isTimed = true;
        }
        public override void OnEnter()
        {
            base.OnEnter();
            player.anim.SetBool(Player.DashAtkKey, true);
        }
        public override void Update()
        {
            base.Update();
            BufferInputs();
        }
        public override void OnExit()
        {
            base.OnExit();
            player.anim.SetBool(Player.DashAtkKey, false);
        }
    }
    
}

