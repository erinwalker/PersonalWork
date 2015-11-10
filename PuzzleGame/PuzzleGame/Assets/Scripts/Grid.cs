using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {
    //Sets variables needed
    public GameObject[] gridPath;
    bool[] added = new bool[29];
    public Material green;
    public Material red;
    private int wrong = 0;
    public GameObject door;
    private int i;
    private int numberOfGrids = 0;
    public GameObject plane;
    public GameObject gridOn;

    void Start()
    {
        //Sets initial added values to false
        for(int i = 0; i < gridPath.Length; i++)
        {
            added[i] = false;
        }
    }


    
    void OnTriggerEnter(Collider grid)
    {
        gridOn = grid.gameObject;
        //Checks to see if the grid piece player is on is on the valid path
        for(i = 0; i < gridPath.Length; i++)
        {
            //returns whether the player has already stepped on that grid piece or not
            if(added[i] == false)
            {
                if(grid.gameObject == gridPath[i])
                {
                    gridPath[i].gameObject.renderer.material= green;
                    Debug.Log("Grid" + i + " Entered");
                    numberOfGrids++;
                    added[i] = true;
                    Debug.Log(numberOfGrids);
                }
             }   
        }
        //Tells player if the grid piece is wrong and reloads if too many wrong
        if (grid.gameObject.tag == "Grid" && wrong == 5)
        {
            Debug.Log("reload");
            grid.gameObject.renderer.material = red;
            Application.LoadLevel(1);
        }
        else if (grid.gameObject.tag == "Grid")
        {
            Debug.Log("wrong");
            grid.gameObject.renderer.material = red;
            wrong++;
            Debug.Log(wrong);
        }
    }

    void Update()
    {
        //Opens door to next puzzle if player lands on final grid piece
        if (gridOn == gridPath[28])
        {
            door.collider.enabled = false;
            door.renderer.enabled = false;
        }
    }
}
