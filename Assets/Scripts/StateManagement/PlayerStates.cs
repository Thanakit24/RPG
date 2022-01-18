using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class BasePlayerState : BaseState
    {
        //PlayerStats
        protected Player player;

        public BasePlayerState(Player daddy) : base(daddy)
        {
            player = daddy;
        }
        public override void Update()
        {
            ProcessInputs();
        }

        protected virtual void ProcessInputs()
        {
            player.moveDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

            if (Input.GetButton("Melee"))
            {
                //Charge up melee
            }
            if (Input.GetButtonUp("Melee"))
            {
                //Fire melee
            }

            if (Input.GetButton("Ranged") && !Input.GetButton("Modifier"))
            {
                //Aim light ranged attack (Bow, machinegun, shotgun)
            }
            else if (Input.GetButton("Ranged") && Input.GetButton("Modifier"))
            {
                //Heavy or alternate ranged attack
            }
        }

    }

    public class Idle : BasePlayerState
    {
        public Idle(Player daddy) : base(daddy) { }

        public override void Update()
        {
            base.Update();
            if (player.moveDir != Vector2.zero) //Is moving
                daddy.ChangeState(new Move(player));
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            player.rb.velocity = Vector2.zero;
        }

        protected override void ProcessInputs()
        {
            base.ProcessInputs();
            //if (Input)
        }
    }

    public class Move : BasePlayerState
    {

        public Move(Player daddy) : base(daddy) { }

        // Update is called once per frame
        public override void Update()
        {
            base.Update();
            if (player.moveDir == Vector2.zero) //Is not moving
                daddy.ChangeState(new Idle(player));
            else
                player.lastDir = player.moveDir;

        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            player.rb.velocity = player.moveSpeed * player.moveDir;
        }
    }
    public class Dash : BasePlayerState
    {
        public Dash(Player daddy) : base(daddy) { }
        private float dashTimer;

        public override void OnEnter()
        {
            base.OnEnter();
            dashTimer = player.dashDur;
        }
        public override void Update()
        {
            base.Update();
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0)
                daddy.GotoBase();
        }

        protected override void ProcessInputs()
        {
            if (dashTimer >= player.dashDur / 2)
                return;

            if (Input.GetButton("Melee"))
            {
                //Charge up melee
                player.bufferedState = new Attack(player);
            }
            
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            player.rb.velocity = player.dashSpeed * player.lastDir;
        }
    }
    public class Attack : BasePlayerState
    {
        public Attack(Player daddy) : base(daddy) { }

    }


}

