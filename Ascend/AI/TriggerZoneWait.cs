using UnityEngine;
using System.Collections;

public class TriggerZoneWait : MonoBehaviour {

    public NPCMovement NpcMovement;
    public int WaitDuration = 5;
    public Transform Goal;

    void OnTriggerEnter()
    {
        NpcMovement.Goal = Goal;
        if (NpcMovement.NpcState == NPCState.Follow)
        {
            NpcMovement.NpcState = NPCState.Idle;
            StartCoroutine("Timer");
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(WaitDuration);
        NpcMovement.NpcState = NPCState.Follow;
    }
}
