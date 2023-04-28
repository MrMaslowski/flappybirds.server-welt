using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMovement : MonoBehaviour
{
    public float speed;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Ground moves from right to left. You can set their speed in Unity!
        transform.position += Vector3.left * speed * Time.deltaTime;

        // if object is out of frame, move it to the right again
        // noch nicht smoothe genug
        // wie kann ich genau sagen wann es raus ist?? Framewidth/2 - position.x + objectwidth/2 <= 0!

        if(GetComponent<SpriteRenderer>().bounds.size.x <= -transform.position.x + 0.1)
        {
            // move to the right again
            transform.position = new Vector3(-transform.position.x, transform.position.y, 0);
        }
    }
}
