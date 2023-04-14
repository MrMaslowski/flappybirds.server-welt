using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Obstacles move from right to left. You can set their speed in Unity!
        transform.position += Vector3.left * speed * Time.deltaTime;
    }
}
