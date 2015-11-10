using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour
{
    public Vector2 direction;
    private Vector2 keyDirection;

    public InputController()
    {
        direction = new Vector2();
        keyDirection = new Vector2();
    }

    // Update is called once per frame
    void Update()
    {
        keyDirection.x = keyDirection.y = 0;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            keyDirection.y += 1;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            keyDirection.y += -1;
        }

        direction = keyDirection;
        direction.Normalize();
    }
}
