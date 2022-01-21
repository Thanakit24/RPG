using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sample Status", menuName = "Status/Standard")]
public class Status : ScriptableObject
{
    #if UNITY_EDITOR
    [ScriptableObjectId]
    #endif
    public string Id;
    public float maxTime = 60 * 1f;

    public virtual void OnEnter(ActorBase actorBase)
    {
       Debug.Log($"entering {actorBase.name}  {this.name}"); 
        
    }

    public virtual void OnExit(ActorBase actorBase)
    {
       Debug.Log($"exiting {actorBase.name}  {this.name}"); 
    }
    
    
}

[System.Serializable]
public class StatusState
{
    public StatusState(Status _status)
    {
        status = _status;
        timer = status.maxTime;
    }
    public Status status;
    public float timer;
}

[CreateAssetMenu(fileName = "Iframe", menuName = "Status/iFrame")]
public class IframeStatus : Status
{
    public override void OnEnter(ActorBase actorBase)
    {
        base.OnEnter(actorBase);
        Player player = (Player) actorBase;
        if (!player) return;
        player.iframeDur = float.MaxValue;
    }

    public override void OnExit(ActorBase actorBase)
    {
        base.OnExit(actorBase);
        Player player = (Player) actorBase;
        if (!player) return;
        player.iframeDur = 0;
    }
}
