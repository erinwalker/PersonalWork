using UnityEngine;
using System.Collections;

public class TriggerZoneStop : MonoBehaviour {

    public NPCMovement NpcMovement;
    public Transform Goal;

    void OnTriggerEnter()
    {
        NpcMovement.Goal = Goal;
        if (NpcMovement.NpcState == NPCState.Follow)
        {
            NpcMovement.NpcState = NPCState.Idle;
        }
        else if (NpcMovement.NpcState == NPCState.Idle)
        {
            NpcMovement.NpcState = NPCState.Follow;
        }
    }
}
