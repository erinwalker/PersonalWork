using UnityEngine;
using System.Collections;

public class TriggerZoneMoveTo : MonoBehaviour {

    public NPCMovement NpcMovement;
    public Transform Goal;

    void OnTriggerEnter()
    {
        NpcMovement.NpcState = NPCState.Follow;
        NpcMovement.Goal = Goal;
    }
}
