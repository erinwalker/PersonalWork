using UnityEngine;
using System.Collections.Generic;

public enum NPCState {Follow, Idle, Wander};

public class NPCMovement : MonoBehaviour
{
    //Public Variables
    public Transform Goal;
    public NPCState NpcState;
    public float DistanceFromPlayerUntilFollow;
    public float DistanceFromPlayerUntilPOICheck;
    private bool followPlayer;
    public float BaseSpeed = 10;
    public float MaxSpeed = 15;
    public float MinSpeed = 5;
    public float PoICheckRadius;
    public List<PointofInterest> PointsOfInterest;

    //Private Variables
    private NavMeshAgent agent;
    private float magnitudeOfSpeed;
    private GameObject player;
    private float yChange = 0;
    private bool yChangeUp = true;
    //Constants
    private const int DIVIDING_FACTOR = 20; 

    //Instantiate Variables 
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        NpcState = NPCState.Follow;
        player = GameObject.Find("Player");
        followPlayer = true;
    }

    //Update 
    void Update()
    {
        if (!ParserFunctionsScript.InPerspectiveShift)
        {
            if (followPlayer)
            {
                if (Vector3.Distance(player.transform.position, this.gameObject.transform.position) <= DistanceFromPlayerUntilPOICheck)
                {
                    followPlayer = false;
                }
            }

            //CheckForSurroundingPointsOfInterest(transform.position, PoICheckRadius);
            SwitchState();
            if (!followPlayer)
            {
                PointOfInterestCheck();
            }
            ChangeSpeed();
            //Float();
            
            //Hack to make the player go to first convo poi and once there the npc can move and do other pois
			if(Goal.name == "1ConvoTriggerIntro")
			{
				if(Vector3.Distance(player.transform.position, Goal.gameObject.transform.position) <= 3)
				{
					Goal.GetComponent<PointofInterest>().CurrentWeight = 0;
				}
			}
        }
    }
    void Float()
    {
        if (yChangeUp)
        {
            yChange = .1f;
        }
        else
        {
            yChange -= .1f;
        }

        if (yChange >= 1 && yChangeUp)
        {
            yChange -= .1f;
            yChangeUp = false;
        }
      
        if (yChange <= -1 && !yChangeUp)
        {
            yChange += .1f;
            yChangeUp = true;
        }

        transform.position = new Vector3(transform.position.x, transform.position.y + yChange, transform.position.z);
    }
    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "DynamicPoI")
        {
            if (!PointsOfInterest.Contains(c.gameObject.GetComponent<PointofInterest>()))
                PointsOfInterest.Add(c.gameObject.GetComponent<PointofInterest>());
        }
    }

    //Follow method sets the agents destination to the set goal and is always looking at the goal
    void Move()
    {
        if (agent.transform.position != agent.destination)
        {
			agent.destination = Goal.position;
        }
        this.transform.LookAt(Goal);
    }

    //Switches between the states. Call this when the state is changed to set the new behavior
    void SwitchState()
    {
        switch (NpcState)
        {
            case NPCState.Follow:
                Goal = GameObject.FindGameObjectWithTag("Player").transform;
                Move();
                break;
            case NPCState.Wander:            
                Move();
                break;
            case NPCState.Idle:
                break;
        }
    }

    //Changes speed based on distance
    void ChangeSpeed()
    {
        magnitudeOfSpeed = (agent.remainingDistance / DIVIDING_FACTOR); //Dividing by 20 makes the speed a good average speed

	if(agent.remainingDistance > 100 || agent.remainingDistance == 0)
		magnitudeOfSpeed = 5; 
        agent.speed = BaseSpeed * magnitudeOfSpeed;

        if(agent.speed > MaxSpeed)
        {
            agent.speed = MaxSpeed;
        }
        else if(agent.speed < MinSpeed)
        {
            agent.speed = MinSpeed;
        }
    }

    public void PointOfInterestCheck()  //finds the PoI with the highest weight, sets its transform as the goal, then changes it if the weight = 0
    {
        PointofInterest pointToVisit = FindHighestWeight();
        if (pointToVisit == null)
        {
            followPlayer = true;
            NpcState = NPCState.Follow;
            Goal = player.transform;
        }
        else
        {
            NpcState = NPCState.Wander;
            Goal = pointToVisit.transform;
            pointToVisit.isVisiting = true;
            if (pointToVisit.CurrentWeight == 0)
            {
                ChangePointOfInterest();
            }
        }    
    }

    public void ChangePointOfInterest()  //changes the current point of interest if its weight equals zero
    {
        PointofInterest p = FindHighestWeight();
        if (p != null && p.CurrentWeight == 0)
        {
            followPlayer = true;
            Goal = GameObject.FindGameObjectWithTag("Player").transform;
            NpcState = NPCState.Follow;
        }
        else
        {
            Goal = FindHighestWeight().transform;
        }
    }

    PointofInterest FindHighestWeight() //loops through the list of PoI's (defined in editor) and finds the one with highest weight
    {
        if (Vector3.Distance(player.transform.position, this.gameObject.transform.position) > DistanceFromPlayerUntilFollow)
        {
            return null;
        }
        else
        {
            int highestWeightIndex = 0;
            float highestWeight = 0f;
            foreach (PointofInterest p in PointsOfInterest)
            {
                if (p != null)
                {
                    if (p.hasVisited == false)
                    {
                        if (p.CurrentWeight > highestWeight)
                        {
                            highestWeight = p.CurrentWeight;
                            highestWeightIndex = PointsOfInterest.IndexOf(p);
                        }
                    }
                }
            }
            if (PointsOfInterest.Count > highestWeightIndex)
                return PointsOfInterest[highestWeightIndex];
            else
                return null;
                   
        }
    }

    void CheckForSurroundingPointsOfInterest(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        PointsOfInterest.Clear();
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].gameObject.tag == "DynamicPoI")
            {
                if (!PointsOfInterest.Contains(hitColliders[i].gameObject.GetComponent<PointofInterest>()))
                    PointsOfInterest.Add(hitColliders[i].gameObject.GetComponent<PointofInterest>());
            }
            i++;
        }
    }
}
