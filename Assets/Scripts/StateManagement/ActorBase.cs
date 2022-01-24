using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorBase : StateMachine
{
    [Space]
    [Header("stats")]
    public float flashDur = 0.2f;
    public Color flashColor;
    public Color regularColor;
    public int currentHealth;
    public int maxHealth;
    public float moveSpeed = 5f;

    public Status gamerStatus;
    public List<StatusState> statusStates = new List<StatusState>();

    protected override void Start()
    {
        base.Start();
        //AddStatus(gamerStatus);
    }

    protected override void Update()
    {
        base.Update();

        for (int i = 0; i < statusStates.Count; i++)
        {
            var statusState = statusStates[i];
        
            statusState.timer -= Time.deltaTime;
            if (statusState.timer <= 0)
            {
                statusState.status.OnExit(this);
                statusStates.Remove(statusState);
            }
        }
    }


    public void AddStatus(Status status)
    {
        status.OnEnter(this);
        statusStates.Add(new StatusState(status));
    }
}