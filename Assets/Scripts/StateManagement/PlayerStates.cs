using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class Idle : BaseState
    {
        //PlayerStats;
        public Idle(Player daddy) : base(daddy)
        {

        }

        public override void Start()
        {

        }

        // Update is called once per frame
        public override void Update()
        {

        }
    }


    public class Chase : BaseState
    {

        public Chase(Player daddy) : base(daddy)
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

