using UnityEngine;

public enum NPCState {Follow, Idle};

public class NPCMovement : MonoBehaviour
{
    //Public Variables
    public Transform Goal;
    public NPCState NpcState;

    //Private Variables
    private NavMeshAgent agent;
    private const float SPEED = 10;
    private float magnitudeOfSpeed;

    //Instantiate Variables 
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        NpcState = NPCState.Follow;
    }

    //Update 
    void Update()
    {
        SwitchState();
        ChangeSpeed();
    }

    //Follow method sets the agents destination to the set goal and is always looking at the goal
    void Follow()
    {
        if (agent.transform.position != agent.destination)
        {
            agent.destination = Goal.localPosition;
        }
        this.transform.LookAt(Goal);
    }

    //Switches between the states. Call this when the state is changed to set the new behavior
    void SwitchState()
    {
        switch (NpcState)
        {
            case NPCState.Follow:
                Follow();
                break;
            case NPCState.Idle:
                break;
        }
    }

    //Changes speed based on distance
    void ChangeSpeed()
    {
        magnitudeOfSpeed = (agent.remainingDistance / 20); //Dividing by 20 makes the speed a good average speed

        agent.speed = SPEED * magnitudeOfSpeed;
    }
}
