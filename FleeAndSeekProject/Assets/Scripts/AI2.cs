using UnityEngine;
using System.Collections;

public enum State { Normal, GoAway};

public class AI2 : Pathfinding
{

    public Transform target;
    //private CharacterController controller;
    private bool newPath = true;
    private bool moving = false;
    private GameObject[] AIList;
    private bool targetReadyToChange = true;
    public GameObject triggerZone;
    public GameObject enemy;
    public State currentState;
    bool entered;

    void Start()
    {
        AIList = GameObject.FindGameObjectsWithTag("Enemy");
        currentState = State.Normal;
        entered = false;
    }

    void OnTriggerEnter(Collider c)
    {
        if(c.tag == "FleeZone")
        {
            if(entered == false)
            {
                currentState = State.GoAway;
                entered = true;
                Debug.Log("Go Away");
            }
            else
            {
                currentState = State.Normal;
                entered = false;
                Debug.Log("Normal");
            }
        }
    }

    void Update()
    {
        if (targetReadyToChange == true)
        {
            target.position = new Vector3(Random.Range(-5f, 5.5f), 0, Random.Range(-5f, 5.5f));
            StartCoroutine("ChangeTargetTimer");
        }

        if (Vector3.Distance(target.position, transform.position) < 25F && !moving)
        {
            if (newPath)
            {
                StartCoroutine(NewPath());
            }
            moving = true;
        }
        else if (Vector3.Distance(target.position, transform.position) < 2F)
        {
            //Stop!
        }
        else if (Vector3.Distance(target.position, transform.position) < 35F && moving)
        {
            if (Path.Count > 0)
            {
                if (Vector3.Distance(target.position, Path[Path.Count - 1]) > 5F)
                {
                    StartCoroutine(NewPath());
                }
            }
            else
            {
                if (newPath)
                {
                    StartCoroutine(NewPath());
                }
            }
            //Move the ai towards the player
            MoveMethod();
        }
        else
        {
            moving = false;
        }
    }

    IEnumerator NewPath()
    {
        newPath = false;
        FindPath(transform.position, target.position);
        yield return new WaitForSeconds(1F);
        newPath = true;
    }

    IEnumerator ChangeTargetTimer()
    {
        targetReadyToChange = false;
        yield return new WaitForSeconds(1f);
        targetReadyToChange = true;
    }


    private void MoveMethod()
    {
        if (Path.Count > 0)
        {
            Vector3 direction = (Path[0] - transform.position).normalized;

            foreach (GameObject g in AIList)
            {
                if (Vector3.Distance(g.transform.position, transform.position) < 1F)
                {
                    Vector3 dir = (transform.position - g.transform.position).normalized;
                    dir.Set(dir.x, 0, dir.z);
                    direction += 0.2F * dir;
                }
            }

            direction.Normalize();


            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, Time.deltaTime * 12F);
            if (transform.position.x < Path[0].x + 0.4F && transform.position.x > Path[0].x - 0.4F && transform.position.z > Path[0].z - 0.4F && transform.position.z < Path[0].z + 0.4F)
            {
                Path.RemoveAt(0);
            }

            RaycastHit[] hit = Physics.RaycastAll(transform.position + (Vector3.up * 20F), Vector3.down, 100);
            float maxY = -Mathf.Infinity;
            foreach (RaycastHit h in hit)
            {
                if (h.transform.tag == "Untagged")
                {
                    if (maxY < h.point.y)
                    {
                        maxY = h.point.y;
                    }
                }
            }
            if (maxY > -100)
            {
                transform.position = new Vector3(transform.position.x, maxY + 1F, transform.position.z);
            }
        }
    }
}